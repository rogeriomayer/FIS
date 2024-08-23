using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

public class Simulate
{
    public DateTime DtEntrace { get; set; }

    public DateTime? DtFirstParcel { get; set; }

    public DateTime DtInsert { get; set; }

    public bool? FlProcess { get; set; }

    public bool FlPromisse { get; set; }

    public long IdAccount { get; set; }

    public long IdProduct { get; set; }

    public long IdSimulate { get; set; }

    public int NrParcel { get; set; }

    public decimal VlDiscount { get; set; }

    public decimal VlEntrace { get; set; }

    public Product Product { get; set; }
}