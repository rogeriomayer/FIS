using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    public class SerasaSimulate
    {
        public DateTime DtEntrace{get;set;}

        public DateTime? DtFirstParcel{get;set;}

        public DateTime DtInsert{get;set;}

        public bool? FlProcess{get;set;}

        public bool FlPromisse{get;set;}

        public long IdAccount{get;set;}

        public long IdSerasaProduct{get;set;}

        public long IdSerasaSimulate{get;set;}

        public int NrParcel{get;set;}

        public decimal VlDesconto{get;set;}

        public decimal VlEntrace{get;set;}

        public SerasaProduct SerasaProduct{get;set;}
    }
}
