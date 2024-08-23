using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.CREDZ.API.Models
{
    [Table("PIDNames")]
    public class PIDNames
    {
        [Key]
        [Column("IdPIDName", TypeName ="int")]
        public int IdPIDName { get; set; }

        [Column("DsName", TypeName = "varchar")]
        public string DsName { get; set; }

        [Column("Gender", TypeName = "char")]
        public string Gender { get; set; }
    }
}
