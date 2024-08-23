using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Billet")]
    public class Billet
    {
        public Billet()
        {
            BilletEmail = new HashSet<BilletEmail>();
            BilletSMS = new HashSet<BilletSMS>();
        }

        [Key]
        [Column("IdBillet", TypeName = "bigint")]
        public long IdBillet { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("IdAgreementParcel", TypeName = "bigint")]
        public Nullable<long> IdAgreementParcel { get; set; }

        [Column("IdPromisse", TypeName = "bigint")]
        public Nullable<long> IdPromisse { get; set; }

        [Column("VlBillet", TypeName = "decimal")]
        public decimal VlBillet { get; set; }

        [Column("DtBillet", TypeName = "date")]
        public DateTime DtBillet { get; set; }

        [Column("Barcode", TypeName = "varchar")]
        public string Barcode { get; set; }

        [Column("Line", TypeName = "varchar")]
        public string Line { get; set; }

        [Column("DocumentNumber", TypeName = "varchar")]
        public string DocumentNumber { get; set; }

        [Column("CdAgreement", TypeName = "varchar")]
        public string CdAgreement { get; set; }

        [Column("CdBillet", TypeName = "varchar")]
        public string CdBillet { get; set; }

        [Column("URL", TypeName = "varchar")]
        public string URL { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdAgreementParcel")]
        public virtual AgreementParcel AgreementParcel { get; set; }

        [ForeignKey("IdPromisse")]
        public virtual Promisse Promisse { get; set; }

        public virtual ICollection<BilletEmail> BilletEmail { get; set; }
        public virtual ICollection<BilletSMS> BilletSMS { get; set; }

    }
}
