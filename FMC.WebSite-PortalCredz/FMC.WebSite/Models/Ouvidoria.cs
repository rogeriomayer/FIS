using System;
using System.ComponentModel.DataAnnotations;

namespace FMC.WebSite.FIS.Models
{
    public class Ouvidoria
    {
        public string Nome { get; set; }
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Tipo { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Descricao { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        public int Status { get; set; }
    }
}
