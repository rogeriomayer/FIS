using FMC.FIS.Business.Models;
using FMC.FIS.Business.Models.Boleto;
using FMC.FIS.Business.Models.CREDZ;
using FMC.FIS.Business.Models.FIS;
using FMC.Generic;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.DAO
{
    public class SacadoDAO : AbstractRepositoryPersistence<Sacado>
    {
        public SacadoDAO() : base("CNN_BOLETO") { }

        public Sacado GetByCPFCNPJ(string cpfCnpj)
        {
            return this.Context.Where(p => p.CpfCnpj == cpfCnpj)
                      .FirstOrDefault<Sacado>();
        }

    }

}
