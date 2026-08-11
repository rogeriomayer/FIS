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
      " select  top 2500 pe.IdPerson, p.idproduct, pe.DsName,Age, IdContract, case when Store is null then Subproduct else Store end Store, phs.Phone as Contato " +
"  from FIS.dbo.Lead l " +
"  	inner join FIS.dbo.Product p " +
"  		on l.IdProduct = p.IdProduct " +
"  	inner join FIS.dbo.Person pe " +
"  		on pe.IdPerson = p.IdPerson " +
" 	inner join bi.dbo.Person bip " +
"  		on bip.NrCNPJCPF = pe.NrCNPJCPF " +
"  	inner join DIGICOB.dbo.Contract co " +
"  		on co.idperson = bip.IdPerson " +
"       AND co.CollectionCount > 0 " +
" 	OUTER APPLY ( " +
" 				select top 1 tel.Phone, tel.Fonte, tel.dt, tel.score " +
" 				from  " +
" 				( " +
" 					select contato as 'Phone', 'hot DM' as Fonte,  " +
"                           COALESCE( " +
"                           TRY_CONVERT(datetime, data_notificacao, 103),  " +
"                               TRY_CONVERT(datetime, data_notificacao, 120)" +
"                           ) AS dt, 10 as Score " +
" 					from WORK.dbo.[BASEHOTDM-20072026] bh " +
" 					where bh.cpf = pe.NrCNPJCPF " +
" 					union all " +
" 					select  Phone, 'Site' as Fonte, DtInsert as dt, 9 as Score " +
" 					from CREDZ.dbo.Billet bl (nolock) " +
" 					where Phone is not null " +
" 					and bl.CPF = pe.NrCNPJCPF " +
" 					union all " +
" 					select CONVERT(varchar(11), telefone) as 'Phone', 'URA', ura.dtLigacao as dt, 8 as Score " +
" 					from CREDZ.dbo.RetornoUra ura (nolock) " +
" 					where ura.cpf = pe.NrCNPJCPF " +
" 					union all " +
" 					select  NrPhone as 'Phone', 'COBMAIS', DtUpdate as dt, ph.IdPhoneStatus as Score " +
" 					from FIS.dbo.Phone ph (nolock) " +
" 					where ph.IdPerson = pe.IdPerson " +
" 				) as tel " +
" 				order by tel.Score desc, Dt DESC " +
" 				) phs   " +
"  where l.DtInsert >= CONVERT(Date, getdate()-1) " +
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
            string message = nome + "!Você tem condições especiais para quitar seu Cartão DM " + loja + " pelo WhatsApp: https://fmc.digital/dm ou 34997973742";

            if (message.Length > 160)
            {
                message = nome + "!Você tem condições especiais para quitar seu Cartão DM " + loja + " pelo WhatsApp: https://fmc.digital/dm";
            }
            else if (message.Length > 160)
            {
                return null;
            }
            return new SingleRequest()
            {
                celular = phone,
                mensagem = message.ToString(),
                carteiraId = 1064,
                parceiroId = "credZ" + DateTime.Now.ToString("ddMMyyyyHHmmss")
            };
        }
    }
}
