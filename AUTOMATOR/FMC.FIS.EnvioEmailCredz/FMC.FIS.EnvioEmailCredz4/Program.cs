
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

    int i = 1;
    while (i < 10)
    {
        i++;
        var query =
    " SELECT DISTINCT TOP (2000)  " +
"     pe.IdPerson, " +
"     p.IdProduct, " +
"     pe.DsName, " +
"     Age, " +
"     CASE " +
"         WHEN Store IS NULL THEN Subproduct " +
"         ELSE Store " +
"     END AS Store, " +
"     Emails.Contato " +
" FROM FIS.dbo.Lead l " +
" INNER JOIN FIS.dbo.Product p " +
"     ON l.IdProduct = p.IdProduct " +
" INNER JOIN FIS.dbo.Person pe " +
"     ON pe.IdPerson = p.IdPerson " +
" INNER JOIN BI.dbo.Person bip " +
"     ON bip.NrCNPJCPF = pe.NrCNPJCPF " +
" INNER JOIN DIGICOB.dbo.Contract co " +
"     ON co.IdPerson = bip.IdPerson " +
" CROSS APPLY " +
" ( " +
"     SELECT STRING_AGG(em.DsEmail, ';') AS Contato " +
"     FROM FIS.dbo.Email em " +
"     WHERE em.IdPerson = pe.IdPerson " +
" 	and (em.flBloqueado is null or em.flBloqueado = 0) " +
" ) Emails " +
" WHERE l.DtInsert >= CONVERT(date, GETDATE()) " +
"   AND Emails.Contato like '%@%.%' " +
"   AND Age BETWEEN 90 AND 150 " +
"   AND NOT EXISTS " +
" 	( " +
" 		SELECT 1 " +
" 		FROM fis.CREDZ.SendEmail rc " +
" 		WHERE rc.IdPerson = p.IdPerson " +
" 		  AND rc.DtInsert >= '2026-07-30' " +
" 	) " +
"   AND NOT EXISTS " +
" 	( " +
" 		SELECT 1 " +
" 		FROM fis.CREDZ.SendRCS rc " +
" 		WHERE rc.IdPerson = p.IdPerson " +
" 		  AND rc.DtInsert >= '2026-07-30' " +
" 	) " +
"   AND NOT EXISTS " +
" 	( " +
" 		SELECT 1 " +
" 		FROM fis.CREDZ.SMS rc " +
" 		WHERE rc.IdPerson = p.IdPerson " +
" 		  AND rc.dtEnvio >= '2026-07-30' " +
" 	); ";
        var listPerson = new GenericQueryBLL<PersonRet>().GetCollection(query);
        Util.SaveFile("Foram encontrados " + listPerson.Count + " CPFs ");

        if (listPerson.Count > 0)
        {
            long idPerson = 0;


            foreach (var person in listPerson.OrderBy(p => p.IdPerson).ToList())
            {
                if (idPerson != person.IdPerson)
                {
                    idPerson = person.IdPerson;
                    System.Threading.Thread.Sleep(500);
                    Console.WriteLine("Enviando para " + person.Contato);
                    EnvioEmailThread.SendMail(person);
                }
            }

            Util.SaveFile("Processo finalizado!");
        }
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
