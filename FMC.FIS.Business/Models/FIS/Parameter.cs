using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Parameter")]
    public class Parameter
    {

        [Key]
        [Column("IdParameter", TypeName = "tinyint")]
        public byte IdProduct { get; set; }

        [Required]
        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Key", TypeName = "varchar")]
        public string Key { get; set; }

        [Required]
        [StringLength(50)]
        [Column("Value", TypeName = "varchar")]
        public string Value { get; set; }

    }
}
