using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.CREDZ
{
    [Table("SendTelegram", Schema = "CREDZ")]
    public class SendTelegram
    {
        [Key]
        [Column("IdSendTelegram", TypeName = "bigint")]
        public long IdSendTelegram { get; set; }

        [Column("IdPerson", TypeName = "bigint")]
        public long IdPerson { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("IdPhone", TypeName = "bigint")]
        public long IdPhone { get; set; }

        [Column("Age", TypeName = "int")]
        public int Age { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }


    }
}
