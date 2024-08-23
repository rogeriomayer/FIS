using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class ParameterDAO : AbstractRepositoryPersistence<Parameter>
    {
        public ParameterDAO() : base("CNN_FIS") { }

        public ICollection<Parameter> GetByProductType(byte productType)
        {
            return Context.Where(p => p.IdProductType == productType).ToList();
        }
    }
}