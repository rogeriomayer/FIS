using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Payment")]
    public class Payment
    {

        [Key]
        [Column("IdPayment", TypeName = "bigint")]
        public long IdPayment { get; set; }

        [Required]
        [Column("IdAgreementParcel", TypeName = "bigint")]
        public long IdAgreementParcel { get; set; }

        [Required]
        [Column("VlPayment", TypeName = "decimal")]
        public decimal VlPayment { get; set; }

        [Required]
        [Column("DtPayment", TypeName = "date")]
        public DateTime DtPayment { get; set; }


        [Required]
        [StringLength(50)]
        [Column("NmFile", TypeName = "varchar")]
        public string NmFile { get; set; }

        [Required]
        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdAgreementParcel")]
        public virtual AgreementParcel Agreement { get; set; }

    }
}
