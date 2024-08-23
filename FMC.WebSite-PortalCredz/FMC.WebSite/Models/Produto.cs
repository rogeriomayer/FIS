using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Models
{
    public class Produto
    {
        public string CodProduto{ get; set; }
        public string NomeProduto{ get; set; }
        public long IdAccount { get; set; }
    }
}
