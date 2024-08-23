namespace FMC.FIS.API.Code.Api.BvTelecom
{
    using Models.BvTelecom;
    using System.Collections.Generic;

    public class BvTelecomAPI
    {
        private static string URL = "https://smartsms.bvtelecom.com.br/webhook/api/delivery";
        private static string Token = "522f56e0-a4dc-4a44-b78a-bb0bbd773368";

        public static SmsResponse SendSingle(string nrTelefone, string message, long carteiraId, string parceiroId)
        {
            var x = RestApi.PostHttpClients();

            var param = new SingleRequest()
            {
                celular = nrTelefone,
                mensagem = message,
                parceiroId = parceiroId,
                carteiraId = carteiraId
            };

            IDictionary<string, string> header = new Dictionary<string, string>();
            header.Add("ApiKey", Token);


            return RestApi.Post<SmsResponse, SingleRequest>(URL, "single-sms", param, header, "", "");
        }

        public static SmsResponse SendBulk(ICollection<SingleRequest> messages)
        {
            IDictionary<string, string> header = new Dictionary<string, string>();
            header.Add("ApiKey", Token);
            return RestApi.Post<SmsResponse, ICollection<SingleRequest>>(URL, "multiple-sms", messages, header, "", "");
        }
    }
}
