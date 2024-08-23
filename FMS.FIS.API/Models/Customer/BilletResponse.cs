using System;

namespace FMC.FIS.API.Models.Customer
{
    public class BilletResponse
    {
        public long IdBillet { get; set; }

        public long IdProduct { get; set; }
        public Nullable<long> IdAgreementParcel { get; set; }

        public Nullable<long> IdPromisse { get; set; }

        public decimal VlBillet { get; set; }

        public DateTime DtBillet { get; set; }

        public string Barcode { get; set; }

        public string Line { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime DtInsert { get; set; }

        public int NrSendEmail { get; set; }
        public int NrSendSMS { get; set; }

        public string CdAgreement { get; set; }

        public string CdBillet { get; set; }
        public string URL { get; set; }

        public int Parcel { get; set; }
    }

    public class NewBilletRequest
    {
        public long IdProduct { get; set; }
        public long IdAgreement { get; set; }
        public long IdAgreementParcel { get; set; }
        public int NrParcel { get; set; }
        public long IdPromisse { get; set; }
        public string CdAgreement { get; set; }
        public decimal VlBillet { get; set; }
        public DateTime DtBillet { get; set; }
        public string CPF { get; set; }

    }
}
