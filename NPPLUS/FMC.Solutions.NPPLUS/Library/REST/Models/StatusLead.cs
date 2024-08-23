using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMC.Solutions.NPPLUS.Library.REST.Models
{
    public class StatusLead
    {
        public long IdStatusLead { get; set; }

        public long IdLead { get; set; }

        public long IdStatus { get; set; }

        public string DsDescription { get; set; }

        public DateTime DtInsert { get; set; }

        public int IdUserLogin { get; set; }

        public virtual ICollection<Promisse> Promisse { get; set; }

        public virtual ICollection<Agreement> Agreement { get; set; }

        public virtual ICollection<CallBack> CallBack { get; set; }
    }

    public class CallBack
    {
        public long IdCallBack { get; set; }

        public long IdStatusLead { get; set; }

        public string NrPhone { get; set; }

        public DateTime DtCallBack { get; set; }

        public DateTime DtInsert { get; set; }


    }

    public class Promisse
    {
        public Int64 IdPromisse { get; set; }

        public long IdStatusLead { get; set; }

        public decimal VlPromisse { get; set; }

        public DateTime DtPromisse { get; set; }

        public byte NrParcel { get; set; }

        public decimal VlParcel { get; set; }

        public byte IdPromisseType { get; set; }

        public DateTime DtInsert { get; set; }

    }

    public class Agreement
    {
        public long IdAgreement { get; set; }

        public long IdStatusLead { get; set; }

        public decimal VlEntrace { get; set; }

        public DateTime DtEntrace { get; set; }

        public decimal PcDiscount { get; set; }

        public int QtParcel { get; set; }

        public decimal VlParcel { get; set; }

        public decimal VlAgreement { get; set; }

        public string CdPaymentOption { get; set; }

        public string CdParcelPlan { get; set; }

        public DateTime DtInsert { get; set; }

        public ICollection<AgreementParcel> AgreementParcel { get; set; }

    }

    public class AgreementParcel
    {
        public long IdAgreementParcel { get; set; }

        public long IdAgreement { get; set; }

        public int NrParcel { get; set; }

        public DateTime DtParcel { get; set; }

        public decimal VlParcel { get; set; }

    }
}
