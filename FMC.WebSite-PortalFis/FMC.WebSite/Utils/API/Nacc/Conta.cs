using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.Afinz.Utils.API.Nacc
{
    public class Conta
    {
        public string CPFCNPJ { get; set; }

        public string Carteira { get; set; }

        public List<Contratos> Contratos { get; set; }

        public List<string> Email { get; set; }

        public string Nome { get; set; }

        public string Telefone { get; set; }
    }
}
