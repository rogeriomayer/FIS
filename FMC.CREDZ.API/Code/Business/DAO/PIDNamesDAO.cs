using FMC.CREDZ.API.Models;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.CREDZ.DAO.Persistence
{
    public class PIDNamesDAO : AbstractRepositoryPersistence<PIDNames>
    {

        public PIDNamesDAO() : base("CNN_CREDZ") { }

        public IList<string> GetNames(string name, int count)
        {
            return Context.Where(p => p.DsName != name).Select(p => p.DsName).ToList();
        }
    }
}
