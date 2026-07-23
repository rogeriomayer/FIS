using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.Business.BLL
{
    public class DownloadCNABBLL : BLL<DownloadCNAB, DownloadCNABDAO>
    {

        public IList<DownloadCNAB> GetByIdRemessa(long idRemessa)
        {
            return persistence.GetByIdRemessa(idRemessa);
        } 
        //
    }
}
