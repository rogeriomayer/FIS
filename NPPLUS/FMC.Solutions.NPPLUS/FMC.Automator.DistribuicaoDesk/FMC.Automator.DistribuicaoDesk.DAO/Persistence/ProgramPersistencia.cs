using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class ProgramPersistencia : AbstractBLLPersistencia<DB_FMCEntities, TB_PROGRAM>
    {
        public TB_PROGRAM GetByLetterCardType(string programLetter, string cardType)
        {
            IQueryable<TB_PROGRAM> query = Context.TB_PROGRAM.AsQueryable();
            query = query.Where(p => p.cd_program_letter == programLetter);
            query = query.Where(p => p.ds_program == cardType);
            query = query.Where(p => p.Fl_Automation_Import.HasValue && p.Fl_Automation_Import.Value);
            return query.FirstOrDefault();
        }

        public TB_PROGRAM GetById(int idProgram)
        {
            return Context.TB_PROGRAM.FirstOrDefault(p => p.id_program == idProgram);
        }

        public bool UpdateCdDebtor(string cdDebtor, int idprogram)
        {
            TB_PROGRAM program = Context.TB_PROGRAM.FirstOrDefault(p => p.id_program == idprogram);
            if (program != null)
            {
                program.cd_last_debtor = cdDebtor.Substring(10);
                program.DT_LAST_DEBTOR = DateTime.Now;
                Update(program);
                return true;
            }

            return false;
        }

    }
}
