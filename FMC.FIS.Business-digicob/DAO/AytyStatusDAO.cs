using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class AytyStatusDAO : AbstractRepositoryPersistence<AytyStatus>
    {
        public AytyStatusDAO() : base("CNN_FIS") { }

        public ICollection<AytyStatus> GetActive()
        {
            return Context.Where(p => !p.DtInactive.HasValue).ToList();
        }
    }
}
