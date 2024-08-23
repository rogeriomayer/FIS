using FMC.CREDZ.API.Models;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.CREDZ.DAO.Persistence
{
    public class NavigationDAO : AbstractRepositoryPersistence<Navigation>
    {
        public NavigationDAO() : base("CNN_CREDZ")
        {
        }

        public ICollection<Navigation> GetWsNavigationByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return Context.Where(p => EF.Property<DateTime>(p, "DtInsert").Date >= dateInitial.Date && EF.Property<DateTime>(p, "DtInsert").Date <= dateEnd.Date).ToList();
        }

        public Navigation UpdateNoContext(Navigation Navigation, bool creationProxy = true)
        {
            NavigationDAO persistence = new NavigationDAO();
            return persistence.UpdateNoContext(Navigation);
        }
    }
}
