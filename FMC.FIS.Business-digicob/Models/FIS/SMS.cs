using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{

    [Table("SMS", Schema = "CREDZ")]
    public class SMS
    {
        [Column("id", TypeName = "bigint")]
        public long id { get; set; }

        [Column("idPerson", TypeName = "bigint")]
        public long idPerson { get; set; }


        [Column("telefone", TypeName = "bigint")]
        public long telefone { get; set; }

        [Column("age", TypeName = "int")]
        public int age { get; set; }

        [Column("dtEnvio", TypeName = "datetime")]
        public DateTime dtEnvio { get; set; }


    }
    public class BvSmsResponse
    {
        public string result { get; set; }
    }

    
}
