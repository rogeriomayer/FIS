using FMC.FIS.Business.Models.RCS;
using System.Collections.Generic;
using System.Linq;

namespace FMC.FIS.Business.Code.Api.RCS
{
    public class OtimaRcsAPI
    {
        public static IList<RcsOtimaResponse> SendSingle(List<MessageItem> messages)
        {
            return RestApi.Post<IList<RcsOtimaResponse>, List<MessageItem>>("http://10.40.0.52/bi/api", "otima/rcs/bulk", messages, "");
        }
    }
}
