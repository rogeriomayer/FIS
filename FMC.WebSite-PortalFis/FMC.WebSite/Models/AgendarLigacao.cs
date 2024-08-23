using System.ComponentModel.DataAnnotations;

namespace FMC.WebSite.FIS.Models
{
    public class AgendarLigacao
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Nome{ get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Telefone{ get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string CpfCnpjCliente{ get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string Periodo{ get; set; }
    }
}
