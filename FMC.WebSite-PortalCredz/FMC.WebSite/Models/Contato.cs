using System.ComponentModel.DataAnnotations;

namespace FMC.WebSite.FIS.Models
{
    public class Contato
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Nome { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Telefone { get; set; }
        public string TelefoneSecundario { get; set; }
        public string Tipo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string CPF { get; set; }
        public string Mensagem { get; set; }
    }
}
