using System;
public class Agreement
{

    public long IdAgreement { get; set; }
    public long IdProduct { get; set; }
    public long IdAccount { get; set; }
    public decimal VlEntrace { get; set; }
    public DateTime DtEntrace { get; set; }
    public DateTime? DtFirstParcel { get; set; }
    public int NrParcel { get; set; }
    public decimal VlParcel { get; set; }
    public decimal VlDiscount { get; set; }
    public bool FlPromisse { get; set; }
    public bool FlAccept { get; set; }
    public DateTime DtInsert { get; set; }
    public virtual Product Product { get; set; }
}