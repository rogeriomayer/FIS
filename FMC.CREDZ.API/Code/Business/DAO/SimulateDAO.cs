using FMC.CREDZ.API.Models;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.CREDZ.DAO.Persistence
{
    public class SimulateDAO : AbstractRepositoryPersistence<Simulate>
    {
        public SimulateDAO() : base("CNN_CREDZ")
        {

        }
        public IList<Simulate> GetSimulatesNaoProcessada()
        {
            DateTime dtInicio = DateTime.Today.AddHours(-3);
            DateTime dtFim = DateTime.Now.AddMinutes(-20);
            IQueryable<Simulate> query = Context.AsQueryable();
            query = query.Where(p => p.DtInsert >= dtInicio && p.DtInsert <= dtFim);
            query = query.Where(p => (!p.FlProcess.HasValue) || (!p.FlProcess.Value));
            return query.ToList();
        }

        public ICollection<Simulate> GetByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return Context.Where(p => EF.Property<DateTime>(p, "DtInsert").Date >= dateInitial.Date && EF.Property<DateTime>(p, "DtInsert").Date <= dateEnd.Date).Include(p => p.Product.Navigation).ToList();
        }
    }
}
