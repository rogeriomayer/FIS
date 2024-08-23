using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class AmexImportPersistencia
    {
        /*public class AmexImportPersistencia : AbstractBLLPersistencia<DB_FMCEntities, TB_AMEX_IMPORT>
        {
            public TB_AMEX_IMPORT GetById(int idAmexImport)
            {
                return Context.TB_AMEX_IMPORT.FirstOrDefault(p => p.ID_AMEX_IMPORT == idAmexImport);
            }

            public IList<TB_AMEX_IMPORT> GetByIdProcess(int idProcess)
            {
                IQueryable<TB_AMEX_IMPORT> query = Context.TB_AMEX_IMPORT.AsQueryable();
                query = query.Where(p => p.ID_PROCESS == idProcess);
                query = query.OrderBy(p => p.CD_PROGRAM_LETTER).ThenBy(p => p.NR_CARDMEMBER);
                return query.ToList();
            }

            public IList<TB_AMEX_IMPORT_SUPP> GetAmexImportSupp(int idAmexImport)
            {
                return Context.TB_AMEX_IMPORT_SUPP.Where(p => p.ID_AMEX_IMPORT == idAmexImport).ToList();
            }*/

    }
}
