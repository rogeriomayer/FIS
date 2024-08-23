using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Lead")]
    public class Lead
    {
        public Lead()
        {
            StatusLead = new HashSet<StatusLead>();
        }

        [Key]
        [Column("IdLead", TypeName = "bigint")]
        public Int64 IdLead { get; set; }

        [Column("IdProduct", TypeName = "bigint")]
        public Int64 IdProduct { get; set; }

        [Column("Age", TypeName = "int")]
        public int Age { get; set; }

        [Column("DebitBalance", TypeName = "numeric")]
        public decimal DebitBalance { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("nmArquivo", TypeName = "varchar")]
        public string nmArquivo { get; set; }

        [ForeignKey("IdProduct")]
        public virtual Product Product { get; set; }

        public virtual ICollection<StatusLead> StatusLead { get; set; }

    }
}
