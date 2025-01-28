
using Microsoft.Extensions.Configuration;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using FMC.FIS.CREDZ.EnvioContatoUra;

var currentProcess = System.Diagnostics.Process.GetCurrentProcess();

try
{
    foreach (var process in System.Diagnostics.Process.GetProcessesByName(currentProcess.ProcessName))
    {

        Util.SaveFile("Processo em execução:" + process.MainModule.FileName + " SessionID:" + process.Id);
        Util.SaveFile("Processo corrente:" + currentProcess.MainModule.FileName + " SessionID:" + currentProcess.Id);
        if (process.MainModule.FileName == currentProcess.MainModule.FileName && process.Id != currentProcess.Id)
        {
            //Log.SaveFile("Matando processo " + process.MainModule.FileName + " SessionID:" + process.Id);
            process.Kill();
        }
    }
    var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

    IConfiguration config = builder.Build();

    Constants.UserCobmaisCredz = config.GetValue<string>("UserCobmaisCredz");
    Constants.PassCobmaisCredz = config.GetValue<string>("PassCobmaisCredz");
    Constants.UrlApiCobmaisCredz = config.GetValue<string>("UrlApiCobmaisCredz");

    while (DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
    {
        DateTime dtIni = DateTime.Now.Hour < 12 ? DateTime.Today.AddDays(-1) : DateTime.Today;
        IList<Discount> Discounts = new DiscountBLL().GetByProductType(3).ToList();
        IList<Product> listProduct = new ProductBLL().GetProductsURA().ToList();
        Util.SaveFile("Foram encontrados " + listProduct.Count + " CPFs ");
        IList<EnviosBalance> enviosBalance = new List<EnviosBalance>();

        if (listProduct.Count > 0)
        {
            string accout = "";
            var listEmail = new List<string>();
            var listPhone = new List<string>();

            IList<KeyValuePair<string, int>> smtpServers = new List<KeyValuePair<string, int>>();
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));

            //smtpServers.Add(new KeyValuePair<string, int>("10.40.0.92", 26));
            //smtpServers.Add(new KeyValuePair<string, int>("10.40.0.94", 26));
            //smtpServers.Add(new KeyValuePair<string, int>("10.40.0.82", 25));

            int balance = 0;
            long idPerson = 0;
            int countYahoo = 0;

            foreach (var product in listProduct.OrderBy(p => p.Person.NrCNPJCPF).ToList())
            {
                try
                {
                    Console.Clear();
                    Console.Write(product.DsProduct);
                    var lead = product.Lead.Where(p => p.DtInsert >= dtIni).OrderByDescending(p => p.IdLead).FirstOrDefault();

                    if (idPerson != product.IdPerson)
                    {
                        idPerson = product.IdPerson;

                        var envioRCS = new EnvioRCS
                                   (
                                       new RCS()
                                       {
                                           IdPerson = product.IdPerson,
                                           IdProduct = product.IdProduct,
                                           Nome = product.Person.DsName.Trim(),
                                           //Phones = phones,
                                           Atraso = lead.Age,
                                           Desconto = Discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel == 1).FirstOrDefault().MaxDiscount,
                                           Lead = lead,
                                           NomeCartao = product.ProductSpecification != null ? product.ProductSpecification.Description : "Cartão Credz",
                                           NumeroCartao = product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8),
                                           UrlCartao = product.ProductSpecification != null ? product.ProductSpecification.UrlImage : ""
                                       }
                                   );
                        envioRCS.Send();


                        if (accout != product.DsProduct)
                        {
                            accout = product.DsProduct;
                            listEmail.Clear();
                            listPhone.Clear();
                        }
                        if (!listEmail.Where(p => product.Person.Email.Where(e => e.DsEmail == p).Any()).Any())
                        {
                            var emails = product.Person.Email.Where(p => p.flBloqueado == false && Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).Distinct().ToList();
                            if (emails.Count() == 0)
                            {
                                product.Person.Email.ToList().ForEach(p => Util.SaveFile(p.DsEmail));
                                Util.SaveFile("update email set flbloqueado = 1 where idperson = " + product.IdPerson.ToString() + ";");
                            }

                            countYahoo = countYahoo + emails.Where(p => p.Contains("yahoo")).Count();
                            if (countYahoo > 50)
                                emails = emails.Where(p => !p.Contains("yahoo")).ToList();

                            if (emails != null && emails.Count() > 0)
                            {
                                try
                                {


                                    if (lead != null && lead.Age > 77)
                                    {

                                        var envioEmailThread = new EnvioEmailThread
                                            (
                                                new EnvioEmailSMS()
                                                {
                                                    IdPerson = product.IdPerson,
                                                    IdProduct = product.IdProduct,
                                                    Nome = product.Person.DsName.Trim(),
                                                    Email = emails,
                                                    Atraso = lead.Age,
                                                    Desconto = Discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel == 1).FirstOrDefault().MaxDiscount,
                                                    Lead = lead,
                                                    NomeCartao = product.ProductSpecification != null ? product.ProductSpecification.Description : "Cartão Credz",
                                                    NumeroCartao = product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8),
                                                    SmtpServer = emails.Contains("yahoo") ? smtpServers.ToArray()[0] : smtpServers.ToArray()[balance],
                                                    UrlCartao = product.ProductSpecification != null ? product.ProductSpecification.UrlImage : ""
                                                }
                                            );
                                        envioEmailThread.SendMail();
                                        balance++;
                                        if (balance >= smtpServers.Count()) balance = 0;
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
                                    Util.SaveFile("Laço: Erro ao enviar e-mail para " + emails.FirstOrDefault() + " conta " + product.DsProduct);
                                    Util.SaveFile(erro);
                                }
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                }
            }
        }

        break;
        Util.SaveFile("Processo finalizado!");
    }
}
catch (Exception ex)
{
    string erro = ex.Message;
    while (ex.InnerException != null)
    {
        ex = ex.InnerException;
        erro += " | " + ex.Message;
    }
    Util.SaveFile("Erro:" + erro);
}

public class EnviosBalance
{
    public string provedor { get; set; }

    public string smtp { get; set; }
}