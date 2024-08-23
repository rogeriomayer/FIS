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
    public class SendBrokenEmailDAO : AbstractRepositoryPersistence<SendBrokenEmail>
    {
        public SendBrokenEmailDAO() : base("CNN_FIS") { }

        public ICollection<long> GetEmail15Days()
        {
            return Context.Where(p => p.dtInsert > DateTime.Today.AddDays(-15)).Select(p => p.idPerson).Take(15000).ToList();
        }

        public ICollection<SendBrokenEmail> GetEmails(long idProduct, DateTime dtSended, string email)
        {
            return Context.Where(p => p.IdProduct == idProduct && p.dtInsert >= dtSended && p.email == email).ToList();
        }
    }
}
