using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class JsonHelper
    {
        public static T GetObjectFromString<T>(object obj)
        {
            if (obj.ToString().Contains("erro"))
            {
                var exception = JsonConvert.DeserializeObject<object>(obj.ToString());
                throw new Exception(exception.ToString());
            }
            else
            {
                /*    if (typeof(T).ToString() == "System.String")
                        return (T)obj;
                    else*/
                return JsonConvert.DeserializeObject<T>(obj.ToString());
            }
        }

        public static T GetObjectFromString<T>(string obj)
        {
            //if (obj.ToString().Contains("erro"))
            //{
            //    var exception = JsonConvert.DeserializeObject<object>(obj);
            //    throw new Exception(exception.ToString());
            //}
            //else
            //{
            /*    if (typeof(T).ToString() == "System.String")
                    return (T)obj;
                else*/
            return JsonConvert.DeserializeObject<T>(obj);

            //}
        }

        internal static string GetJSONFromObject<T>(T obj)
        {
            string json = JsonConvert.SerializeObject(obj,
                            Newtonsoft.Json.Formatting.None,
                            new JsonSerializerSettings
                            {
                                NullValueHandling = NullValueHandling.Ignore,
                                DateFormatHandling = DateFormatHandling.MicrosoftDateFormat
                            });

            return json;
        }
    }
}
