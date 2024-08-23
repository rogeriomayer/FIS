using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.APIDialer
{
    public class Transferencia
    {
        public string Canal { get; set; }

        public ICollection<Resultado> Resultado { get; set; }

    }
}
