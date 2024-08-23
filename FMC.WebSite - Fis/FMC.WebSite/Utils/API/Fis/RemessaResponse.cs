using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Utils.API.Fis
{
    public class RemessaResponse
    {

        public long IdRemessa { get; set; }

        public string NomeArquivo { get; set; }

        public byte[] File { get; set; }

    }
}
