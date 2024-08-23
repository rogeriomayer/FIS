using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class DownloadCNABBLL : BLL<DownloadCNAB, DownloadCNABDAO>
    {

        public IList<DownloadCNAB> GetByIdRemessa(long idRemessa)
        {
            return persistence.GetByIdRemessa(idRemessa);
        }
    }
}
