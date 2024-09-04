using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class ProductDAO : AbstractRepositoryPersistence<Product>
    {
        public ProductDAO() : base("CNN_FIS") { }

        public ICollection<Product> GetProductByProductType(int productType)
        {
            //return Context.Where(p => p.IdProductType == productType).OrderByDescending(p => p.IdProduct).ToList();
            var query = new StringBuilder();
            query.Append(" select distinct a.* ");
            query.Append(" from Product a ");
            query.Append(" 	inner join Address b ");
            query.Append(" 		on a.IdPerson = b.IdPerson ");
            query.Append("  inner join Lead l ");
            query.Append("      on a.idProduct  = l.idProduct ");
            query.Append(" where a.IdProductType = ").Append(productType);
            query.Append(" and not exists  ");
            query.Append(" ( ");
            query.Append(" 	select * ");
            query.Append(" 	from ScoreAI c ");
            query.Append(" 	where c.idproduct = a.IdProduct ");
            query.Append(" ) ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Product> GetProductQuebraInterna()
        {
            var query = new StringBuilder();
            query.Append(" select distinct pr.IdProduct,pr.IdPerson,pr.IdProductType,pr.DsProduct ");
            query.Append("      ,pr.IdProductSpecification,pr.DtInsert,pr.FlCobranca ");
            query.Append(" from Person pe  ");
            query.Append(" 	inner join Product pr  ");
            query.Append(" 		on pr.IdPerson = pe.IdPerson  ");
            query.Append(" 		   and IdProductType = 3  ");
            query.Append(" 	inner join Lead le  ");
            query.Append(" 		on le.IdProduct = pr.IdProduct  ");
            query.Append(" 	inner join StatusLead sl  ");
            query.Append(" 		on sl.IdLead = le.IdLead  ");
            query.Append(" 	inner join Agreement ag  ");
            query.Append(" 		on ag.IdStatusLead = sl.IdStatusLead  ");
            query.Append(" 		and ag.CdParcelPlan = ''  ");
            query.Append(" 		and ag.DtEntrace < GETDATE() -10 ");
            query.Append(" where exists (select * from Lead l where l.IdProduct = pr.IdProduct and l.DtInsert >= CONVERT(date, getdate()))  ");
            query.Append(" 		and not exists (select * from CREDZ.SendBrokenEmail sb where sb.IdProduct = pr.IdProduct and dtinsert >= getdate() -15)  ");
            query.Append("        and not exists (select * from CREDZ.SendEmail se where se.IdProduct = pr.IdProduct and dtinsert >= getdate() -15) ");


            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Product> GetProductQuebraExterna()
        {
            var query = new StringBuilder();
            query.Append(" select distinct top 10000 pr.IdProduct,pr.IdPerson,pr.IdProductType,pr.DsProduct ");
            query.Append("      ,pr.IdProductSpecification,pr.DtInsert,pr.FlCobranca ");
            query.Append(" from Person pe  ");
            query.Append(" 	inner join Product pr  ");
            query.Append(" 		on pr.IdPerson = pe.IdPerson  ");
            query.Append(" 		   and IdProductType = 3  ");
            query.Append(" 	inner join Lead le  ");
            query.Append(" 		on le.IdProduct = pr.IdProduct  ");
            query.Append("	inner join  ");
            query.Append("	( ");
            query.Append("		select coluna4, coluna3 ");
            query.Append("		from CREDZ.Temp te  ");
            query.Append("		where te.coluna4 like '%quebra%' ");
            query.Append("	) as import ");
            query.Append("		on import.coluna3 = pr.DsProduct collate Latin1_General_CI_AS  ");
            query.Append(" where le.DtInsert >= CONVERT(Date, getdate()) ");
            query.Append(" 		and not exists (select * from CREDZ.SendBrokenEmail sb where sb.IdProduct = pr.IdProduct and dtinsert >= getdate() -15)  ");
            query.Append("        and not exists (select * from CREDZ.SendEmail se where se.IdProduct = pr.IdProduct and dtinsert >= getdate() -15) ");


            return Context.FromSqlRaw(query.ToString()).ToList();
        }

        public ICollection<Product> GetDebitBalance100()
        {
            var query = new StringBuilder();

            query.Append(" select distinct pr.*  ");
            query.Append(" from Person pe ");
            query.Append(" 	inner join product pr ");
            query.Append(" 		on pe.IdPerson = pr.IdPerson ");
            query.Append(" 	inner join email em ");
            query.Append(" 		on pe.IdPerson = em.IdPerson ");
            query.Append(" 	inner join Lead l ");
            query.Append(" 		on l.IdProduct = pr.IdProduct ");
            query.Append(" 		and l.DtInsert >= CONVERT(Date, getdate()) ");
            query.Append(" 	inner join CREDZ.dbo.Navigation nav ");
            query.Append(" 		on nav.CPF = pe.NrCNPJCPF ");
            query.Append(" 		and nav.DtInsert between '2024-02-01' and '2024-03-12' ");
            query.Append(" where not exists ");
            query.Append(" (	 ");
            query.Append(" select * ");
            query.Append(" from CREDZ.dbo.Product prd ");
            query.Append(" where prd.IdNavigation = nav.IdNavigation ");
            query.Append(" ) ");
            query.Append(" and DtBirth is not null ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }


        public ICollection<Product> GetProductsURA()
        {
            var query = new StringBuilder();


            query.Append(" select distinct pr.*, Age ");
            query.Append(" from Product pr ");
            query.Append(" 	inner join Person pe ");
            query.Append(" 		on pe.IdPerson = pr.IdPerson ");
            query.Append(" 	inner join CREDZ.dbo.RetornoUra ru ");
            query.Append(" 		on ru.cpf = pe.NrCNPJCPF ");
            query.Append(" 		and IdProductType = 3 ");
            query.Append(" 	inner join Lead l ");
            query.Append(" 		on l.IdProduct = pr.IdProduct ");
            query.Append(" 		and l.DtInsert >= CONVERT(Date, getdate()-1) ");
            query.Append(" where  ru.dtLigacao >= CONVERT(Date, getdate() -3) ");
            query.Append(" and pr.IdProduct not in (select IdProduct from CREDZ.SendEmailUra seu where seu.dtInsert > GETDATE() -2 and seu.IdProduct = pr.IdProduct ) ");
            query.Append(" and pr.IdProduct not in (select IdProduct from CREDZ.SendRCS sr where sr.dtInsert > GETDATE() -2 and sr.IdProduct = pr.IdProduct) ");
            query.Append(" and pe.NrCNPJCPF not in (select cpf from CREDZ.dbo.Navigation where DtInsert >= GETDATE() -2) ");
            query.Append(" order by Age desc ");
            return Context.FromSqlRaw(query.ToString()).ToList();


            query.Append(" select distinct c.[IdProduct] ,c.[IdPerson],c.[IdProductType],c.[DsProduct],c.[IdProductSpecification],c.[DtInsert] ");
            query.Append(" from CREDZ.dbo.RetornoUra a ");
            query.Append(" 	inner join Person b ");
            query.Append(" 		on a.cpf = b.NrCNPJCPF ");
            query.Append(" 	inner join Product c ");
            query.Append(" 		on c.IdPerson = b.IdPerson ");
            query.Append(" 	inner join Lead d ");
            query.Append(" 		on c.IdProduct = d.IdProduct ");
            query.Append(" 		and d.Age > 78 ");
            query.Append(" 		and d.DtInsert > convert(date, GETDATE() )");
            query.Append(" 	left join Email f ");
            query.Append(" 		on f.IdPerson = b.IdPerson ");
            query.Append(" where a.flEnviado = 0 ");
            query.Append(" and a.dtligacao > getdate() - 8 ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.dbo.Navigation nav ");
            query.Append(" 	    inner join credz.dbo.Product prd ");
            query.Append(" 	        on nav.idnavigation = prd.idnavigation ");
            query.Append(" 	    inner join credz.dbo.simulate sim ");
            query.Append(" 	        on sim.idproduct = prd.idproduct ");
            query.Append(" 	where nav.CPF = b.NrCNPJCPF ");
            query.Append(" 	and nav.DtInsert between a.dtligacao and DATEADD(day,3, a.dtligacao)  ");
            query.Append(" ) ");
            query.Append(" and not exists ");
            query.Append(" ( ");
            query.Append(" 	select 1 ");
            query.Append(" 	from CREDZ.SendEmailUra seu ");
            query.Append(" 	where DtInsert >= GETDATE() -8 ");
            query.Append(" 	and seu.idproduct = c.IdProduct ");
            query.Append(" ) ");

            query.Append(" order by c.IdPerson ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }
    }
}
