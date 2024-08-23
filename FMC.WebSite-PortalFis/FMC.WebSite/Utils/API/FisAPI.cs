using FMC.WebSite.FIS.Models;
using FMC.WebSite.FIS.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class FisAPI
{
    private static string Url_FIS_API = AppSettings.URL_API_FIS; //"http://localhost:5831/api";
    /*
    public static ICollection<ParameterResponse> GetParameters(long idProduct)
    {
        return RestAPI.Get<ICollection<ParameterResponse>>(AppSettings.Url_FIS_API, "parameter/" + idProduct, null, "");
    }

    public static UserAuthenticate GetLogin(LoginUser login)
    {
        return RestAPI.Post<UserAuthenticate, LoginUser>(AppSettings.Url_FIS_API, "login/idp", login);
    }

    public static ICollection<UserProfile> GetUserProlife(string profiles, int idProductType, string token)
    {
        return RestAPI.Get<ICollection<UserProfile>>(AppSettings.Url_FIS_API, "login/userprofile/" + profiles + "/" + idProductType, null, token);
    }
    
    public static bool PostPerson(PersonUpdateRequest personUpdateRequest, int idProductType, string token)
    {
        var result = RestAPI.Post<string, PersonUpdateRequest>(AppSettings.Url_FIS_API, "person/" + idProductType, personUpdateRequest, token);
        return result.ToUpper() == "OK";
    }
    */

    public static string GetToken()
    {
        return string.Empty;
    }

    public static PersonResponse GetPerson(string cpf, int idProductType)
    {
        return RestAPI.Get<PersonResponse>(Url_FIS_API, "Person/" + cpf + "/" + idProductType, null);
    }

    public static AgreementSimulateResponse AgreementSimulate(AgreementSimulateRequest agreementSimulateRequest, int idProductType)
    {
        var result = RestAPI.Post<AgreementSimulateResponse, AgreementSimulateRequest>(Url_FIS_API, "agreement/simulate/" + idProductType, agreementSimulateRequest, "");
        return result;
    }

    public static Agreement SetAgreement(Agreement agreementRequest)
    {
        var result = RestAPI.Post<Agreement, Agreement>(Url_FIS_API, "agreement", agreementRequest, "");
        return result;
    }

    public static ParcelamentoFaturaResponse GetParcelamentoFatura(long idProduct, string cpf, decimal vlEntrada, decimal saldo, decimal minimo, DateTime dtVencimento, int age, string token)
    {
        string sSaldo = saldo.ToString("N2").Replace(".", "").Replace(",", "");
        sSaldo = sSaldo.Insert(sSaldo.Length - 2, ".");

        string sMinimo = minimo.ToString("N2").Replace(".", "").Replace(",", "");
        sMinimo = sMinimo.Insert(sMinimo.Length - 2, ".");

        string sVlEntrada = vlEntrada.ToString("N2").Replace(".", "").Replace(",", "");
        sVlEntrada = sVlEntrada.Insert(sVlEntrada.Length - 2, ".");

        string url = string.Format("lead/SimulacaoParcelamentoFatura/{0}/{1}/{2}/{3}/{4}/{5}/{6}",
            idProduct,
            cpf.Replace("-", "").Replace(".", ""),
            sVlEntrada,
            sSaldo,
            sMinimo,
            dtVencimento.ToString("yyyy-MM-dd"),
            age);

        return RestAPI.Get<ParcelamentoFaturaResponse>(Url_FIS_API, url, null, token);
    }

    public static ICollection<BilletResponse> GetBillets(long idProduct, string token)
    {
        return RestAPI.Get<ICollection<BilletResponse>>(Url_FIS_API, "billet/" + idProduct, null, token);
    }

    public static byte[] GetBilletPDF(string cpf, string codBillet, DateTime dtPayment, int productType, string token)
    {
        string method = string.Format("billet/pdf/{0}/{1}/{2}/{3}", cpf, codBillet, dtPayment.ToString("yyyy-MM-dd"), productType);
        return RestAPI.Get<byte[]>(Url_FIS_API, method, null, token);
    }

    public static byte[] GetFaturaPDF(string cpf, string nrConta, string tpCliente, DateTime dtIni, DateTime dtFim, string token)
    {
        string method = string.Format("billet/FaturaPDF/{0}/{1}/{2}/{3}/{4}", cpf, nrConta, "PF", dtIni.ToString("yyyy-MM-dd"), dtFim.ToString("yyyy-MM-dd"));
        return RestAPI.Get<byte[]>(Url_FIS_API, method, null, token);
    }

    public static LinhaDigitavelResponse GetLinhaDigitavelFatura(string nrConta, string token)
    {
        return RestAPI.Get<LinhaDigitavelResponse>(Url_FIS_API, "billet/LinhaDigitavelFatura/" + nrConta, null, token);
    }

    public static BilletResponse AddBillet(BilletRequest billetRequest, int idProductType, string token)
    {
        var result = RestAPI.Post<BilletResponse, BilletRequest>(Url_FIS_API, "billet/" + idProductType, billetRequest, token);
        return result;
    }

    public static BilletResponse SendBilletEmail(SendEmailRequest sendEmailRequest, int idProductType, string token)
    {
        var result = RestAPI.Post<BilletResponse, SendEmailRequest>(Url_FIS_API, "billet/sendEmail/" + idProductType, sendEmailRequest, token);
        return result;
    }

    public static string SendBilletSMS(SMSRequest smsRequest, int idProductType, string token)
    {
        var result = RestAPI.Post<string, SMSRequest>(Url_FIS_API, "sms/" + 0, smsRequest, token);
        return result;
    }

    /*
    public static BilletResponse SendBilletSMS(SendSMSRequest sendSMSRequest, int idProductType, string token)
    {
        var result = RestAPI.Post<BilletResponse, SendSMSRequest>(Url_FIS_API, "billet/sendSMS/" + idProductType, sendSMSRequest, token);
        return result;
    }
    */

    public static bool PostStatusLead(StatusLead statusLead, string cpf, string telefone, string token)
    {
        var result = RestAPI.Post<string, StatusLead>(Url_FIS_API, "status/" + cpf + "/" + telefone + "/1", statusLead, token);
        return result.ToUpper() == "OK";
    }

    /*
    public static bool ChangePassword(LoginUser login, string token)
    {
        return RestAPI.Post<bool, LoginUser>(AppSettings.Url_FIS_API, "ChangePassword", login, token);
    }

    public static ICollection<StatusResponse> GetStatus(int idProductType, string token)
    {
        return RestAPI.Get<ICollection<StatusResponse>>(AppSettings.Url_FIS_API, "status/" + idProductType, null, token);
    }

    public static ICollection<PromisseTypeResponse> GetTipoPromessa(int idProductType, string token)
    {
        return RestAPI.Get<ICollection<PromisseTypeResponse>>(AppSettings.Url_FIS_API, "status/promisseType/" + idProductType, null, token);
    }

    

    public static ICollection<StatusLeadResponse> GetStatusLeads(int idUser, DateTime dtIni, DateTime dtFim, int idProductType, string token)
    {
        return RestAPI.Get<ICollection<StatusLeadResponse>>(AppSettings.Url_FIS_API, "status/statuslead/" + idUser + "/" + idProductType + "/" + dtIni.ToString("yyyy-MM-dd") + "/" + dtFim.ToString("yyyy-MM-dd"), null, token);
    }

    

    public static ICollection<Discount> GetDiscount(int idProductType, string token)
    {
        return RestAPI.Get<ICollection<Discount>>(AppSettings.Url_FIS_API, "discount/" + idProductType, null, token);
    }

    

    public static ICollection<UserLoginResponse> GetUsers(int idProductType, string token)
    {
        return RestAPI.Get<ICollection<UserLoginResponse>>(AppSettings.Url_FIS_API, "login/all/" + idProductType, null, token);
    }

    public static ICollection<UserLoginResponse> GetUsersIdManager(int idManager, string token)
    {
        return RestAPI.Get<ICollection<UserLoginResponse>>(AppSettings.Url_FIS_API, "login/manager/" + idManager, null, token);
    }

    public static ICollection<StatusLeadResponse> GetStatusLeads(ICollection<int> idUser, DateTime dtIni, DateTime dtFim, int idProductType, string token)
    {
        var statusLeadRequest = new StatusLeadRequest()
        {
            idUser = idUser,
            dtIni = dtIni,
            dtFim = dtFim
        };
        return RestAPI.Post<ICollection<StatusLeadResponse>, StatusLeadRequest>(AppSettings.Url_FIS_API, "status/statuslead/" + idProductType, statusLeadRequest, token);
    }
    */
}
