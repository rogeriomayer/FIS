using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("ProductType")]
    public class ProductType
    {
        [Key]
        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Required]
        [StringLength(100)]
        public string DsProductType { get; set; }

        public virtual IList<Product> Product { get; set; }
    }
}
