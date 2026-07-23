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
    public class IdentificadorBinDAO : AbstractRepositoryPersistence<IdentificadorBin>
    {
        public IdentificadorBinDAO() : base("CNN_BOLETO") { }


        public IdentificadorBin GetByBinRange(string binRange)
        {
            return this.Context.Where<IdentificadorBin>(p => p.BinRange.Contains(binRange)).FirstOrDefault();
        }
    }

}
