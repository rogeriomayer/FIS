using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Email")]
    public class Email
    {
        [Key]
        [Column("IdEmail", TypeName = "bigint")]
        public Int64 IdEmail { get; set; }

        [Column("IdPerson", TypeName = "bigint")]
        public Int64 IdPerson { get; set; }

        [Column("IdOrigem", TypeName = "tinyint")]
        public byte IdOrigem { get; set; }

        [MaxLength(500)]
        [Column("DsEmail", TypeName = "varchar")]
        public string DsEmail { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("flBloqueado", TypeName = "bit")]
        public bool flBloqueado { get; set; }

        [ForeignKey("IdPerson")]
        public virtual Person Account { get; set; }
    }
}
