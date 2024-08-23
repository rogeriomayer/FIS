using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class DebtorDeskPersistencia : AbstractBLLPersistencia<DB_FMCEntities, tb_debtor_desks>
    {
        public void Delete(int idDebtor)
        {
            IList<tb_debtor_desks> listDebtorDesks = Context.tb_debtor_desks.Where(p => p.id_debtor == idDebtor).ToList();
            foreach (var debtorDesk in listDebtorDesks)
                Delete(debtorDesk);
        }

        public Boolean GetDeskAtiva(string desk)
        {
            StringBuilder sql = new StringBuilder();

            sql.Append("SELECT A.FL_ACTIVE ");
            sql.Append("FROM TB_USER_GROUP_X_DESK A WITH(NOLOCK) ");
            sql.Append("INNER JOIN TB_USER_GROUP B ON A.ID_USER_GROUP = B.ID_USER_GROUP ");
            sql.Append("INNER JOIN TB_DESK C ON A.CD_DESK = C.CD_DESK ");
            sql.Append("WHERE C.cd_desk = '" + desk + "' ");
            sql.Append("ORDER BY A.FL_ACTIVE DESC");

            return Context.ExecuteStoreQuery<Boolean>(sql.ToString()).FirstOrDefault();
        }

        public int MaxNrDesk(int idDebtor)
        {
            var debtorDesk = Context.tb_debtor_desks
                                    .Where(p => p.id_debtor == idDebtor)
                                    .OrderByDescending(p => p.nr_desk)
                                    .FirstOrDefault();
            if (debtorDesk != null)
                return debtorDesk.nr_desk + 1;
            else
                return 1;

        }
    }
}
