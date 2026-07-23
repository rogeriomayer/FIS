using FMC.FIS.Business.Models;
using FMC.FIS.Model;
using FMC.Generic;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
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
