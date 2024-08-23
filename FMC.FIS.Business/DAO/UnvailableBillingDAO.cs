using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class UnvailableBillingDAO : AbstractRepositoryPersistence<UnvailableBilling>
    {
        public UnvailableBillingDAO() : base("CNN_FIS") { }

        public UnvailableBilling GetByProduct(string dsProduct)
        {
            return Context.Where(p => p.DsProduct == dsProduct).FirstOrDefault();
        }
    }
}