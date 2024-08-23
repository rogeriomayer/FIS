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
    public class EmailRememberDAO : AbstractRepositoryPersistence<EmailRemember>
    {
        public EmailRememberDAO() : base("CNN_FIS") { }

        public ICollection<EmailRemember> GetEmailRemember(long idAgreementParcel)
        {
            return Context.Where(p => p.IdAgreementParcel == idAgreementParcel).ToList();
        }
    }
}
