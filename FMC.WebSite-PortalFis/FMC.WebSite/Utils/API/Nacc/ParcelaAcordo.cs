using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class ParcelaAcordo
    {
        public string DataPagamento { get; set; }
        public string DataVencimento { get; set; }
        public long? IdBoleto { get; set; }
        public int NumeroParcela { get; set; }
        public string StatusParcela { get; set; }
        public decimal ValorParcela { get; set; }
    }
}
