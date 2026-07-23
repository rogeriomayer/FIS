using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("ProductSpecification")]
    public class ProductSpecification
    {
        [Key]
        [Column("IdProductSpecification", TypeName = "int")]
        public int IdProductSpecification { get; set; }

        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Column("Logo", TypeName = "int")]
        public int Logo { get; set; }

        [Required]
        [StringLength(100)]
        [Column("Description", TypeName = "varchar")]
        public string Description { get; set; }

        [StringLength(200)]
        [Column("UrlImage", TypeName = "varchar")]
        public string UrlImage { get; set; }

        public virtual IList<Product> Product { get; set; }
    }
}
