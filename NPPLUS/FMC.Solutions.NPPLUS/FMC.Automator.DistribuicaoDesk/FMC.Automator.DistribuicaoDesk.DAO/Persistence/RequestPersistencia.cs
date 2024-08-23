using FMC.Automator.DistribuicaoDesk.DAO.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FMC.Automator.DistribuicaoDesk.DAO.Persistence
{
    public class RequestPersistencia : AbstractBLLPersistencia<DB_FMCEntities, TB_REQUEST>
    {
        public override TB_REQUEST Add(TB_REQUEST pEntity)
        {
            pEntity.id_request = Context.TB_REQUEST.Max(p => p.id_request) + 1;
            return base.Add(pEntity);
        }
    }
}
