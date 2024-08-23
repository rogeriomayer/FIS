using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class PauseDAO : AbstractRepositoryPersistence<Pause>
    {
        public PauseDAO() : base("CNN_FIS") { }

        public ICollection<Pause> GetActive()
        {
            return Context.Where(p => !p.DtInactive.HasValue).ToList();
        }
    }
}
