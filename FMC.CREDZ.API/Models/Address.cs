namespace FMC.CREDZ.API.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Address")]
    public class Address
    {
        [Key]
        [Column("IdAddress", TypeName = "bigint")]
        public long IdAddress { get; set; }

        [Column("FlUpdateAddress", TypeName = "bit")]
        public bool FlUpdateAddress { get; set; }

        [Column("FlContinue", TypeName = "bit")]
        public bool FlContinue { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("IdNavigation", TypeName = "bigint")]
        public long IdNavigation { get; set; }

        [ForeignKey("IdNavigation")]
        public Navigation Navigation { get; set; }
    }
}
