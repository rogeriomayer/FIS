using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.API.Models.FIS
{
    [Table("StatusLead")]
    public class StatusLead
    {
        public StatusLead()
        {
            Promisse = new HashSet<Promisse>();
            Agreement = new HashSet<Agreement>();
            CallBack = new HashSet<CallBack>();
        }

        [Key]
        [Column("IdStatusLead", TypeName = "bigint")]
        public long IdStatusLead { get; set; }

        [Column("IdLead", TypeName = "bigint")]
        public long IdLead { get; set; }

        [Column("IdStatus", TypeName = "int")]
        public int IdStatus { get; set; }

        [Column("DsDescription", TypeName = "varchar")]
        public string DsDescription { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("IdUserLogin", TypeName = "int")]
        public int IdUserLogin { get; set; }

        [ForeignKey("IdLead")]
        public virtual Lead Lead { get; set; }

        [ForeignKey("IdUserLogin")]
        public virtual UserLogin UserLogin { get; set; }

        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; }

        public virtual ICollection<Promisse> Promisse { get; set; }

        public virtual ICollection<Agreement> Agreement { get; set; }

        public virtual ICollection<CallBack> CallBack { get; set; }
    }
}
