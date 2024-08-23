using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class PromisseTypeDAO : AbstractRepositoryPersistence<PromisseType>
    {
        public PromisseTypeDAO() : base("CNN_FIS") { }

    }
}
