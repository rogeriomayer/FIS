using FMC.CREDZ.API.Models;
using FMC.Generic;
using System.Linq;

namespace FMC.CREDZ.DAO.Persistence
{
    public class ProductDAO : AbstractRepositoryPersistence<Product>
    {
        public ProductDAO() : base("CNN_CREDZ")
        {
            
        }

        public Product GetByAccountIbi(string accountIbi)
        {
            return Context.Where(p => p.Account == accountIbi).FirstOrDefault();
        }
    }
}
