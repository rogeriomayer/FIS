using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("TelegramCode")]
    public class TelegramCode
    {
        [Key]
        [Column("IdTelegramCode", TypeName = "bigint")]
        public Int64 IdTelegramCode { get; set; }

        [Column("IdPhone", TypeName = "bigint")]
        public Int64 IdPhone { get; set; }

        [Column("IdChat", TypeName = "bigint")]
        public Int64 IdChat { get; set; }

        [Column("LastAcess", TypeName = "datetime")]
        public DateTime LastAcess { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdPhone")]
        public virtual Phone Phone { get; set; }

    }
}
