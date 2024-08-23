using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class ParameterBLL : BLL<Parameter, ParameterDAO>
    {
        public ICollection<Parameter> GetByProductType(byte productType)
        {
            return persistence.GetByProductType(productType);
        }
    }
}
