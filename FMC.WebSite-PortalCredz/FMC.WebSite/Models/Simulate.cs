using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Simulate
{
    public long IdSimulate { get; set; }

    public long IdProduct { get; set; }

    public decimal VlEntrace { get; set; }

    public int NrParcel { get; set; }

    public DateTime DtEntrace { get; set; }

    public Nullable<DateTime> DtFirstParcel { get; set; }

    public decimal VlDiscount { get; set; }

    public bool FlPromisse { get; set; }

    public Nullable<bool> FlProcess { get; set; }


    public DateTime DtInsert { get; set; }

}