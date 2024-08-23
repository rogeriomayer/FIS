using System.ComponentModel.DataAnnotations;

namespace FMC.WebSite.FIS.Models
{
    public class LoginOuvidoria
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
