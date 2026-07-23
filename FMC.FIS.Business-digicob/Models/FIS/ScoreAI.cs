using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("ScoreAI_NEW")]
    public class ScoreAI
    {
        [Key]
        public long IdScoreAI { get; set; }

        public long IdProduct { get; set; }

        public decimal Score { get; set; }

        public DateTime DtInsert { get; set; }

        public DateTime DtUpdate { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
    }
}
