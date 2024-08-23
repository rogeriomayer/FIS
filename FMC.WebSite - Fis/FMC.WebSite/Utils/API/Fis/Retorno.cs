using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Fis.Utils.API.Fis
{
    public class RetornoRequest
    {
        public string NomeArquivo { get; set; }
        public byte[] Arquivo { get; set; }
        public int IdUserLogin { get; set; }
    }

    public class RetornoResponse
    {
        public string NomeArquivo { get; set; }
        public string DataUpload { get; set; }
        public string UserLogin { get; set; }
    }

    public class FormFile
    {
        public IFormFile formFile { get; set; }
    }
}
