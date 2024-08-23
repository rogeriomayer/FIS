using System;
using System.Collections.Generic;

namespace FMC.WebSite.FIS.Models
{
    public class AgreementSimulateResponse
    {
        public AgreementSimulateResponse()
        {
            ParcelResponse = new HashSet<ParcelResponse>();
        }

        public DateTime DateEntrace { get; set; }

        public DateTime DateFirstParcel { get; set; }

        public decimal VlDue { get; set; }

        public decimal VlFull { get; set; }

        public decimal PctInterest { get; set; }

        public decimal PctDiscount { get; set; }

        public string CdSimulate { get; set; }

        public ICollection<ParcelResponse> ParcelResponse { get; set; }
    }

    public class ParcelResponse
    {
        public decimal ValueEntrace { get; set; }
        public int NrParcel { get; set; }
        public decimal VlParcel { get; set; }
        public decimal VlDiscount { get; set; }
        public decimal PctMonthCET { get; set; }
        public decimal PctYearCET { get; set; }
        public decimal VlMonthCET { get; set; }
        public decimal VlYearCET { get; set; }
        public decimal VlFull { get; set; }
        public DateTime DtParcel { get; set; }
        public string CdParcel { get; set; }
    }

    public class AgreementSimulateRequest
    {
        public string CPF { get; set; }
        public string Product { get; set; }
        public DateTime DtEntrace { get; set; }
        public decimal VlEntrace { get; set; }
        public int PctDiscount { get; set; }
        public int Age { get; set; }
        public string CdSimulate { get; set; }
    }
}
