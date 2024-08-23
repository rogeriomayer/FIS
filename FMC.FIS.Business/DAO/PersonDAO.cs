using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Data.SqlClient;

namespace FMC.FIS.Business.DAO
{
    public class PersonDAO : AbstractRepositoryPersistence<Person>
    {
        public PersonDAO() : base("CNN_FIS") { }

        public Person GetByCPF(string cpf)
        {
            return Context.Where(p => p.NrCNPJCPF == cpf).FirstOrDefault();
        }

        public ICollection<Person> GetByMailSend(int idProductType, DateTime dtLead, int count, int ageIni, int ageFim)
        {
            var query = new StringBuilder();

            //teste agendamento
            /*            
                        query.Append(" SELECT distinct top 10000  pe.* ");
                        query.Append(" FROM Person pe ");
                        query.Append(" 	inner join Product pr ");
                        query.Append(" 		on pr.IdPerson  = pe.IdPerson ");
                        query.Append(" 	inner join Lead le ");
                        query.Append(" 		on le.IdProduct = pr.IdProduct ");
                        query.Append(" 		and le.DtInsert >= '2024-01-30 06:00' ");
                        query.Append(" 	inner join Email em ");
                        query.Append(" 		on em.IdPerson = pe.IdPerson ");
                        query.Append(" where not exists  ");
                        query.Append(" ( ");
                        query.Append(" 	select se.id ");
                        query.Append(" 	from CREDZ.SendEmail se ");
                        query.Append(" 	where pe.IdPerson = se.idPerson ");
                        query.Append(" 	and se.dtInsert >= GETDATE() -30 ");
                        query.Append(" ) ");
                        query.Append(" and not exists  ");
                        query.Append(" ( ");
                        query.Append(" 	select ag.IdAgreement ");
                        query.Append(" 	from StatusLead sl ");
                        query.Append(" 		inner join Agreement ag ");
                        query.Append(" 			on ag.IdStatusLead = sl.IdStatusLead ");
                        query.Append(" 	where sl.IdLead = le.IdLead ");
                        query.Append(" 	and ag.IdAgreementStatus not in (1,5,6) ");
                        query.Append(" ) ");
                        //query.Append(" and age between 151 and 280");

                        return Context.FromSqlRaw(query.ToString()).ToList();

              */

            query.Append(" select distinct TOP ").Append(count).Append(" [pe].[IdPerson], [pe].[DsName], [pe].[DtBirth], ");
            query.Append("           [pe].[DtInsert], [pe].[DtUpdate], [pe].[MotherName],  ");
            query.Append("           [pe].[NrCNPJCPF], [pe].[NrRG],Age, DebitBalance, Score ");
            query.Append("  FROM FIS.DBO.Person pe  ");
            query.Append("       INNER JOIN FIS.DBO.Product PR  ");
            query.Append("           ON PR.IdPerson = PE.IdPerson ");
            query.Append("       INNER join FIS.DBO.EMAIL EM ");
            query.Append("           ON EM.IdPerson = PR.IdPerson ");
            query.Append("           AND flBloqueado = 0");
            query.Append("       INNER join FIS.DBO.LEAD LE ");
            query.Append("           ON LE.IdProduct = PR.IdProduct ");
            query.Append("       LEFT join FIS.DBO.SCOREAI SA ");
            query.Append("           ON SA.IdProduct = PR.IdProduct ");

            query.Append("  WHERE NOT EXISTS(SELECT * FROM FIS.CREDZ.SENDEMAIL SE WHERE SE.IDPERSON = PR.IdPerson and dtinsert >= getdate() -8) ");
            query.Append("       AND LE.DtInsert >= '").Append(dtLead.ToString("yyyy-MM-dd")).Append("'");
            query.Append("       AND PR.IdProductType = 3 ");
            query.Append("       and not exists (select * from CREDZ.SendBrokenEmail sb where sb.IdProduct = pr.IdProduct and dtinsert >= getdate() -2)  ");
            //query.Append("       and em.DtInsert >= GETDATE() -10 ");
            query.Append("       and not exists ");
            query.Append("       ( ");
            query.Append("       select * ");
            query.Append("       from Lead l ");
            query.Append("       where l.DtInsert between DATEADD(day, -30, getdate())  and DATEADD(day, -1, getdate()) ");
            query.Append("       and le.IdProduct = l.IdProduct ");
            query.Append("       ) ");
            query.Append("       and le.age between ").Append(ageIni).Append(" and ").Append(ageFim);
            query.Append("  union all ");
            query.Append(" select distinct TOP  ").Append(count).Append("  [pe].[IdPerson], [pe].[DsName], [pe].[DtBirth],  ");
            query.Append("           [pe].[DtInsert], [pe].[DtUpdate], [pe].[MotherName],  ");
            query.Append("           [pe].[NrCNPJCPF], [pe].[NrRG], Age, DebitBalance ,Score ");
            query.Append("  FROM FIS.DBO.Person pe  ");
            query.Append("       INNER JOIN FIS.DBO.Product PR  ");
            query.Append("           ON PR.IdPerson = PE.IdPerson ");
            query.Append("       INNER join FIS.DBO.EMAIL EM ");
            query.Append("           ON EM.IdPerson = PR.IdPerson ");
            query.Append("           AND flBloqueado = 0");
            query.Append("       INNER join FIS.DBO.LEAD LE ");
            query.Append("           ON LE.IdProduct = PR.IdProduct ");
            query.Append("       LEFT join FIS.DBO.SCOREAI SA ");
            query.Append("           ON SA.IdProduct = PR.IdProduct ");

            query.Append("  WHERE NOT EXISTS(SELECT * FROM FIS.CREDZ.SENDEMAIL SE WHERE SE.IDPERSON = PR.IdPerson and dtinsert >= getdate() -8) ");
            query.Append("       AND LE.DtInsert >= '").Append(dtLead.ToString("yyyy-MM-dd")).Append("'");
            query.Append("       and le.age between ").Append(ageIni).Append(" and ").Append(ageFim);
            query.Append("       AND PR.IdProductType = 3 ");
            query.Append("       AND not exists (select * from CREDZ.SendBrokenEmail sb where sb.IdProduct = pr.IdProduct and dtinsert >= getdate() -2)  ");
            query.Append("       AND not exists (select * from UnvailableBilling u where u.dsproduct = pr.dsproduct collate Latin1_General_CI_AS )");
            query.Append("       and pe.NrCNPJCPF not in ");
            query.Append("       ( ");
            query.Append("          select CPFCNPJ ");
            query.Append("          from CREDZ.dbo.PesquisaCliente pc ");
            query.Append("             inner join credz.cobmaisevent ce ");
            query.Append("                on ce.IdEvent = pc.idEvento");
            query.Append("          where ultimocontato > DATEADD(day,- ce.lift, getdate())  ");
            query.Append("       ) ");
            query.Append("  union all ");
            query.Append("  select distinct TOP 5000 [pe].[IdPerson], [pe].[DsName], [pe].[DtBirth],  ");
            query.Append("           [pe].[DtInsert], [pe].[DtUpdate], [pe].[MotherName],  ");
            query.Append("           [pe].[NrCNPJCPF], [pe].[NrRG], Age, DebitBalance,Score  ");
            query.Append("  FROM Person pe ");
            query.Append("  	inner join Product pr ");
            query.Append("  		on pr.IdPerson  = pe.IdPerson ");
            query.Append("  	inner join Lead le ");
            query.Append("  		on le.IdProduct = pr.IdProduct ");
            query.Append("  		and LE.DtInsert >= '").Append(dtLead.ToString("yyyy-MM-dd")).Append("'");
            query.Append("          and le.age between ").Append(ageIni).Append(" and ").Append(ageFim);
            query.Append("  	inner join Email em ");
            query.Append("  		on em.IdPerson = pe.IdPerson ");
            query.Append("           AND flBloqueado = 0");
            query.Append("       LEFT join FIS.DBO.SCOREAI SA ");
            query.Append("           ON SA.IdProduct = PR.IdProduct ");

            query.Append("  where not exists  ");
            query.Append("  ( ");
            query.Append("  	select se.id ");
            query.Append("  	from CREDZ.SendEmail se ");
            query.Append("  	where pe.IdPerson = se.idPerson ");
            query.Append("  	and se.dtInsert >= GETDATE() -1 ");
            query.Append("  ) ");
            query.Append("  and not exists  ");
            query.Append("  ( ");
            query.Append("  	select ag.IdAgreement ");
            query.Append("  	from StatusLead sl ");
            query.Append("  		inner join Agreement ag ");
            query.Append("  			on ag.IdStatusLead = sl.IdStatusLead ");
            query.Append("  	where sl.IdLead = le.IdLead ");
            query.Append("  	and ag.IdAgreementStatus not in (1,5,6) ");
            query.Append("  ) ");
            query.Append("  and pe.NrCNPJCPF in  ");
            query.Append("  ( ");
            query.Append("      select CPFCNPJ ");
            query.Append("      from CREDZ.dbo.PesquisaCliente pc");
            query.Append("          INNER JOIN credz.CobmaisEvent cv");
            query.Append("                  on cv.IdEvent = pc.idEvento");
            query.Append("      where agendamento is not null ");
            query.Append("          and agendamento between CONVERT(date, getdate() -1) and CONVERT(date, getdate() +2) ");
            query.Append("          AND event NOT IN ('Telefone Nao Atende','Discagem sem sucesso', 'Linha Muda', 'Queda de ligação- Com Cliente',  ");
            query.Append("  						 '408 - NAO ATENDE', 'CLIENTE RECUSA-SE A ATENDER','Terceiro Desligou','Atende e Desliga','Recado Caixa Postal', ");
            query.Append("  						 'Higienização de Dados','Nao Anotou Recado','Telefone Desligado','Recado Com Terceiros', 'Ligação Muda', ");
            query.Append("  						 'Ligação Muda/Cx Postal/Msg  Electrónica', 'Cliente desligou', 'Desconhece Cliente', 'Discagem Automática', ");
            query.Append("  						 'Ligação Cai/Cliente Desliga','Discagem sem sucesso','Queda de Ligação- Sem Contato') ");
            query.Append("  ) ");

            query.Append("  ORDER BY Score DESC, Age ASC, DebitBalance DESC ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Person> GetByMailSendNews(int idProductType, DateTime dtLead, int count)
        {
            var query = new StringBuilder();

            /*
            query.Append(" select distinct top 30000 p.*, Age  ");
            query.Append(" from Person p ");
            query.Append(" 	inner join Product pr ");
            query.Append(" 		on pr.IdPerson = p.IdPerson ");
            query.Append(" 	inner join Lead l ");
            query.Append(" 		on l.IdProduct = pr.IdProduct ");
            query.Append(" 		and l.dtinsert >= convert(date, getdate()-1)");
            //query.Append(" 		and l.age in (77,78)");
            query.Append(" 		and (l.age in (151, 152,181, 182,211,281,282,721,722))");
            query.Append(" 	inner join Email e ");
            query.Append(" 		on e.IdPerson = p.IdPerson ");
            query.Append(" 		and flBloqueado = 0 ");
            query.Append(" where not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.SendEmail se ");
            query.Append(" 	where dtInsert > CONVERT(Date, getdate()  ) ");
            query.Append(" 	and p.IdPerson = se.idperson ");
            query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.SendEmailUra su ");
            query.Append(" 	where dtInsert > CONVERT(Date, getdate() -2) ");
            query.Append(" 	and p.IdPerson = su.idperson ");
            query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.SendBrokenEmail sb ");
            query.Append(" 	where dtInsert > CONVERT(Date, getdate() -2) ");
            query.Append(" 	and p.IdPerson = sb.idperson ");
            query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.SMS sms ");
            query.Append(" 	where dtEnvio > CONVERT(Date, getdate() -5) ");
            query.Append(" 	and p.IdPerson = sms.idperson ");
            query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.sendrcs rcs ");
            query.Append(" 	where dtInsert > CONVERT(Date, getdate() -5) ");
            query.Append(" 	and p.IdPerson = rcs.idperson ");

            query.Append(" ) ");
            query.Append("  ORDER BY Age ASC");
            return Context.FromSqlRaw(query.ToString()).ToList();


            */
            /*--------------------------------------------------------*/

            query.Append(" select distinct TOP 10000 [pe].[IdPerson], [pe].[DsName], [pe].[DtBirth], ");
            query.Append("          [pe].[DtInsert], [pe].[DtUpdate], [pe].[MotherName],   ");
            query.Append("          [pe].[NrCNPJCPF], [pe].[NrRG],Age, DebitBalance, Score  ");
            query.Append(" FROM FIS.DBO.Person pe   ");
            query.Append("      INNER JOIN FIS.DBO.Product PR   ");
            query.Append("          ON PR.IdPerson = PE.IdPerson  ");
            query.Append(" 		  AND PR.IdProductType = 3  ");
            query.Append("      INNER join FIS.DBO.EMAIL EM  ");
            query.Append("          ON EM.IdPerson = PR.IdPerson  ");
            query.Append("      INNER join FIS.DBO.LEAD LE  ");
            query.Append("          ON LE.IdProduct = PR.IdProduct  ");
            query.Append(" 			and LE.DtInsert >= CONVERT(date, getdate()) ");
            query.Append(" 			AND LE.Age >= 78  ");
            query.Append("      LEFT join FIS.DBO.SCOREAI SA  ");
            query.Append("          ON SA.IdProduct = PR.IdProduct  ");
            query.Append(" WHERE (   ");
            query.Append("         NOT EXISTS(SELECT * FROM FIS.CREDZ.SENDEMAIL SE WHERE SE.IDPERSON = PR.IdPerson and se.dtinsert >= getdate() -8)  ");
            query.Append("         and not exists  ");
            query.Append("         (  ");
            query.Append("         select idlead  ");
            query.Append("         from Lead l  ");
            query.Append("         where l.DtInsert between DATEADD(day, -30, getdate())  and DATEADD(day, -1, getdate())  ");
            query.Append("         and le.IdProduct = l.IdProduct  ");
            query.Append(" 		) ");
            query.Append(" 	) ");
            query.Append("    or  ");
            query.Append(" 	( ");
            query.Append(" 		NOT EXISTS(SELECT * FROM FIS.CREDZ.SENDEMAIL SE WHERE SE.IDPERSON = PR.IdPerson and se.dtinsert >= getdate())  ");
            query.Append(" 		and LE.Age in (151,181,211,281,721) ");
            query.Append(" 	) ");
            query.Append(" ORDER BY Score DESC, Age ASC, DebitBalance DESC  ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Person> GetPersonSentedSMS()
        {
            var query = new StringBuilder();

            query.Append(" SELECT DISTINCT TOP 5000 P.*, LE.Age ");
            query.Append(" from CREDZ.SMS S ");
            query.Append(" 	INNER JOIN Person P WITH(NOLOCK) ");
            query.Append(" 		ON P.IdPerson = S.idPerson ");
            query.Append("	INNER JOIN Email e WITH(NOLOCK) ");
            query.Append("			on e.IdPerson = p.IdPerson ");
            query.Append(" 	INNER JOIN Product PR WITH(NOLOCK) ");
            query.Append(" 		ON PR.IdPerson = P.IdPerson ");
            query.Append(" 	INNER JOIN LEAD LE WITH(NOLOCK) ");
            query.Append(" 		ON LE.IdProduct = PR.IdProduct ");
            query.Append(" WHERE LE.DtInsert >= CONVERT(DATE, GETDATE()) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SendEmail SE WITH(NOLOCK) ");
            query.Append(" 	WHERE SE.idPerson = P.idPerson ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.DBO.Navigation N WITH(NOLOCK) ");
            query.Append(" 	WHERE N.CPF = P.NrCNPJCPF AND N.DTINSERT < GETDATE() -1");
            query.Append(" ) ");
            query.Append(" ORDER BY LE.Age DESC ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Person> GetPersonSendSMS(DateTime dtLead)
        {
            var query = new StringBuilder();



            query.Append(" SELECT DISTINCT TOP 1550 P.*, SAI.Score ");
            query.Append(" from Person P WITH(NOLOCK) ");
            query.Append("	INNER JOIN PHONE e WITH(NOLOCK) ");
            query.Append("			on e.IdPerson = p.IdPerson ");
            query.Append("			    AND idphonestatus = 1 ");
            query.Append(" 	INNER JOIN Product PR WITH(NOLOCK) ");
            query.Append(" 		ON PR.IdPerson = P.IdPerson ");
            query.Append(" 	INNER JOIN LEAD LE WITH(NOLOCK) ");
            query.Append(" 		ON LE.IdProduct = PR.IdProduct ");
            query.Append(" 	LEFT JOIN ScoreAI SAI WITH(NOLOCK) ");
            query.Append(" 		ON SAI.IdProduct = PR.IdProduct ");
            query.Append(" WHERE LE.DtInsert >= '").Append(dtLead.ToString("yyyy-MM-dd")).Append("'");
            query.Append("       AND LE.Age >= 78");
            //query.Append("       AND LE.DEBITBALANCE >= 1000");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            //query.Append(" 	FROM CREDZ.SendEmail SE WITH(NOLOCK) ");
            query.Append(" 	FROM Email SE WITH(NOLOCK) ");
            query.Append(" 	WHERE SE.idPerson = P.idPerson ");
            query.Append(" 	AND SE.DTINSERT >= GETDATE() - 30 ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM STATUSLEAD SL WITH(NOLOCK) ");
            query.Append(" 	INNER JOIN AGREEMENT AG ");
            query.Append(" 	    ON AG.IDSTATUSLEAD = SL.IDSTATUSLEAD ");
            query.Append(" 	WHERE SL.IDLEAD = LE.IDLEAD ");
            query.Append(" 	    AND AG.IDAGREEMENTSTATUS IN (1) ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SMS SE WITH(NOLOCK) ");
            query.Append(" 	WHERE SE.idPerson = P.idPerson ");
            query.Append("  AND SE.DTENVIO >= GETDATE() -8 ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" SELECT * ");
            query.Append(" FROM CREDZ.DBO.Navigation NAV ");
            query.Append(" WHERE NAV.CPF = P.NrCNPJCPF ");
            query.Append("     AND NAV.DtInsert >= GETDATE() - 8 ");
            query.Append(" ) ");
            query.Append(" and p.NrCNPJCPF not in ");
            query.Append(" ( ");
            query.Append("    select CPFCNPJ ");
            query.Append("    from CREDZ.dbo.PesquisaCliente pc ");
            query.Append("       inner join credz.cobmaisevent ce ");
            query.Append("          on ce.IdEvent = pc.idEvento ");
            query.Append("    where ultimocontato > DATEADD(day,- ce.lift, getdate())  ");
            query.Append(" ) ");
            query.Append(" ORDER BY SAI.Score DESC ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }


        public ICollection<Person> GetPersonSendRCSNews()
        {
            var query = new StringBuilder();

            query.Append(" select pe.*");
            query.Append(" from Lead a  WITH(NOLOCK) ");
            query.Append(" 	inner join Product p WITH(NOLOCK) ");
            query.Append(" 		on a.IdProduct = p.IdProduct ");
            query.Append(" 	inner join Person pe	WITH(NOLOCK)  ");
            query.Append(" 		on pe.IdPerson = p.IdPerson ");
            query.Append(" where a.DtInsert >= CONVERT(date, getdate()-1)  ");
            query.Append(" and a.age between 78 and 81");
            //query.Append(" and a.IdProduct not in   ");
            //query.Append(" (  ");
            //query.Append(" select IdProduct  ");
            //query.Append(" from Lead b  ");
            //query.Append(" where b.DtInsert between CONVERT(Date, getdate() -60) and CONVERT(date, getdate())  ");
            //query.Append(" and a.IdProduct = b.IdProduct  ");
            //query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select IdPerson ");
            query.Append(" 	from CREDZ.SendEmail se WITH(NOLOCK) ");
            query.Append(" 	where se.dtInsert >= CONVERT(Date, getdate()-1) ");
            query.Append(" 	and se.idPerson = pe.IdPerson ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SendRCS rcs WITH(NOLOCK) ");
            query.Append(" 	WHERE rcs.idPerson = Pe.idPerson ");
            query.Append(" 	AND rcs.DTINSERT >= GETDATE() - 8 ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.sms sms WITH(NOLOCK) ");
            query.Append(" 	WHERE sms.idPerson = Pe.idPerson ");
            query.Append(" 	AND sms.dtenvio >= GETDATE() - 8 ");
            query.Append(" ) ");
            //query.Append(" ORDER BY S.Score DESC ");


            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Person> GetPersonSendRCS(DateTime dtLead)
        {
            var query = new StringBuilder();
            query.Append(" SELECT DISTINCT  P.*, SAI.Score ");
            query.Append(" from Person P WITH(NOLOCK) ");
            query.Append("	INNER JOIN PHONE e WITH(NOLOCK) ");
            query.Append("			on e.IdPerson = p.IdPerson ");
            query.Append("			    AND idphonestatus = 1 ");
            query.Append(" 	INNER JOIN Product PR WITH(NOLOCK) ");
            query.Append(" 		ON PR.IdPerson = P.IdPerson ");
            query.Append(" 	INNER JOIN LEAD LE WITH(NOLOCK) ");
            query.Append(" 		ON LE.IdProduct = PR.IdProduct ");
            query.Append(" 	INNER JOIN ScoreAI SAI WITH(NOLOCK) ");
            query.Append(" 		ON SAI.IdProduct = PR.IdProduct ");
            query.Append(" WHERE LE.DtInsert >= '").Append(dtLead.ToString("yyyy-MM-dd")).Append("'");
            //query.Append("       AND LE.Age between 78 and 320");
            query.Append("       AND LE.debitBalance >= 100");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SendRCS rcs WITH(NOLOCK) ");
            query.Append(" 	WHERE rcs.idPerson = P.idPerson ");
            query.Append(" 	AND rcs.DTINSERT >= GETDATE() - 30 ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SendEmail SE WITH(NOLOCK) ");
            query.Append(" 	WHERE SE.idPerson = P.idPerson ");
            query.Append(" 	AND SE.DTINSERT >= GETDATE() - 15 ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM STATUSLEAD SL WITH(NOLOCK) ");
            query.Append(" 	INNER JOIN AGREEMENT AG ");
            query.Append(" 	    ON AG.IDSTATUSLEAD = SL.IDSTATUSLEAD ");
            query.Append(" 	WHERE SL.IDLEAD = LE.IDLEAD ");
            query.Append(" 	    AND AG.IDAGREEMENTSTATUS IN (1) ");
            query.Append(" ) ");
            query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" 	SELECT * ");
            query.Append(" 	FROM CREDZ.SMS SE WITH(NOLOCK) ");
            query.Append(" 	WHERE SE.idPerson = P.idPerson ");
            query.Append("  AND SE.DTENVIO >= GETDATE() - 8 ");
            query.Append(" ) ");
            /*query.Append(" AND NOT EXISTS ");
            query.Append(" ( ");
            query.Append(" SELECT * ");
            query.Append(" FROM CREDZ.DBO.Navigation NAV ");
            query.Append(" WHERE NAV.CPF = P.NrCNPJCPF ");
            query.Append(" ) ");
            query.Append(" ORDER BY SAI.Score DESC ");*/

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

    }
}
