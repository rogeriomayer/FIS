using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Contratos
    {
        public string Contrato{get;set;}

        public string DataVencimento{get;set;}

        public int DiasAtraso{get;set;}

        public int IdConta{get;set;}

        public int LeadId{get;set;}

        public string UniqueIdVicidialLog{get;set;}

        public decimal ValorDebito{get;set;}

        public decimal VlAcordoVista{get;set;}

        public decimal VlMaxDescontoParcelado{get;set;}

        public decimal VlMaxDescontoVista{get;set;}
    }
}
