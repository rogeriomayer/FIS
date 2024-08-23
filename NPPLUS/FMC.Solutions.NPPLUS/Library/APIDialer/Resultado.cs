using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.APIDialer
{
    public class Resultado
    {
        public string Conta { get; set; }

        public string CodigoCodificacao { get; set; }

        //public string GrupoCodificacao { get; set; }

        public string TelefoneRetorno { get; set; }

        public DateTime? DataRetorno { get; set; }

        public string IdStatusDialer { get; set; }
    }
}
