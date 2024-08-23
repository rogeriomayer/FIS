using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
    public string URL { get; set; }

    public int Parcel { get; set; }
}

public class BilletRequest
{
    public long IdProduct { get; set; }
    public long IdAgreement { get; set; }
    public int NrParcel { get; set; }
    public long IdAgreementParcel { get; set; }
    public long IdPromisse { get; set; }
    public string CdAgreement { get; set; }
    public decimal VlBillet { get; set; }
    public DateTime DtBillet { get; set; }
    public string CPF { get; set; }

}


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

public class SMSRequest
{
    public string phone { get; set; }
    public string message { get; set; }
}

public class LinhaDigitavelResponse
{
    public LinhaDigitavelRet responseData { get; set; }
}

public class LinhaDigitavelRet
{
    public string linhaDigitavel { get; set; }
}

