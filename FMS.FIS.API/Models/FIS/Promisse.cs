using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Promisse")]
    public class Promisse
    {
        public Promisse()
        {
            Billet = new HashSet<Billet>();
        }

        [Key]
        [Column("IdPromisse", TypeName = "bigint")]
        public Int64 IdPromisse { get; set; }

        [Column("IdStatusLead", TypeName = "bigint")]
        public long IdStatusLead { get; set; }

        [Required]
        [Column("VlPromisse", TypeName = "decimal")]
        public decimal VlPromisse { get; set; }

        [Required]
        [Column("DtPromisse", TypeName = "date")]
        public DateTime DtPromisse { get; set; }

        [Required]
        [Column("NrParcel", TypeName = "tinyint")]
        public byte NrParcel { get; set; }

        [Required]
        [Column("VlParcel", TypeName = "decimal")]
        public decimal VlParcel { get; set; }

        [Required]
        [Column("IdPromisseType", TypeName = "tinyint")]
        public byte IdPromisseType { get; set; }
                
        [Required]
        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [ForeignKey("IdStatusLead")]
        public virtual StatusLead StatusLead { get; set; }

        [ForeignKey("IdPromisseType")]
        public virtual PromisseType PromisseType { get; set; }

        public virtual ICollection<Billet> Billet { get; set; }

    }
}
