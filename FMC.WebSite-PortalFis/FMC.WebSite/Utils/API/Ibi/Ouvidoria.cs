using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    public class Ouvidoria
    {
        public System.Nullable<System.DateTime> DtAnalysis{get;set;}

        public System.Nullable<System.DateTime> DtClose{get;set;}

        public System.Nullable<System.DateTime> DtImportant{get;set;}

        public System.DateTime DtInsert{get;set;}

        public System.Nullable<System.DateTime> DtVisualized{get;set;}

        public string Email{get;set;}

        public long IdOuvidoria{get;set;}

        public string Mensagem{get;set;}

        public string Nome{get;set;}

        public System.Nullable<int> Status{get;set;}

        public string Tipo{get;set;}
    }
}
