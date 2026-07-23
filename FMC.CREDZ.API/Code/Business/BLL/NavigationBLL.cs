using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;
using System;
using System.Collections.Generic;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class NavigationBLL : BLL<Navigation, NavigationDAO>
    {
        public ICollection<Navigation> GetWsNavigationByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return persistence.GetWsNavigationByPeriod(dateInitial, dateEnd);
        }

        public Navigation GetNavigation(string cpf, string cdFrom, string dsOrigem, DateTime dtInsert)
        {
            return persistence.GetNavigation(cpf, cdFrom, dsOrigem, dtInsert);
        }

    }
}
