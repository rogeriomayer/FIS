using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class DebtorCommentsPersistencia : AbstractBLLPersistencia<DB_FMCEntities, tb_debtor_comments>
    {
        public short GetMaxNrComments(int idDebtor)
        {
            IQueryable<tb_debtor_comments> query = Context.tb_debtor_comments.AsQueryable();

            query = query.Where(p => p.id_debtor == idDebtor);

            IList<tb_debtor_comments> comments = query.ToList();

            if (comments.Any())
                return comments.Max(p => p.nr_comment);
            else
                return Convert.ToInt16(0);
        }
    }
}
