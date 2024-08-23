using FMC.FIS.Business.Models.RCS;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.Code.Api.RCS
{
    public class InfobipRcsAPI
    {
        public static SendRCSResponse SendSingle(IList<string> phones, ContentRoot content, Options options)
        {
            var destinations = new List<Destination>();
            phones.ToList().ForEach(p => destinations.Add(new Destination() { to = "+55" + p }));

            var param = new SendRCSRequest();
            param.messages.Add
                (
                    new Message()
                    {

                        sender = "UDI2FMC",
                        destinations = destinations,
                        content = content,
                        options = options
                    }
                );

            IDictionary<string, string> header = new Dictionary<string, string>();
            header.Add("Authorization", "App e0d6425094f4a5ec40321077be4ea576-6aa09e93-bc35-41df-aba2-5064aba56452");


            return RestApi.Post<SendRCSResponse, SendRCSRequest>("https://6gr4lz.api.infobip.com/rcs/2", "messages", param, header, "", "");
        }
    }
}
