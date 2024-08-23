using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Product")]
    public class Product
    {

        public Product()
        {
            Lead = new HashSet<Lead>();
        }

        [Key]
        [Column("IdProduct", TypeName = "bigint")]
        public Int64 IdProduct { get; set; }

        [Required]
        [Column("IdPerson", TypeName = "bigint")]
        public Int64 IdPerson { get; set; }

        [Required]
        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Column("IdProductSpecification", TypeName = "int")]
        public Nullable<int> IdProductSpecification { get; set; }

        [Required]
        [StringLength(50)]
        [Column("DsProduct", TypeName = "varchar")]
        public string DsProduct { get; set; }

        [Required]
        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }


        [ForeignKey("IdPerson")]
        public virtual Person Person { get; set; }

        [ForeignKey("IdProductType")]
        public virtual ProductType ProductType { get; set; }

        [ForeignKey("IdProductSpecification")]
        public virtual ProductSpecification ProductSpecification { get; set; }
        

        public virtual ICollection<Lead> Lead { get; set; }

    }
}
