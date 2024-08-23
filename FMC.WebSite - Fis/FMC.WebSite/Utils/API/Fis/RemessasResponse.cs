using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Utils.API.Fis
{
    public class RemessasResponse
    {
        public string TipoProduto { get; set; }

        public DateTime DtGeracaoRemessa { get; set; }

        public DateTime? DtRetornoRemessa { get; set; }

        public long IdRemessa { get; set; }

        public string StatusRemessa { get; set; }

        public string NomeArquivo { get; set; }

        public long NrRemessa { get; set; }

    }
}
