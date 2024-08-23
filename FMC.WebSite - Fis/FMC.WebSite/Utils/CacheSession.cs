using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Caching;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FMC.Fis.Utils
{
    public class CacheSession
    {

        public IMemoryCache cache;
        private IHttpContextAccessor context;
        private string keySession;

        public CacheSession(IHttpContextAccessor context, string keyCache)
        {

            //this.cache = cache;
            this.context = context;
            this.keySession = keyCache;
        }

        public void AddCache<T>(string keyName, T itemSession)
        {
            string key = keySession + "_" + keyName;
            SetObject(key, itemSession);
        }

        public void AddCache<T>(string keyName, List<T> itemSession)
        {
            string key = keySession + "_" + keyName;
            SetObject(key, itemSession);
        }

        public T Get<T>(string keyName)
        {
            string key = keySession + "_" + keyName;
            return GetObject<T>(key);
        }

        public void Remove(string keyName)
        {
            string key = keySession + "_" + keyName;
            context.HttpContext.Session.Remove(key);
            //cache.Remove(key);
        }

        public void Remove(List<string> keyList)
        {
            Parallel.ForEach(keyList,  (string k) => {
                string key = keySession + "_" + k;
                context.HttpContext.Session.Remove(key);
            });
                
        }

        public void Reset()
        {
            //cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        }



        public void SetObject<T>(string key, T value)
        {
            context.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public void SetObject<T>(string key, List<T> value)
        {
            context.HttpContext.Session.SetString(key, JsonConvert.SerializeObject(value));
        }

        public T GetObject<T>(string key)
        {
            var value = context.HttpContext.Session.GetString(key);
            return value == null ? default(T) : JsonConvert.DeserializeObject<T>(value);
        }
    }
}
