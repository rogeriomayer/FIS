using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class AmexImportReservedPersistencia // : AbstractBLLPersistencia<DB_FMCEntities, TB_AMEX_IMPORT_RESERVED>
    {
        /*public TB_AMEX_IMPORT_RESERVED GetById(int idReserved)
        {
            return Context.TB_AMEX_IMPORT_RESERVED.FirstOrDefault(p => p.ID_RESERVE == idReserved);
        }

        public IList<TB_AMEX_IMPORT_RESERVED> GetAmexImportReserved()
        {
            IQueryable<TB_AMEX_IMPORT_RESERVED> query = Context.TB_AMEX_IMPORT_RESERVED.AsQueryable();
            query = query.Where(p => p.TB_AMEX_IMPORT.CD_DESK == "AMX");
            query = query.Where(p => p.ID_AMEX_IMPORT == null);
            return query.ToList();
        }*/
    }
}
