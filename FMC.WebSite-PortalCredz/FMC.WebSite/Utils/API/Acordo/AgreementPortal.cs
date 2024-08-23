using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Acordo
{
    public class AgreementPortal
    {
        public string Carteira{get;set;}

        public string Documento{get;set;}

        public System.DateTime DtParcel{get;set;}

        public long IdAcordo{get;set;}

        public long IdLead{get;set;}

        public string Nome{get;set;}

        public int NumParcel{get;set;}

        public string Origem{get;set;}

        public string Produto{get;set;}

        public decimal VlParcel{get;set;}
    }
}
