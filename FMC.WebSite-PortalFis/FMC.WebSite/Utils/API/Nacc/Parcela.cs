using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Parcela
    {
        public string DataEntrada{get;set;}

        public string DataVencParcela1{get;set;}

        public string DataVencUltimaParcela{get;set;}

        public string DiaVencParcelas{get;set;}

        public int NumeroParcelas{get;set;}

        public decimal Total{get;set;}

        public decimal ValorEntrada{get;set;}

        public decimal ValorParcela{get;set;}
    }
}
