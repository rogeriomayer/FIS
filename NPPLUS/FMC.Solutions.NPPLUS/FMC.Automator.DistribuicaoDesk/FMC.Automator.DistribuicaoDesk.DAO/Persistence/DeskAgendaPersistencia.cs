using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FMC.Automator.DistribuicaoDesk.DAO.Model;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class DeskAgendaPersistencia : AbstractBLLPersistencia<DB_FMCEntities, tb_desk_agenda>
    {
        public tb_desk_agenda GetAgendaAnoCorrente(string desk)
        {
            DateTime anoCorrente = Convert.ToDateTime("01/01/" + DateTime.Now.Year.ToString());
            IQueryable<tb_desk_agenda> query = Context.tb_desk_agenda.AsQueryable();
            query = query.Where(p => p.cd_desk == desk);
            query = query.Where(p => p.dt_agenda == anoCorrente);
            return query.FirstOrDefault();
        }

        public void AddDeskAgendaDetail(string cd_desk,
                                 string cd_debtor,
                                 string cd_status,
                                 decimal vl_balance,
                                 DateTime dt_agenda,
                                 DateTime dt_follow,
                                 DateTime dt_status,
                                 int nr_order)
        {
            Context.InsertDeskAgendaDetail(cd_desk,
                                           cd_debtor,
                                           cd_status,
                                           vl_balance,
                                           dt_agenda,
                                           dt_follow,
                                           dt_status,
                                           nr_order,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null,
                                           null);
        }
    }
}
