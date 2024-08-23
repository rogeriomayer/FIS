using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Pause")]
    public class Pause
    {
        [Key]
        [Column("IdPause", TypeName = "int")]
        public int IdPause { get; set; }

        [Column("IdAytyPause", TypeName = "int")]
        public int IdAytyPause { get; set; }

        [Column("DsPause", TypeName = "varchar")]
        public string DsPause { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtInactive", TypeName = "datetime")]
        public Nullable<DateTime> DtInactive { get; set; }
    }
}
