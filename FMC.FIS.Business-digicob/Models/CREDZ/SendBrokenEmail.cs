using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.CREDZ
{
    [Table("ResendEmail", Schema = "CREDZ")]
    public class ResendEmail
    {
        [Column("id", TypeName = "bigint")]
        public long id { get; set; }

        [Column("idPerson", TypeName = "bigint")]
        public long idPerson { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("email", TypeName = "varchar")]
        public string email { get; set; }

        [Column("nrSimulation", TypeName = "int")]
        public int nrSimulation { get; set; }

        [Column("dtInsert", TypeName = "datetime")]
        public DateTime dtInsert { get; set; }


    }
}
