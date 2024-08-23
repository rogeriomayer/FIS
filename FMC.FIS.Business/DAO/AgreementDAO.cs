using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class AgreementDAO : AbstractRepositoryPersistence<Agreement>
    {
        public AgreementDAO() : base("CNN_FIS") { }

        public ICollection<Agreement> GetRemember(DateTime dtIni, DateTime dtFim)
        {
            var query = new StringBuilder();

            query.Append("select a.* ");
            query.Append("from agreement a ");
            query.Append("where CdParcelPlan = '' ");
            query.Append("and cdagreement in  ");
            query.Append("( ");
            query.Append("'4142698186') ");


            return Context.FromSqlRaw(query.ToString()).ToList();


            query.Append(" SELECT DISTINCT [A].[IdAgreement] ");
            query.Append("       ,[A].[IdStatusLead] ");
            query.Append("       ,[A].[VlEntrace] ");
            query.Append("       ,[A].[DtEntrace] ");
            query.Append("       ,[A].[PcDiscount] ");
            query.Append("       ,[A].[QtParcel] ");
            query.Append("       ,[A].[VlParcel] ");
            query.Append("       ,[A].[VlAgreement] ");
            query.Append("       ,[A].[CdPaymentOption] ");
            query.Append("       ,[A].[CdParcelPlan] ");
            query.Append("       ,[A].[CdAgreement] ");
            query.Append("       ,[A].[IdAgreementStatus] ");
            query.Append("       ,[A].[DtInsert] ");
            query.Append(" FROM [FIS].[dbo].[Agreement] A ");
            query.Append(" 		INNER JOIN [FIS].[dbo].[AgreementParcel] AP ");
            query.Append(" 			ON [A].[IdAgreement] = [AP].[IdAgreement] ");
            //query.Append("      LEFT JOIN [FIS].[DBO].[PAYMENT] PAY ");
            //query.Append(" 			ON [AP].[IdAgreementPARCEL] = [PAY].[IdAgreementPARCEL] ");
            query.Append("  WHERE [A].[IdAgreementStatus] not in (2,5)");
            query.Append("  and [A].[cdparcelplan]  in ('','API CREDZ')");
            //query.Append("  AND [PAY].[IdPAYMENT] IS NULL ");
            query.Append("    AND   [AP].[DtParcel] BETWEEN '").Append(dtIni.ToString("yyyy-MM-dd")).Append("' AND '").Append(dtFim.ToString("yyyy-MM-dd")).Append(" 23:59'");


            return Context.FromSqlRaw(query.ToString()).ToList();

        }
    }
}