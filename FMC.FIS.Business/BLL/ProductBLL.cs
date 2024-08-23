using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

using FMC.FIS.Business.Models.FIS;
using FMC.FIS.Business.DAO;

namespace FMC.FIS.Business.BLL
{
    public class ProductBLL : BLL<Product, ProductDAO>
    {
        public ICollection<Product> GetProductByProductType(int productType)
        {
            return persistence.GetProductByProductType(productType);
        }
        public ICollection<Product> GetProductQuebraInterna()
        {
            return persistence.GetProductQuebraInterna();
        }

        public ICollection<Product> GetProductQuebraExterna()
        {
            return persistence.GetProductQuebraExterna();
        }

        public ICollection<Product> GetDebitBalance100()
        {
            return persistence.GetDebitBalance100();
        }

        public ICollection<Product> GetProductsURA()
        {
            return persistence.GetProductsURA();
        }

    }

}
