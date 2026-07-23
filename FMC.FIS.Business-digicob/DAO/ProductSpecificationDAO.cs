using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class ProductSpecificationDAO : AbstractRepositoryPersistence<ProductSpecification>
    {
        public ProductSpecificationDAO() : base("CNN_FIS") { }

        public ICollection<ProductSpecification> GetByProductType(int idProductType)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            var query = new StringBuilder();
            query.Append(" select COUNT(*), ps.* ");
            query.Append(" from ProductSpecification ps ");
            query.Append(" 	inner join Product p ");
            query.Append(" 		on ps.IdProductSpecification = p.IdProductSpecification ");
            query.Append(" where p.IdProductType = ").Append(idProductType);
            query.Append(" group by ps.IdProductSpecification, ps.IdProductType, ps.Logo, ps.Description, ps.UrlImage ");
            query.Append(" order by 1 desc ");

            return Context.FromSqlRaw(query.ToString()).ToList();
            //return Context.Where(p => p.IdProductType == idProductType).ToList();
        }

        public ProductSpecification GetByLogo(int idProductType, int logo)
        {
            return Context.Where(p => p.IdProductType == idProductType && p.Logo == logo).FirstOrDefault();
        }

    }
}
