using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models
{
    [Table("UnvailableBilling")]
    public class UnvailableBilling
    {
        [Key]
        [Column("DsProduct", TypeName = "varchar")]
        public string DsProduct { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }
    }
}
