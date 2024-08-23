namespace FMC.CREDZ.API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Simulate")]
    public class Simulate
    {
        [Key]
        [Column("IdSimulate", TypeName = "bigint")]
        public long IdSimulate { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        //[Column("IdAccount", TypeName = "bigint")]
        //public long IdAccount { get; set; }

        [Column("VlEntrace", TypeName = "decimal")]
        public decimal VlEntrace { get; set; }

        [Column("NrParcel", TypeName = "int")]
        public int NrParcel { get; set; }

        [Column("DtEntrace", TypeName = "datetime")]
        public DateTime DtEntrace { get; set; }

        [Column("DtFirstParcel", TypeName = "datetime")]
        public Nullable<DateTime> DtFirstParcel { get; set; }

        [Column("VlDiscount", TypeName = "decimal")]
        public decimal VlDiscount { get; set; }

        [Column("FlPromisse", TypeName = "bit")]
        public bool FlPromisse { get; set; }

        [Column("FlProcess", TypeName = "bit")]
        public Nullable<bool> FlProcess { get; set; }


        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }
    }
}
