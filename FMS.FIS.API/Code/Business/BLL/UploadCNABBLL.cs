using FMC.FIS.API.Code.Business.DAO;
using FMC.FIS.API.Models;
using FMC.Generic;
using System.Collections.Generic;

namespace FMC.FIS.API.Code.Business.BLL
{
    public class UploadCNABBLL : BLL<UploadCNAB, UploadCNABDAO>
    {

        public IList<UploadCNAB> GetLasts(int days)
        {
            return persistence.GetLasts(days);
        }
    }
}
