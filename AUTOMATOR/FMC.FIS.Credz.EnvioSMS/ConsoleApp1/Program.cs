using FMC.FIS.BLL;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Code.Api.Cobmais;
using FMC.FIS.Business.Models.BvTelecom;
using FMC.FIS.Business.Models.Cobmais;
using FMC.FIS.Business.Models.Customer;
using FMC.FIS.Business.Models.FIS;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Text;

namespace FMC.FIS.CREZ.EnvioEmailQuebra
{
    public class Cpf
    {
        [Key]
        public string item { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var currentProcess = System.Diagnostics.Process.GetCurrentProcess();
            var builder = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json", optional: false);

            IConfiguration config = builder.Build();

            Constants.UserCobmaisCredz = config.GetValue<string>("UserCobmaisCredz");
            Constants.PassCobmaisCredz = config.GetValue<string>("PassCobmaisCredz");
            Constants.UrlApiCobmaisCredz = config.GetValue<string>("UrlApiCobmaisCredz");


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

                DateTime dtLead = DateTime.Now.Hour > 11 ? DateTime.Today : DateTime.Today.AddDays(-1);
                //IList<Person> listPerson = new PersonBLL().GetPersonSendSMS(dtLead).ToList();

                var query =
    " select distinct top 3000 pe.IdPerson, p.idproduct, pe.DsName,Age, case when Store is null then Subproduct else Store end Store, h.contato " +
    " from FIS.dbo.Lead l " +
    " 	inner join FIS.dbo.Product p " +
    " 		on l.IdProduct = p.IdProduct " +
    " 	inner join FIS.dbo.Person pe " +
    " 		on pe.IdPerson = p.IdPerson " +
    " 	inner join WORK.dbo.[BASEHOTDM-20072026] h " +
    " 		on  RIGHT('00000000000' + CPF, 11) = pe.NrCNPJCPF " +
    " 	inner join bi.dbo.Person bip " +
    " 		on bip.NrCNPJCPF = pe.NrCNPJCPF " +
    " 	inner join DIGICOB.dbo.Contract co " +
    " 		on co.idperson = bip.IdPerson " +
    " where l.DtInsert >= CONVERT(Date, getdate()) " +
    " and age between 360 and 1500 " +
    " and not exists " +
    " ( " +
    " 	select * from fis.CREDZ.SMS sm  " +
    " 	where sm.IdPerson = p.IdPerson " +
    " 	and sm.dtEnvio >= '2026-07-30' " +
    " ) ";
;

                var listPerson = new GenericQueryBLL<PersonRet>().GetCollection(query);

                if (listPerson.Count > 0)
                {
                    string cpf = "";
                    Contrato contract = null;

                    IList<SingleRequest> listSend = new List<SingleRequest>();
                    IList<SMS> listSMS = new List<SMS>();
                    int count = 0;

                    foreach (var person in listPerson.Distinct().ToList())
                    {
                        Console.WriteLine("Total: " + count);
                        try
                        {

                            var obj = SendSMS(person.contato, person.DsName.Split(' ').FirstOrDefault(), person.Store);
                            if (obj != null)
                            {
                                count++;
                                listSend.Add(obj);
                                listSMS.Add
                                    (
                                        new Business.Models.FIS.SMS()
                                        {
                                            idPerson = person.IdPerson,
                                            age = person.Age,
                                            telefone = Convert.ToInt64(person.contato),
                                            dtEnvio = DateTime.Now
                                        }
                                    );
                                Console.WriteLine(listSend.Count);
                                if (listSend.Count > 20)
                                {
                                    if (new BvSmsBLL().SmsBulk(new BulkRequest() { bulk = listSend }) == "OK")
                                    {
                                        try
                                        {
                                            new SmsBLL().AddRangeNormal(listSMS.ToList());
                                        }
                                        catch (Exception ex)
                                        {

                                        }
                                    }
                                    listSend = new List<SingleRequest>();
                                    listSMS = new List<SMS>();
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
                            Util.SaveFile(erro);
                        }
                    }

                    if (listSend.Count > 1)
                    {
                        if (new BvSmsBLL().SmsBulk(new BulkRequest() { bulk = listSend }) == "OK")
                            new SmsBLL().AddRangeNormal(listSMS.ToList());
                    }
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
        }
        private static SingleRequest SendSMS(string phone, string nome, string loja)
        {
            string message = "Ola," + nome + "! Vamos facilitar a regularizacao do seu cartao DM referente a loja " + loja + "? Whatsapp: https://zaps.chat/r/dm.";

            if (message.Length <= 160)
            {
                //var ret = new BvSmsBLL().SmsSingle
                //               (
                return new SingleRequest()
                {
                    celular = phone,
                    mensagem = message.ToString(),
                    carteiraId = 1064,
                    parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss")
                };
                //               );

                //return ret.ToUpper() == "OK";

            }
            else
            {
                return null;
            }
        }

    }
}
