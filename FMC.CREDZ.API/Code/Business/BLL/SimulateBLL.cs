using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;
using System;
using System.Collections.Generic;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class SimulateBLL : BLL<Simulate, SimulateDAO>
    {

        public IList<Simulate> GetSimulatesNaoProcessada()
        {
            var SimulatePersistence = new SimulateDAO();
            return SimulatePersistence.GetSimulatesNaoProcessada();
        }

        public ICollection<Simulate> GetByPeriod(DateTime dateInitial, DateTime dateEnd)
        {
            return persistence.GetByPeriod(dateInitial, dateEnd);
        }
    }
}
