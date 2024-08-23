
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
        DateTime dtIni = DateTime.Now.Hour < 12 ? DateTime.Today.AddDays(-2) : DateTime.Today;
        IList<Discount> Discounts = new DiscountBLL().GetByProductType(3).ToList();
        IList<Person> listPerson = new PersonBLL().GetByMailSend(3, dtIni, 7600, 251, 330).ToList();
        Util.SaveFile("Foram encontrados " + listPerson.Count + " CPFs ");
        IList<EnviosBalance> enviosBalance = new List<EnviosBalance>();

        if (listPerson.Count > 0)
        {
            string accout = "";
            var listEmail = new List<string>();
            var listPhone = new List<string>();

            IList<KeyValuePair<string, int>> smtpServers = new List<KeyValuePair<string, int>>();
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.21", 25));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.92", 26));
            //smtpServers.Add("10.40.0.92", 26);
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.94", 26));
            smtpServers.Add(new KeyValuePair<string, int>("10.40.0.82", 25));
            //IList<string> smtpServers = new List<string> { "10.40.0.92", "10.40.0.92", "10.40.0.92" };
            int balance = 0;
            long idPerson = 0;
            int countYahoo = 0;

            foreach (var person in listPerson.OrderBy(p => p.NrCNPJCPF).ToList())
            {
                if (idPerson != person.IdPerson)
                {
                    idPerson = person.IdPerson;

                    foreach (var product in person.Product.Where(p => p.Lead.Where(l => l.DtInsert >= DateTime.Today.AddDays(-1)).Any()))
                    {
                        if (accout != product.DsProduct)
                        {
                            accout = product.DsProduct;
                            listEmail.Clear();
                            listPhone.Clear();
                        }
                        if (!listEmail.Where(p => person.Email.Where(e => e.DsEmail == p).Any()).Any())
                        {
                            var emails = product.Person.Email.Where(p => p.flBloqueado == false && Util.IsEmail(p.DsEmail)).Select(p => p.DsEmail).Distinct().ToList();
                            /*var emails = product.Person.Email.Where(p => Util.IsEmail(p.DsEmail)
                                && (!p.DsEmail.Contains("outlook"))
                                && (!p.DsEmail.Contains("hotmail"))
                                && (!p.DsEmail.Contains("yahoo"))
                            ).Select(p => p.DsEmail).ToList();*/

                            countYahoo = countYahoo + emails.Where(p => p.Contains("yahoo")).Count();
                            if (countYahoo > 50)
                                emails = emails.Where(p => !p.Contains("yahoo")).ToList();

                            if (emails != null && emails.Count() > 0)
                            {
                                try
                                {
                                    var lead = product.Lead.Where(p => p.DtInsert >= dtIni).OrderByDescending(p => p.IdLead).FirstOrDefault();

                                    if (lead != null && lead.Age > 77)
                                    {

                                        var envioEmailThread = new EnvioEmailThread
                                            (
                                                new EnvioEmailSMS()
                                                {
                                                    IdPerson = person.IdPerson,
                                                    IdProduct = product.IdProduct,
                                                    Nome = person.DsName.Trim(),
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