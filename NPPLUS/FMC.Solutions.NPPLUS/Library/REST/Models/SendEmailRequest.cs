using System;

public class SendEmailRequest
{
    public long idProduct { get; set; }
    public string cpf { get; set; }
    public string codBillet { get; set; }
    public int parcel { get; set; }
    public DateTime dtPayment { get; set; }
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