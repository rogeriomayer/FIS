
using Microsoft.Extensions.Configuration;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.EnvioEmailCredz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

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
        IList<Discount> Discounts = new DiscountBLL().GetByProductType(3).ToList();
        IList<Person> listPerson = new PersonBLL().GetPersonSendRCSNews().ToList();
        Util.SaveFile("Foram encontrados " + listPerson.Count + " CPFs ");

        if (listPerson.Count > 0)
        {
            string accout = "";
            var listPhone = new List<string>();

            long idPerson = 0;
            int count = 0;

            foreach (var person in listPerson.OrderBy(p => p.NrCNPJCPF).ToList())
            {
                if (count > 2000)
                    break;
                else
                    Console.WriteLine("Total: " + count);
                if (idPerson != person.IdPerson)
                {
                    idPerson = person.IdPerson;

                    Console.WriteLine(idPerson);

                    var product = person.Product.Where(p => p.Lead.Where(l => l.DtInsert >= DateTime.Today.AddDays(-1)).Any()).FirstOrDefault();

                    if (!listPhone.Where(p => person.Phone.Where(e => e.NrPhone == p).Any()).Any())
                    {
                        /*var phones = person.Phone
                            .Where(p => p.IdPhoneStatus == 1 && Convert.ToInt32(p.NrPhone.Substring(2, 1)) >= 6 && p.Blacklist == false)
                            .Select(p => p.NrPhone).FirstOrDefault();

                        if (phones != null && phones.Count() > 0)
                        {*/
                        try
                        {
                            var lead = product.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).OrderByDescending(p => p.IdLead).FirstOrDefault();

                            if (lead != null && lead.Age > 77)
                            {

                                var envioRCS = new EnvioRCS
                                    (
                                        new RCS()
                                        {
                                            IdPerson = person.IdPerson,
                                            IdProduct = product.IdProduct,
                                            Nome = person.DsName.Trim(),
                                            //Phones = phones,
                                            Atraso = lead.Age,
                                            Desconto = Discounts.Where(p => (lead.Age >= p.MinAge && lead.Age <= p.MaxAge) && p.MaxParcel == 1).FirstOrDefault().MaxDiscount,
                                            Lead = lead,
                                            NomeCartao = product.ProductSpecification != null ? product.ProductSpecification.Description : "Cartão Credz",
                                            NumeroCartao = product.DsProduct.StartsWith("000") ? product.DsProduct.Substring(3, 8) + "********" : product.DsProduct.Substring(0, 8),
                                            UrlCartao = product.ProductSpecification != null ? product.ProductSpecification.UrlImage : ""
                                        }
                                    );
                                if (!string.IsNullOrEmpty(envioRCS.Send()))
                                    count++;

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
                            //Util.SaveFile("Laço: Erro ao enviar RCS para " + phones + " conta " + product.DsProduct);
                            Util.SaveFile(erro);
                        }
                        //}
                    }
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

