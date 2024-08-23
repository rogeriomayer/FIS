using FMC.FIS.API.Models;
using FMC.FIS.Model;
using FMC.Generic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.API.Code.Business.DAO
{
    public class UploadCNABDAO : AbstractRepositoryPersistence<UploadCNAB>
    {
        public UploadCNABDAO() : base("CNN_FIS") { }

        public IList<UploadCNAB> GetLasts(int days)
        {
            DateTime dtQuery = DateTime.Today.AddDays(-days);
            return Context.Where(p => p.DtUpload >= dtQuery).ToList();
        }
    }
}
