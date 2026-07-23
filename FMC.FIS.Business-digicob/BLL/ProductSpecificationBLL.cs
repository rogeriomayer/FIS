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
    public class ProductSpecificationBLL : BLL<ProductSpecification, ProductSpecificationDAO>
    {

        public ICollection<ProductSpecification> GetByProductType(int idProductType)
        {
            return persistence.GetByProductType(idProductType);
        }

        public ProductSpecification GetByLogo(int idProductType, int logo)
        {
            return persistence.GetByLogo(idProductType, logo);
        }
    }

}
