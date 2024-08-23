using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models
{
    [Table("UploadCNAB", Schema = "dbo")]
    public class UploadCNAB
    {
        [Key]
        [Column("IdUploadCNAB", TypeName = "bigint")]
        public long IdUploadCNAB { get; set; }

        [Column("DsName", TypeName = "varchar")]
        public string DsName { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("DtUpload", TypeName = "datetime")]
        public DateTime DtUpload { get; set; }

    }
}

