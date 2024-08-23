using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("ShortAccess")]
    public class ShortAccess
    {
        [Key]
        [Column("IdShortAccess", TypeName = "bigint")]
        public Int64 IdShortAccess { get; set; }

        [Column("IdShortURL", TypeName = "bigint")]
        public Int64 IdShortURL { get; set; }

        [MaxLength(50)]
        [Column("IP", TypeName = "varchar"),]
        public string IP { get; set; }


        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

    }
}
