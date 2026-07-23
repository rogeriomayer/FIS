using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.Business.BLL
{
    public class ParameterBLL : BLL<Parameter, ParameterDAO>
    {
        public ICollection<Parameter> GetByProductType(byte productType)
        {
            return persistence.GetByProductType(productType);
        }

        public Parameter GetBykey(int productType, string key)
        {
            return persistence.GetBykey(productType, key);
        }
    }
}
