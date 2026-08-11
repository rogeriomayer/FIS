
using Microsoft.Extensions.Configuration;
using FMC.FIS.Business.BLL;
using FMC.FIS.Business.Models.FIS;
using FMC.FIS.EnvioEmailCredz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using FMC.FIS.EnvioRCSNews;

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
        //IList<Discount> Discounts = new DiscountBLL().GetByProductType(3).ToList();
        //IList<Person> listPerson = new PersonBLL().GetPersonSendRCSNews().ToList();
        var query =
   " select  top 3000 pe.IdPerson, p.idproduct, pe.DsName,Age, IdContract, case when Store is null then Subproduct else Store end Store, phs.Phone as Contato " +"  from FIS.dbo.Lead l " +
"  	inner join FIS.dbo.Product p " +
"  		on l.IdProduct = p.IdProduct " +
"  	inner join FIS.dbo.Person pe " +
"  		on pe.IdPerson = p.IdPerson " +
" 	inner join bi.dbo.Person bip " +
"  		on bip.NrCNPJCPF = pe.NrCNPJCPF " +
"  	inner join DIGICOB.dbo.Contract co " +
"  		on co.idperson = bip.IdPerson " +
"       AND co.CollectionCount > 0 " +
//"       AND CO.SUBPRODUCT LIKE '%Credz%'" + 
" 	OUTER APPLY ( " +
" 				select top 1 tel.Phone, tel.Fonte, tel.dt, tel.score " +
" 				from  " +
" 				( " +
" 					select contato as 'Phone', 'hot DM' as Fonte,  " +
"                           COALESCE( " +
"                           TRY_CONVERT(datetime, data_notificacao, 103),  "+
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
"  and age between 91 and 120 " +
"  	and not exists " +
" 	( " +
" 		select * from fis.CREDZ.SendRCS rc " +
" 		where rc.IdPerson = p.IdPerson " +
" 		and rc.DtInsert >= '2026-07-30' " +
" 	) " +
" order by Age asc";
        var listPerson = new GenericQueryBLL<PersonRet>().GetCollection(query); 
        Util.SaveFile("Foram encontrados " + listPerson.Count + " CPFs ");

        if (listPerson.Count > 0)
        {
            string accout = "";
            var listPhone = new List<string>();

            long idPerson = 0;
            int count = 0;

            //foreach (var person in listPerson.OrderBy(p=>p.Product.).OrderBy(p => p.NrCNPJCPF).ToList())
            foreach (var person in listPerson)
            {
                if (count > 3000)
                    break;
                else
                    Console.WriteLine("Total: " + count);
                if (idPerson != person.IdPerson)
                {
                    idPerson = person.IdPerson;

                    Console.WriteLine(idPerson);

                    //var product = person.Product.Where(p => p.Lead.Where(l => l.DtInsert >= DateTime.Today.AddDays(-1)).Any()).FirstOrDefault();

                    //if (product != null && !listPhone.Where(p => person.Phone.Where(e => e.NrPhone == p).Any()).Any())
                    //{
                    /*var phones = person.Phone
                        .Where(p => p.IdPhoneStatus == 1 && Convert.ToInt32(p.NrPhone.Substring(2, 1)) >= 6 && p.Blacklist == false)
                        .Select(p => p.NrPhone).FirstOrDefault();

                    if (phones != null && phones.Count() > 0)
                    {*/
                    try
                    {
                        //var lead = product.Lead.Where(p => p.DtInsert >= DateTime.Today.AddDays(-1)).OrderByDescending(p => p.IdLead).FirstOrDefault();

                        //if (lead != null && lead.Age > 77)
                        //{

                        var envioRCS = new EnvioRCS
                            (
                                new RCS()
                                {
                                    Total = count,
                                    IdPerson = person.IdPerson,
                                    IdProduct = person.idproduct,
                                    Nome = person.DsName.Split(' ').FirstOrDefault(),
                                    Phones = { person.contato },
                                    IdContract = person.IdContract,
                                    Atraso = person.Age,
                                    NomeCartao = person.Store
                                }
                            );
                        if (!string.IsNullOrEmpty(envioRCS.Send()))
                            count++;

                        //}

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
                    //}
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



