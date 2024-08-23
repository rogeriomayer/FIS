using FMC.CREDZ.API.Models;
using FMC.Generic;
using System;
using System.Linq;

namespace FMC.CREDZ.DAO.Persistence
{
    public class BilletDAO : AbstractRepositoryPersistence<Billet>
    {
        public BilletDAO() : base("CNN_CREDZ")
        {
            //Configuration.ProxyCreationEnabled = false;
        }

        public Billet GetByCPF(string cpf, DateTime dtBillet)
        {
            return Context.Where(p => p.CPF == cpf && p.DtInsert >= dtBillet).OrderByDescending(p => p.IdBillet).FirstOrDefault();
        }
    }
}
