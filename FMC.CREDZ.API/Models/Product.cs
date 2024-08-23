namespace FMC.CREDZ.API.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Product")]
    public class Product
    {
        public Product()
        {
            //Simulate = new HashSet<Simulate>();
            //Agreement = new HashSet<Agreement>();
        }
        [Key]
        [Column("IdProduct", TypeName = "bigint")]
        public long IdProduct { get; set; }

        [Column("IdNavigation", TypeName = "bigint")]
        public long IdNavigation { get; set; }


        [Column("Account", TypeName = "varchar")]
        public string Account { get; set; }

        [Column("Age", TypeName = "int")]
        public int Age { get; set; }

        [Column("VlFull", TypeName = "decimal")]
        public decimal VlFull { get; set; }

        [Column("VlMinimum", TypeName = "decimal")]
        public decimal VlMinimum { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("ProductType", TypeName = "varchar")]
        public string ProductType { get; set; }

        [ForeignKey("IdNavigation")]
        public virtual Navigation Navigation { get; set; }
        // [ForeignKey("IdSystem")]

        //[DataMember]
        //public virtual ICollection<Simulate> Simulate { get; set; }
        //[DataMember]
        //public virtual ICollection<Agreement> Agreement { get; set; }
    }
}
