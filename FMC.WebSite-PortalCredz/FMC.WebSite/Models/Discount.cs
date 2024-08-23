using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Discount
{
    public int IdDiscount { get; set; }
    public byte IdProductType { get; set; }
    public int MinAge { get; set; }
    public int MaxAge { get; set; }
    public decimal MaxDiscount { get; set; }
    public int MinParcel { get; set; }
    public int MaxParcel { get; set; }
    public Nullable<DateTime> DtInactive { get; set; }

}
