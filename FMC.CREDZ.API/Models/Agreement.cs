namespace FMC.CREDZ.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Agreement")]
    public class Agreement
    {
        [Key]
        [Column("IdAgreement", TypeName = "bigint")]
        public long IdAgreement { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("VlEntrace", TypeName = "decimal")]
        public decimal VlEntrace { get; set; }

        [Column("DtEntrace", TypeName = "datetime")]
        public DateTime DtEntrace { get; set; }


        [Column("DtFirstParcel", TypeName = "datetime")]
        public Nullable<DateTime> DtFirstParcel { get; set; }

        [Column("NrParcel", TypeName = "int")]
        public int NrParcel { get; set; }

        [Column("VlParcel", TypeName = "decimal")]
        public decimal VlParcel { get; set; }

        [Column("VlDiscount", TypeName = "decimal")]
        public decimal VlDiscount { get; set; }

        [Column("FlPromisse", TypeName = "bit")]
        public bool FlPromisse { get; set; }

        [Column("FlAccept", TypeName = "bit")]
        public bool FlAccept { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtMainframeRegister", TypeName = "datetime")]
        public DateTime? DtMainframeRegister { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

    }
}
