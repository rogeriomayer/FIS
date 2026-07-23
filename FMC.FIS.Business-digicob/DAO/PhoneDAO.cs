using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class PhoneDAO : AbstractRepositoryPersistence<Phone>
    {
        public PhoneDAO() : base("CNN_FIS") { }

        public IList<Phone> GetBySendTelegram()
        {
            StringBuilder query = new StringBuilder();

            query.Append("Select p.*");
            query.Append(" from fis.dbo.Lead a	(nolock) ");
            query.Append(" 	inner join fis.dbo.Product b (nolock) ");
            query.Append(" 		on a.IdProduct = b.IdProduct and b.IdProductType = 3 ");
            query.Append(" 	INNER JOIN FIS.DBO.Person C (nolock) ");
            query.Append(" 		ON C.IdPerson = B.IdPerson ");
            //query.Append(" 	inner join fis.dbo.ProductSpecification d (nolock) ");
            //query.Append(" 		on d.IdProductSpecification = b.IdProductSpecification ");
            //query.Append(" 	inner join discount dis ");
            //query.Append(" 		on a.age between MinAge and MaxAge and dis.IdProductType = 3 and dis.MaxParcel = 1 ");
            //query.Append(" 	join credz.Cliente cli (nolock) on cli.cpfCnpj = c.NrCNPJCPF COLLATE Latin1_General_CI_AS ");
            //query.Append(" 	outer apply (select top(1) t.telefone  ");
            //query.Append(" 				   from credz.Telefone (nolock) t  ");
            //query.Append(" 				  where t.idCliente = cli.id ");
            //query.Append(" 				    and substring(cast(t.telefone as varchar(15)),3,1) > 6 ");
            //query.Append(" 				  order by  t.contato desc, t.score asc) t ");
            query.Append(" 	inner join fis.dbo.Phone p ");
            query.Append(" 		on p.IdPerson = c.IdPerson ");
            //query.Append(" 		and p.NrPhone = t.telefone ");
            query.Append(" where a.DtInsert >= CONVERT(date, getdate()-1) ");
            /*query.Append(" 	and  ");
            query.Append(" 	( ");
            query.Append(" 		(a.Age between 78 and 150  and a.DebitBalance > 2000) ");
            query.Append(" 			or  ");
            query.Append(" 		(a.Age between 281 and 580 and a.DebitBalance > 2000) ");
            query.Append(" 	) ");
            //query.Append(" 	and not exists ");
            //query.Append(" 	( ");
            //query.Append(" 		select * ");
            //query.Append(" 		from CREDZ.SMS sms ");
            //query.Append(" 		where sms.idPerson = c.IdPerson ");
           // query.Append(" 	) ");
            query.Append(" 	and not exists ");
            query.Append(" 	( ");
            query.Append(" 		select * ");
            query.Append(" 		from CREDZ.SendEmail em ");
            query.Append(" 		where em.idPerson = c.IdPerson ");
            query.Append(" 	) ");
            query.Append(" 	and not exists ");
            query.Append(" 	( ");
            query.Append(" 	select * ");
            query.Append(" 		from CREDZ.SendWhatsapp wp ");
            query.Append(" 		where wp.idPerson = c.IdPerson ");
            query.Append(" 	) ");
            query.Append(" 	and not exists ");
            query.Append(" 	( ");
            query.Append(" 	select * ");
            query.Append(" 	from CREDZ.sendTelegram st ");
            query.Append(" 	where st.idperson = c.IdPerson");
            query.Append(" 	) ");
            query.Append(" and (telefone is not null or len(telefone) > 10) ");*/
            query.Append(" and (p.IdPhoneStatus = 1) ");

            return Context.FromSqlRaw(query.ToString()).ToList();
        }
    }
}
