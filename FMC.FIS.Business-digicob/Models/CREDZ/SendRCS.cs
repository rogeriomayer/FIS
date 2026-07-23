using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.CREDZ
{
    [Table("SendRCS", Schema = "CREDZ")]
    public class SendRCS
    {
        [Column("Id", TypeName = "bigint")]
        public long Id { get; set; }

        [Column("IdPerson", TypeName = "bigint")]
        public long IdPerson { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("phone", TypeName = "varchar")]
        public string Phone { get; set; }

        [Column("IdRCS", TypeName = "varchar")]
        public string IdRCS { get; set; }

        [Column("Age", TypeName = "int")]
        public int Age { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }


    }
}
