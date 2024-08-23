using System;
namespace FMC.WebSite.FIS.Utils.API.Ibi
{
    public class SerasaAgreement
    {

        public long IdSerasaAgreement { get; set; }
        public long IdSerasaProduct { get; set; }
        public long IdAccount { get; set; }
        public decimal VlEntrace { get; set; }
        public DateTime DtEntrace { get; set; }
        public DateTime? DtFirstParcel { get; set; }
        public int NrParcel { get; set; }
        public decimal VlParcel { get; set; }
        public decimal VlDesconto { get; set; }
        public bool FlPromisse { get; set; }
        public bool FlAccept { get; set; }
        public DateTime DtInsert { get; set; }
        public DateTime? DtMainframeRegister { get; set; }
        public virtual SerasaProduct SerasaProduct { get; set; }
    }
}
