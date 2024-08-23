using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.API.Models.FIS
{
    [Table("Status")]
    public class Status
    {
        public Status()
        {
            StatusLead = new HashSet<StatusLead>();
        }

        [Key]
        [Column("IdStatus", TypeName = "int")]
        public int IdStatus { get; set; }

        [Column("DsStatus", TypeName = "varchar")]
        public string DsStatus { get; set; }

        [Column("CdStatus", TypeName = "varchar")]
        public string CdStatus { get; set; }

        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Column("DaysLift", TypeName = "tinyint")]
        public byte DaysLift { get; set; }

        [Column("DaysSystem", TypeName = "tinyint")]
        public byte DaysSystem { get; set; }

        [Column("FlEfective", TypeName = "bit")]
        public bool FlEfective { get; set; }

        [Column("FlShowUser", TypeName = "bit")]
        public bool FlShowUser { get; set; }

        [Column("FlCallBack", TypeName = "bit")]
        public bool FlCallBack { get; set; }

        [Column("FlSetPhone", TypeName = "bit")]
        public bool FlSetPhone { get; set; }

        [Column("DtInsert", TypeName = "datetime")]
        public DateTime DtInsert { get; set; }

        [Column("DtInactive", TypeName = "datetime")]
        public DateTime? DtInactive { get; set; }

        [Column("IdStatusDialer", TypeName = "varchar")]
        public string IdStatusDialer { get; set; }

        public virtual ICollection<StatusLead> StatusLead { get; set; }

    }

    public class StatusResponse
    {
        public int IdStatus { get; set; }

        public string DsStatus { get; set; }
        public string CdStatus { get; set; }

        public bool FlEfective { get; set; }

        public bool FlShowUser { get; set; }

        public bool FlCallBack { get; set; }

        public string IdStatusDialer { get; set; }
    }
}
