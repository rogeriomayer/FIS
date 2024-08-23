using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Models
{
    public class TermoECondicao
    {
        public int Age { get; set; }
        public int NrParcel { get; set; }
        public decimal ValueParcel { get; set; }
        public DateTime DateParcel { get; set; }
        public decimal PercentageTax { get; set; }
        public DateTime DateEntranceParcel { get; set; }
        public decimal ValueEntrance { get; set; }
        public decimal CETMensal { get; set; }
        public decimal CETAnual { get; set; }
        public decimal VlCETMensal { get; set; }
        public decimal VlCETAnual { get; set; }
        public string CarteiraNacc { get; set; }
    }
}
