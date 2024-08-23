using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("AytyStatus")]
    public class AytyStatus
    {
        [Key]
        [Column("IdAytyStatus", TypeName = "int")]
        public int IdAytyStatus { get; set; }

        [Column("IdAyty", TypeName = "int")]
        public int IdAyty { get; set; }
        
        [Column("IdStatus", TypeName = "int")]
        public int IdStatus { get; set; }

        [Column("DsStatus", TypeName = "varchar")]
        public string DsStatus { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtInactive", TypeName = "datetime")]
        public Nullable<DateTime> DtInactive { get; set; }
    }
}
