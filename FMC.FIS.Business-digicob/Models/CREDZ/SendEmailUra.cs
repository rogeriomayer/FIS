using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.CREDZ
{
    [Table("SendEmailUra", Schema = "CREDZ")]
    public class SendEmailUra
    {
        [Column("IdSendEmailUra", TypeName = "bigint")]
        public long id { get; set; }

        [Column("idPerson", TypeName = "bigint")]
        public long idPerson { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("email", TypeName = "varchar")]
        public string email { get; set; }

        [Column("age", TypeName = "int")]
        public int age { get; set; }

        [Column("dtInsert", TypeName = "datetime")]
        public DateTime dtInsert { get; set; }


    }
}
