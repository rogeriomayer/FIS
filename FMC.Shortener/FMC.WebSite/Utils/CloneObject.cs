using Newtonsoft.Json;

namespace FMC.Shortener.Utils
{
    public static class SystemExtension
    {
        public static T DeepCopy<T>(T source)
        {
            var DeserializeSettings = new JsonSerializerSettings { ObjectCreationHandling = ObjectCreationHandling.Replace };
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(source), DeserializeSettings);
        }
    }
}
