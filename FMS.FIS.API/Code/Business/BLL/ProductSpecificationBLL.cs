using FMC.FIS.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FMC.FIS.Model;
using FMC.Generic;
using FMC.FIS.API.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using FMC.FIS.API.Models.FIS;
using FMC.FIS.API.Code.Business.DAO;

namespace FMC.FIS.BLL
{
    public class ProductSpecificationBLL : BLL<ProductSpecification, ProductSpecificationDAO>
    {

        public ICollection<ProductSpecification> GetByProductType(int idProductType)
        {
            return persistence.GetByProductType(idProductType);
        }
    }

}
