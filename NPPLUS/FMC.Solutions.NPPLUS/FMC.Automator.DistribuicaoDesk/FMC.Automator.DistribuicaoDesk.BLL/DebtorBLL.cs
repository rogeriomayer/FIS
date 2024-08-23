using FMC.Automator.DistribuicaoDesk.DAO.Model;
using FMC.Automator.DistribuicaoDesk.DAO.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.Automator.DistribuicaoDesk.BLL
{
    public class DebtorBLL
    {
        DebtorPersistencia debtorPersistencia = new DebtorPersistencia();

        public TB_DEBTOR GetById(int idDebtor)
        {
            return debtorPersistencia.GetById(idDebtor);
        }
        public IList<int> GetListDebtorUpdate()
        {
            return debtorPersistencia.GetListDebtorUpdate();
        }

        public string GetLastDeskInsert(string programLetter, IList<string> programs, IList<string> desks)
        {
            return debtorPersistencia.GetLastDeskInsert(programLetter, programs, desks);
        }

        public void Update(TB_DEBTOR debtor)
        {
            debtorPersistencia.Update(debtor);
        }
    }
}
