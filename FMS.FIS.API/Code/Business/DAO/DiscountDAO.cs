using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class DiscountDAO : AbstractRepositoryPersistence<Discount>
    {
        public DiscountDAO() : base("CNN_FIS") { }

        public ICollection<Discount> GetByProductType(int idProdutctType)
        {
            return Context.Where(p => p.IdProductType == idProdutctType).ToList();
        }

        public Discount GetDiscount(int idProdutctType, int age, int parcel)
        {
            return Context.Where(p => p.IdProductType == idProdutctType
                                && (age >= p.MinAge && age <= p.MaxAge)
                                && (parcel >= p.MinParcel && parcel <= p.MaxParcel)).FirstOrDefault();
        }
    }

}
