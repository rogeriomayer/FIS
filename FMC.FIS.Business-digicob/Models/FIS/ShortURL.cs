using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("ShortURL")]
    public class ShortURL
    {
        [Key]
        [Column("IdShortURL", TypeName = "bigint")]
        public Int64 IdShortURL { get; set; }

        [MaxLength(20)]
        [Column("Code", TypeName = "varchar"),]
        public string Code { get; set; }

        [Column("URL", TypeName = "varchar")]
        public string URL { get; set; }

        [Column("Access", TypeName = "int")]
        public int Access { get; set; }

        [Column("DtLastAccess", TypeName = "datetime")]
        public Nullable<DateTime> DtLastAccess { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }



    }
}
