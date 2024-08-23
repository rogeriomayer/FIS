using FMC.FIS.API.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class StatusDAO : AbstractRepositoryPersistence<Status>
    {
        public StatusDAO() : base("CNN_FIS") { }

        public ICollection<Status> GetByProductType(int idProdutctType)
        {
            return Context.Where(p => p.IdProductType == idProdutctType).ToList();
        }
    }
}
