using FMC.FIS.Business.DAO;
using FMC.FIS.Business.Models;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.Business.BLL
{
    public class UploadCNABBLL : BLL<UploadCNAB, UploadCNABDAO>
    {

        public IList<UploadCNAB> GetLasts(int days)
        {
            return persistence.GetLasts(days);
        }
    }
}
