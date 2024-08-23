using FMC.FIS.API.Models.Customer;

namespace FMC.FIS.API.Code.Api.TWW
{
    public class TwwApi
    {
        private static TwwSoapService.ReluzCapWebServiceSoapClient ApiClient = new TwwSoapService.ReluzCapWebServiceSoapClient(TwwSoapService.ReluzCapWebServiceSoapClient.EndpointConfiguration.ReluzCap_x0020_Web_x0020_ServiceSoap);
        public static string SendSms(SMSRequest smsRequest)
        {
            string seuNum = "";
            if (smsRequest.phone.Length > 10)
                seuNum = smsRequest.phone.Substring(0, 2) + smsRequest.phone.Substring(smsRequest.phone.Length - 8, 8);
            else
                seuNum = smsRequest.phone;

            return ApiClient.EnviaSMS(Constants.USER_TWW, Constants.PASS_TWW, seuNum, smsRequest.phone, smsRequest.message);

            }
    }
}
