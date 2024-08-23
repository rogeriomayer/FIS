using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Models
{
    public class Relatorio
    {
        public string CPF { get; set; }
        public string DataInicio { get; set; }
        public string DataFim { get; set; }
    }
}
