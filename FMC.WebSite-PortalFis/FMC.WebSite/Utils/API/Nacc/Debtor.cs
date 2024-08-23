using System;
using System.Collections.Generic;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Debtor
    {

        public ICollection<Acordo> Acordo { get; set; }
        public string Carteira { get; set; }
        public string Contrato { get; set; }
        public int DiasAtraso { get; set; }
        public int IdDebtor { get; set; }
        public string LogoCarteira { get; set; }
        public string Produto { get; set; }
        public string Status { get; set; }
        public decimal ValorPrincipal { get; set; }
        public decimal ValorSaldo { get; set; }
    }
}
