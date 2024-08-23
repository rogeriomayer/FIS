using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Acordo
    {
        public string DataAcordo { get; set; }
        public string DataUltimoPagamento { get; set; }
        public int IdAcordo { get; set; }
        public string Negociador { get; set; }
        public ICollection<ParcelaAcordo> ParcelaAcordo { get; set; }
        public string StatusAcordo { get; set; }
        public decimal ValorAcordo { get; set; }
    }
}
