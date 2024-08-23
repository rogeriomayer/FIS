using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models
{
    public class ConstantAccess
    {
        [Key]
        [Required(ErrorMessage = "Informe o Código de Acesso.")]
        public int IdConstantAccess { get; set; }

        [Required(ErrorMessage = "Informe a Descrição do Acesso.")]
        [StringLength(100, ErrorMessage = "O tamanho máximo são 100 caracteres.")]
        public string DsConstantAccess { get; set; }

        public virtual ICollection<UserProfileConstantAccess> UserProfileConstantAccess { get; set; }
    }
}
