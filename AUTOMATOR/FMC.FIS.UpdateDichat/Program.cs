using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using FMC.FIS.Business.Code.Api.Dichat;
using FMC.CREDZ.API.Models;
using System.IO;
using System.Text;

namespace FMC.FIS.GenerateScore
{
    internal class Program
    {
        static async Task Main(string[] args)
        {


            var dtIni = DateTime.Now.AddHours(-3).ToString("yyyy-MM-dd HH:mm:ss");
            var dtFim = DateTime.Now.AddHours(3).ToString("yyyy-MM-dd HH:mm:ss");

            try
            {

                using var httpClient = new HttpClient();

                httpClient.DefaultRequestHeaders.Add("x-api-key", "SUA_API_KEY");
                httpClient.DefaultRequestHeaders.Add("Accept", "application/json");

                var url = String.Format("http://10.40.0.52/bi/api/dichat/interactions?from={0}&to={1}&limit=500", dtIni, dtFim);
                var response = await httpClient.GetAsync(url);

                response.EnsureSuccessStatusCode();

                var json = await response.Content.ReadAsStringAsync();

                var interactions = JsonSerializer.Deserialize<DichatResponse>(json, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                SaveFile(interactions.Data.Count().ToString() + " itens encontrados.");
                if (interactions.Data.Count() > 0)
                {
                    var navegacoes = interactions.Data.GroupBy(x => x.CPF).Select(grupoCpf =>
                    {
                        var primeiroCpf = grupoCpf.OrderBy(p => p.CreatedAt).First();

                        var navigation = new Navigation
                        {
                            Cpf = primeiroCpf.CPF,
                            CdFrom = "dichat",
                            DsOrigem = "dichat",
                            DtInsert = primeiroCpf.CreatedAt.DateTime,
                            FlNegotiateNow = true,
                            Product = new List<CREDZ.API.Models.Product>()
                        };

                        foreach (var grupoContrato in grupoCpf.GroupBy(x => x.ContractId))
                        {
                            var primeiroContrato = grupoContrato.First();

                            var product = new CREDZ.API.Models.Product
                            {
                                Account = primeiroContrato.ContractId,
                                Age = 0,
                                DtInsert = primeiroContrato.CreatedAt.DateTime,
                                ProductType = "3",
                                VlFull = primeiroContrato.TotalAmount ?? 0,
                                VlMinimum = primeiroContrato.TotalAmount ?? 0,
                                Simulate = new List<Simulate>(),
                                Agreement = new List<CREDZ.API.Models.Agreement>()
                            };

                            foreach (var item in grupoContrato.OrderBy(x => x.CreatedAt))
                            {
                                if (item.Status == "simulated")
                                {
                                    product.Simulate.Add(new Simulate
                                    {
                                        VlEntrace = 99,
                                        NrParcel = item.Installments,
                                        DtEntrace = item.CreatedAt.DateTime.AddDays(7),
                                        DtFirstParcel = item.CreatedAt.DateTime.AddDays(7).AddMonths(1),
                                        VlDiscount = item.DiscountValue ?? 0,
                                        FlPromisse = false,
                                        FlProcess = true,
                                        DtInsert = item.CreatedAt.DateTime
                                    });
                                }
                                else if (item.Status == "registered")
                                {
                                    product.Agreement.Add(new CREDZ.API.Models.Agreement
                                    {
                                        VlEntrace = 99,
                                        NrParcel = item.Installments,
                                        DtEntrace = item.CreatedAt.DateTime.AddDays(7),
                                        DtFirstParcel = item.CreatedAt.DateTime.AddDays(7).AddMonths(1),
                                        VlDiscount = item.DiscountValue ?? 0,
                                        FlPromisse = false,
                                        DtMainframeRegister = item.CreatedAt.DateTime,
                                        FlAccept = true,
                                        VlParcel = item.InstallmentAmount ?? 0,
                                        DtInsert = item.CreatedAt.DateTime
                                    });
                                }
                            }

                            navigation.Product.Add(product);
                        }

                        return navigation;
                    }).ToList();
                    SaveFile("Gravando navegações " + navegacoes.Count());
                    foreach (var item in navegacoes)
                    {
                        try
                        {
                            var navBLL = new FMC.CREDZ.API.Code.Business.BLL.NavigationBLL();
                            var nav = navBLL.GetNavigation(item.Cpf, item.CdFrom, item.DsOrigem, item.DtInsert);
                            if (nav == null)
                                navBLL.Add(item);
                            else
                            {
                                foreach (var prod in item.Product)
                                {
                                    var product = nav.Product.Where(p => p.Account == prod.Account).FirstOrDefault();

                                    if (product == null)
                                    {
                                        nav.Product.Add(prod);
                                    }
                                    else
                                    {
                                        foreach (var simulate in prod.Simulate)
                                        {
                                            if (!product.Simulate.Where(s => s.DtEntrace.Date == simulate.DtEntrace.Date && s.VlEntrace == simulate.VlEntrace).Any())
                                            {
                                                product.Simulate.Add(simulate);
                                            }
                                        }
                                        foreach (var agreement in prod.Agreement)
                                        {
                                            if (!product.Simulate.Where(s => s.DtEntrace.Date == agreement.DtEntrace.Date && s.VlEntrace == agreement.VlEntrace).Any())
                                            {
                                                product.Agreement.Add(agreement);
                                            }
                                        }

                                    }
                                }

                                navBLL.Update(nav);
                            }
                        }
                        catch (Exception ex)
                        {
                            string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                            while (ex.InnerException != null)
                            {
                                ex = ex.InnerException;
                                erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                            }
                            SaveFile(erro);
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                string erro = ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                while (ex.InnerException != null)
                {
                    ex = ex.InnerException;
                    erro += ex.Message + " | " + ex.StackTrace + Environment.NewLine;
                }
                SaveFile(erro);
            }



        }

        public static string pathLog = AppDomain.CurrentDomain.BaseDirectory + "LOG";
        public static void SaveFile(string message)
        {
            string fileName = pathLog + "\\LOG_" + Environment.CurrentManagedThreadId.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHH") + ".log";
            StreamWriter logExecution = null;
            try
            {
                if (!Directory.Exists(pathLog))
                    Directory.CreateDirectory(pathLog);

                using (logExecution = new StreamWriter(fileName, true, ASCIIEncoding.Default))
                {
                    logExecution.WriteLine(Environment.NewLine + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + " => " + message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

