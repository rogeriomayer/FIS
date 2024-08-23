using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;
using System.Data.Objects;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class DebtorPersistencia : AbstractBLLPersistencia<DB_FMCEntities, TB_DEBTOR>
    {
        public TB_DEBTOR GetById(int idDebtor)
        {
            return Context.TB_DEBTOR.FirstOrDefault(p => p.id_debtor == idDebtor);
        }

        public TB_DEBTOR AddDebtor(TB_DEBTOR debtor)
        {
            return this.Add(debtor);
            //return GetLastInsert();
        }

        public IList<int> GetListDebtorUpdate()
        {
            DateTime dataInsert = DateTime.Today.AddDays(-30);
            IQueryable<TB_DEBTOR> query = Context.TB_DEBTOR.AsQueryable();

            //return query.Where(p => p.id_debtor == 22596952).Select(p => p.id_debtor).ToList();

            query = query.Where(p => p.cd_desk == "AMX");
            query = query.Where(p => p.cd_debtor.StartsWith("75*AMX"));
            query = query.Where(p => p.CD_STATUS == "NEW");
            query = query.Where(p => p.DT_DEBTOR_INSERT > dataInsert);
            query = query.OrderBy(p => p.id_debtor);
            return query.Select(p => p.id_debtor).ToList();
        }

        public TB_DEBTOR GetLastInsert()
        {
            IQueryable<TB_DEBTOR> query = Context.TB_DEBTOR.AsQueryable();
            query = query.Where(p => p.cd_debtor.StartsWith("75*AMX"));
            query = query.OrderByDescending(p => p.dt_update).ThenByDescending(p => p.id_debtor);
            return query.FirstOrDefault();
        }

        public string GetCdDebtor(int idProgram)
        {
            var cdDebtor = Context.ExecuteStoreQuery<string>("SELECT dbo.Fn_Next_Debtor_Code(" + idProgram + ",GETDATE()) ").FirstOrDefault();

            while (Context.TB_DEBTOR.Where(p => p.cd_debtor == cdDebtor && p.id_program == idProgram).FirstOrDefault() != null)
            {
                ProgramPersistencia programPersistencia = new ProgramPersistencia();
                programPersistencia.UpdateCdDebtor(cdDebtor, Convert.ToInt32(idProgram));
                cdDebtor = Context.ExecuteStoreQuery<string>("SELECT dbo.Fn_Next_Debtor_Code(" + idProgram + ",GETDATE()) ").FirstOrDefault();
            }

            return cdDebtor.ToString();
        }

        public string GetLastDeskInsert(string programLetter, IList<string> programs, IList<string> desks)
        {
            DateTime dtPesquisa = DateTime.Now.AddDays(-8);

            IQueryable<TB_DEBTOR> query = Context.TB_DEBTOR.AsQueryable();
            query = query.Where(p => p.cd_debtor.StartsWith("75*AMX"));
            query = query.Where(p => programs.Contains(p.cd_debtor.Substring(0, 9)));
            query = query.Where(p => desks.Contains(p.cd_desk));
            query = query.Where(p => p.DT_DEBTOR_INSERT > dtPesquisa);
            //query = query.Where(p => p.TB_AMEX_IMPORT.Any(a => a.CD_PROGRAM_LETTER.Trim().ToUpper() == programLetter.Trim().ToUpper()));
            query = query.OrderByDescending(p => p.dt_update).ThenByDescending(p => p.id_debtor);
            return query.FirstOrDefault()?.cd_desk;
        }
    }
}
