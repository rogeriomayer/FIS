using FMC.CREDZ.API.Models;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.CREDZ.DAO.Persistence
{
    public class AgreementDAO : AbstractRepositoryPersistence<Agreement>
    {

        public AgreementDAO() : base("CNN_CREDZ")
        {
        }


        public ICollection<Agreement> GetAgreementToday()
        {
            return Context.Where(p => EF.Property<DateTime>(p, "DtInsert").Date == DateTime.Today).ToList();
        }

        public ICollection<Agreement> GetByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return Context.Where(p => EF.Property<DateTime>(p, "DtInsert").Date >= dateInitial.Date && EF.Property<DateTime>(p, "DtInsert").Date <= dateEnd.Date).Include(p => p.Product.Navigation).ToList();
        }


        public ICollection<Agreement> GetAgreementTodayByCard(string nrCard)
        {
            var dtIni = DateTime.Today.AddDays(-3);

            return Context.Where(p => EF.Property<DateTime>(p, "DtInsert").Date >= dtIni && p.Product.Account == nrCard && p.Product.ProductType == "P2").ToList();

        }

        public Agreement GetLastAgreement(string nrAccount)
        {
            return Context.Where(p => p.Product.Account == nrAccount).OrderByDescending(p => p.IdAgreement).FirstOrDefault();
        }

    }
}


