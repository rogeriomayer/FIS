using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class ProductSpecificationDAO : AbstractRepositoryPersistence<ProductSpecification>
    {
        public ProductSpecificationDAO() : base("CNN_FIS") { }

        public ICollection<ProductSpecification> GetByProductType(int idProductType)
        {
            this.ChangeTracker.LazyLoadingEnabled = false;
            return Context.Where(p => p.IdProductType == idProductType).ToList();
        }

    }
}
