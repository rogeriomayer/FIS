using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.FIS.Business.DAO
{
    public class PaymentDAO : AbstractRepositoryPersistence<Payment>
    {
        public PaymentDAO() : base("CNN_FIS") { }

        public IList<Payment> GetPayments(long idAgreement)
        {
            return Context.Where(p => p.AgreementParcel.IdAgreement == idAgreement).ToList();
        }
    }
}