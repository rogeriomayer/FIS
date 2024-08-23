using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class PromisseTypeDAO : AbstractRepositoryPersistence<PromisseType>
    {
        public PromisseTypeDAO() : base("CNN_FIS") { }

    }
}
