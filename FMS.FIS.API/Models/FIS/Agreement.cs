
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FMC.FIS.API.Models.FIS
{
    [Table("Agreement")]
    public class Agreement
    {
        public Agreement()
        {
            AgreementParcel = new HashSet<AgreementParcel>();
        }

        [Key]
        [Column("IdAgreement", TypeName = "bigint")]
        public long IdAgreement { get; set; }

        [Column("IdStatusLead", TypeName = "bigint")]
        public long IdStatusLead { get; set; }

        [Column("VlEntrace", TypeName = "decimal")]
        public decimal VlEntrace { get; set; }

        [Column("DtEntrace", TypeName = "date")]
        public DateTime DtEntrace { get; set; }

        [Column("PcDiscount", TypeName = "decimal")]
        public decimal PcDiscount { get; set; }

        [Column("QtParcel", TypeName = "int")]
        public int QtParcel { get; set; }

        [Column("VlParcel", TypeName = "decimal")]
        public decimal VlParcel { get; set; }

        [Column("VlAgreement", TypeName = "decimal")]
        public decimal VlAgreement { get; set; }


        [Column("IdAgreementStatus", TypeName = "int")]
        public int IdAgreementStatus { get; set; }

        [Column("CdPaymentOption", TypeName = "varchar")]
        public string CdPaymentOption { get; set; }

        [Column("CdParcelPlan", TypeName = "varchar")]
        public string CdParcelPlan { get; set; }

        [Column("CdAgreement", TypeName = "varchar")]
        public string CdAgreement{ get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdStatusLead")]
        public virtual StatusLead StatusLead { get; set; }

        [ForeignKey("IdAgreementStatus")]
        public virtual AgreementStatus AgreementStatus { get; set; }

        public virtual ICollection<AgreementParcel> AgreementParcel { get; set; }
    }
}

