using FMC.FIS.API.Models;
using FMC.FIS.Model;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class DownloadCNABDAO : AbstractRepositoryPersistence<DownloadCNAB>
    {
        public DownloadCNABDAO() : base("CNN_FIS") { }

        public IList<DownloadCNAB> GetByIdRemessa(long idRemessa)
        {
            return Context.Where(p => p.IdRemessa == idRemessa).ToList();
        }
    }
}
