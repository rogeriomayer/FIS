using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("PromisseType")]
    public class PromisseType
    {

        [Key]
        [Required]
        [Column("IdPromisseType", TypeName = "tinyint")]
        public byte IdPromisseType { get; set; }

        [Required]
        [Column("DsPromisseType", TypeName = "varchar")]
        public string DsPromisseType { get; set; }

        [Column("FlParcel", TypeName = "bit")]
        public bool FlParcel { get; set; }

        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }
        
        public virtual ICollection<Promisse> Promisse { get; set; }



    }
}
