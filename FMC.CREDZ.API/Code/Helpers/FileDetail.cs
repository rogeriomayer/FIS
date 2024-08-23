using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.CREDZ.API.Code.Helpers
{
    public class FileDetail
    {
        public string Arquivo { get; set; }
        public DateTime DtUpload { get; set; }
        public int NrCpfs { get; set; }
        public string ArquivoRetorno { get; set; }
        public Nullable<DateTime> DtArquivoRetorno { get; set; }
        public Nullable<int> NrCpfsRetorno { get; set; }
        public Nullable<int> NrDownloads { get; set; }
    }
}
