using System;

namespace FMC.FIS.API.Models.Customer
{
    public class SendEmailRequest
    {
        public long idProduct { get; set; }
        public string cpf { get; set; }
        public string nrConta { get; set; }
        public string codBillet { get; set; }
        public int parcel { get; set; }
        public DateTime dtPayment { get; set; }
        public decimal vlBillet { get; set; }
        public string line { get; set; }
        public string urlPdf { get; set; }
        public string email { get; set; }
        public int idUserLogin { get; set; }
    }

    public class SendSMSRequest
    {
        public long idProduct { get; set; }
        public string cpf { get; set; }
        public string codBillet { get; set; }
        public int parcel { get; set; }
        public DateTime dtPayment { get; set; }
        public string phone { get; set; }
        public int idUserLogin { get; set; }
    }
}
