using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.APIDialer
{
    public class SMS
    {

        public SMS()
        {
            Telefones = new HashSet<string>();
        }
        public ICollection<string> Telefones { get; set; }

        public string Texto { get; set; }

    }

    public class RetornoSMS
    {
        public string Telefone { get; set; }

        public bool EnviadoSucesso { get; set; }
    }
}
