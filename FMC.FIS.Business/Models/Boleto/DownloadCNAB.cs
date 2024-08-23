using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models
{
    [Table("DownloadCNAB", Schema = "dbo")]
    public class DownloadCNAB
    {
        [Key]
        [Column("IdDownloadCNAB", TypeName = "bigint")]
        public long IdDownloadCNAB { get; set; }

        [Column("IdRemessa", TypeName = "bigint")]
        public long IdRemessa { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [Column("DtDownload", TypeName = "datetime")]
        public DateTime DtDownload { get; set; }

    }
}
