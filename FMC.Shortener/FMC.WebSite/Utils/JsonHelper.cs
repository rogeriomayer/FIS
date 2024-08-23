using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Shortener.Utils
{
    public class JsonHelper
    {
        internal static T GetObjectFromJSONString<T>(string json) where T : new()
        {
            return JsonConvert.DeserializeObject<T>(json.Replace(".issued", "issued").Replace(".expires", "expires"));
        }

        internal static string GetJSONFromObject<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj,
                            Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore
                            });

            return json;
        }
    }
}
