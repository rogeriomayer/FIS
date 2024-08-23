using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("AgreementParcel")]
    public class AgreementParcel
    {
        public AgreementParcel()
        {
            Billet = new HashSet<Billet>();
            Payment = new HashSet<Payment>();
        }

        [Key]
        [Column("IdAgreementParcel", TypeName = "bigint")]
        public long IdAgreementParcel { get; set; }

        [Column("IdAgreement", TypeName = "bigint")]
        public long IdAgreement { get; set; }

        [Column("NrParcel", TypeName = "int")]
        public int NrParcel { get; set; }

        [Column("DtParcel", TypeName = "date")]
        public DateTime DtParcel { get; set; }

        [Column("VlParcel", TypeName = "decimal")]
        public decimal VlParcel { get; set; }

        [Column("IdAgreementStatus", TypeName = "int")]
        public Nullable<int> IdAgreementStatus { get; set; }

        [ForeignKey("IdAgreement")]
        public virtual Agreement Agreement { get; set; }

        public virtual ICollection<Billet> Billet { get; set; }

        public virtual ICollection<Payment> Payment { get; set; }
    }

}
