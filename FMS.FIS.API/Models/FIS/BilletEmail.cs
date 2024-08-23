using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("BilletEmail")]
    public class BilletEmail
    {
        [Key]
        [Column("IdBilletEmail", TypeName = "bigint")]
        public long IdBilletEmail { get; set; }

        [Column("IdBillet", TypeName = "bigint")]
        public long IdBillet { get; set; }

        [Column("Email", TypeName = "varchar")]
        public string Email { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdBillet")]
        public virtual Billet Billet { get; set; }

    }
}
