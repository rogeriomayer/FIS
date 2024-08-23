using System.ComponentModel.DataAnnotations;

namespace FMC.Shortener.Models
{
    public class Login
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        public string User { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = " ")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
