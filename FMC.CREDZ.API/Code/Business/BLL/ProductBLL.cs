using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class ProductBLL : BLL<Product, ProductDAO>
    {
        public Product GetByAccountIbi(string accountIbi)
        {
            return persistence.GetByAccountIbi(accountIbi);
        }
    }
}
