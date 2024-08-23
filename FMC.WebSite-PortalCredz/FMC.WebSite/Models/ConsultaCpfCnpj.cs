using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Models
{
    public class ConsultaCpfCnpj
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "É obrigatório preencher o número do CPF.")]
        [StringLength(maximumLength: 14, MinimumLength = 14, ErrorMessage = "No mínimo 14 dígitos.")]
        public string CpfCnpj { get; set; }
    }
}
