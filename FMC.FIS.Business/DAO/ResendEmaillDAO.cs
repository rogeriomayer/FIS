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
    public class ResendEmaillDAO : AbstractRepositoryPersistence<ResendEmail>
    {
        public ResendEmaillDAO() : base("CNN_FIS") { }

        public ICollection<long> GetEmail15Days()
        {
            return Context.Where(p => p.dtInsert > DateTime.Today.AddDays(-15)).Select(p => p.idPerson).Take(15000).ToList();
        }
    }
}
