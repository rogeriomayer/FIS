using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class ParameterDAO : AbstractRepositoryPersistence<Parameter>
    {
        public ParameterDAO() : base("CNN_FIS") { }

        public ICollection<Parameter> GetByProductType(byte productType)
        {
            return Context.Where(p => p.IdProductType == productType).ToList();
        }

        public Parameter GetBykey(int productType, string key)
        {
            return Context.Where(p => p.Key == key && p.IdProductType == productType).FirstOrDefault();
        }
    }
}