using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("BilletSMS")]
    public class BilletSMS
    {
        [Key]
        [Column("IdBilletSMS", TypeName = "bigint")]
        public long IdBilletSMS { get; set; }

        [Column("IdBillet", TypeName = "bigint")]
        public long IdBillet { get; set; }

        [Column("Phone", TypeName = "varchar")]
        public string Phone { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdBillet")]
        public virtual Billet Billet { get; set; }

    }
}
