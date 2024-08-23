using Foolproof;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Models
{
    public class Endereco
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Rua { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Numero { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Bairro { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [StringLength(10, MinimumLength = 10, ErrorMessage = " ")]
        public string Cep { get; set; }
        public string Complemento { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Cidade { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Estado { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]

        public string Telefone { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Celular { get; set; }

        public ICollection<PhoneResponse> Telefones { get; set; }

        public ICollection<string> NaccTelefones { get; set; }

    }
}
