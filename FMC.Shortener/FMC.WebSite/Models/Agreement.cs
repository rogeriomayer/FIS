using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Shortener.Models
{
    public class Agreement
    {
        public string StatusCode { get; set; }
        public string Status { get; set; }
        public string StatusDate { get; set; }
        public string Agent { get; set; }
        public string Partner { get; set; }
        public string AgreementDate { get; set; }
        public decimal Balance { get; set; }
        public decimal Discount { get; set; }
        public string EntraceDate { get; set; }
        public decimal EntraceValue { get; set; }
        public string FirstParcelDate { get; set; }
        public string Parcels { get; set; }
        public string PaymentDay { get; set; }
        public decimal ParcelDay { get; set; }
        public string DelayedParcel { get; set; }
        public string DelayedParcelDate { get; set; }
        public decimal DelayedParcelValue { get; set; }
        public string DaysDelayedParcel { get; set; }
        public string DaysDelayedEntrace { get; set; }
        public decimal BalancePayment { get; set; }
        public decimal ParcelValue { get; set; }
        public string IndDelayedEntrance { get; set; }

    }
}
