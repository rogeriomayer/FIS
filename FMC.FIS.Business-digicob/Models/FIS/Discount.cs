using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.FIS.Business.Models.FIS
{
    [Table("Discount")]
    public class Discount
    {
        [Key]
        [Column("IdDiscount", TypeName = "int")]
        public int IdDiscount { get; set; }

        [Column("IdProductType", TypeName = "tinyint")]
        public byte IdProductType { get; set; }

        [Column("MinAge", TypeName = "int")]
        public int MinAge { get; set; }

        [Column("MaxAge", TypeName = "int")]
        public int MaxAge { get; set; }

        [Column("MaxDiscount", TypeName = "decimal")]
        public decimal MaxDiscount { get; set; }

            [Column("MinParcel", TypeName = "int")]
        public int MinParcel { get; set; }

        [Column("MaxParcel", TypeName = "int")]
        public int MaxParcel { get; set; }

        [Column("DtInactive", TypeName = "datetime")]
        public Nullable<DateTime> DtInactive { get; set; }

    }
}
