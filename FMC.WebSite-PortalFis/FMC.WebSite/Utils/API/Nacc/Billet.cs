using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Billet
    {
        public string CodigoBarras{get;set;}

        public long IdBoleto{get;set;}

        public string LinhaDigitavel{get;set;}

        public string NossoNumero{get;set;}

        public byte[] PDF{get;set;}
    }
}
