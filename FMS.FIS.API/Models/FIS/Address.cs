namespace FMC.FIS.API.Models.FIS
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

        [Column("IdPerson", TypeName = "bigint")]
        public long IdPerson { get; set; }

        [Column("DsCep", TypeName = "varchar")]
        public string DsCep { get; set; }

        [Column("DsAddress", TypeName = "varchar")]
        public string DsAddress { get; set; }

        [Column("NrAddress", TypeName = "varchar")]
        public string NrAddress { get; set; }

        [Column("DsComplement", TypeName = "varchar")]
        public string DsComplement { get; set; }

        [Column("DsDistrict", TypeName = "varchar")]
        public string DsDistrict { get; set; }
        
        [Column("DsCity", TypeName = "varchar")]
        public string DsCity { get; set; }

        [Column("DsUF", TypeName = "varchar")]
        public string DsUF { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public System.DateTime DtInsert { get; set; }

        [ForeignKey("IdPerson")]
        public virtual Person Account { get; set; }
    }
}
