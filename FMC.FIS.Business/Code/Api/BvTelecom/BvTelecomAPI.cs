namespace FMC.FIS.Business.Code.Api.BvTelecom
{
    using FMC.FIS.Business.Models.RCS;
    using Models.BvTelecom;
    using System.Collections.Generic;
    using System.Linq;

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

        public static SendRCSResponse SendSingle(IList<string> phones, ContentRoot content, Options options, Webhooks webhooks)
        {
            var destinations = new List<Destination>();
            phones.ToList().ForEach(p => destinations.Add(new Destination() { to = "+55" + p }));

            var param = new SendRCSRequest();
            param.messages.Add
                (
                    new Message()
                    {

                        sender = "rcs_udiconversacional",
                        destinations = destinations,
                        content = content,
                        options = options,
                        webhooks = webhooks
                    }
                );

            IDictionary<string, string> header = new Dictionary<string, string>();
            header.Add("X-Api-Key", "B1A7BFAF-2804-4A9F-8648-8837B42450FF");


            return RestApi.Post<SendRCSResponse, SendRCSRequest>("https://integration.smartrcs.com.br/api", "message/text", param, header, "", "");
        }


    }
}
