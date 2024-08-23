using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.Class.Model
{
    public class Produto
    {
        public string Select { get; set; }
        public string Card { get; set; }
        public decimal Saldo { get; set; }
        public int DiasAtraso { get; set; }
        public int DiaVencimento { get; set; }
        public string Account { get; set; }
        public string OrgConta { get; set; }
        public string OrgCMS { get; set; }
        public string LogoCMS { get; set; }
        public string CPF { get; set; }
        public string NomeCartao { get; set; }
        public int IdProductType { get; set; }
        public long IdProduct { get; set; }
        public long IdLead { get; set; }
        public bool DisponivelCobranca { get; set; }
    }
}
