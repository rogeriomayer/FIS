using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class SendRcsDAO : AbstractRepositoryPersistence<SendRCS>
    {
        public SendRcsDAO() : base("CNN_FIS") { }


        public IList<SendRCS> GetSendToday()
        {
            var today = DateTime.Today;

            return Context.Where(p => p.DtInsert >= today).ToList();
        }
    }

}
