using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("CallBack")]
    public class CallBack
    {

        [Key]
        [Column("IdCallBack", TypeName = "bigint")]
        public long IdCallBack { get; set; }

        [Column("IdStatusLead", TypeName = "bigint")]
        public long IdStatusLead { get; set; }

        [Required]
        [Column("NrPhone", TypeName = "varchar")]
        public string NrPhone { get; set; }

        [Required]
        [Column("DtCallBack", TypeName = "datetime")]
        public DateTime DtCallBack { get; set; }

        [Required]
        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdStatusLead")]
        public virtual StatusLead StatusLead { get; set; }



    }
}
