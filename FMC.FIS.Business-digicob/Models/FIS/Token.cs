using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models
{
    [Table("TokenAPI")]
    public class TokenAPI
    {
        [Key]
        [Column("API", TypeName = "varchar")]
        public string API { get; set; }

        [Column("Token", TypeName = "varchar")]
        public string Token { get; set; }

        [Column("DtExpiration", TypeName = "datetime")]
        public DateTime DtExpiration { get; set; }
    }
}
