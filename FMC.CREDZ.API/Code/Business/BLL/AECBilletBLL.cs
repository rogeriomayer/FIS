using FMC.CREDZ.API.Models;
using FMC.CREDZ.DAO.Persistence;
using FMC.Generic;
using System;

namespace FMC.CREDZ.API.Code.Business.BLL
{
    public class BilletBLL : BLL<Billet, BilletDAO>
    {
        public Billet GetByCPF(string cpf, DateTime dtBillet)
        {
            return persistence.GetByCPF(cpf, dtBillet);
        }

        public override Billet Add(Billet model)
        {
            if (model.IdBillet > 0)
            {
                return UpdateNoContext(model);
            }
            else
            {
                return base.Add(model);
            }
        }
    }
}
