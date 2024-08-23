using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
//using System.Runtime.Caching;
using System.Security.Principal;
using System.Threading.Tasks;

namespace FMC.WebSite.FIS.Utils
{
    public class Cache
    {

        private IMemoryCache cache;
        private string keyCache;

        public Cache(IMemoryCache cache, string keyCache)
        {

            this.cache = cache;
            this.keyCache = keyCache;
        }

        public void AddCache<T>(string keyName, T itemCache)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(3));
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);


            //CacheItemPolicy policy = new CacheItemPolicy();
            //policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Convert.ToDouble(6));

            string key = keyCache + "_" + keyName;
            cache.Set(key, itemCache, cacheEntryOptions);
        }

        public void AddCache<T>(string keyName, List<T> itemCache)
        {
            var cacheEntryOptions = new MemoryCacheEntryOptions();
            cacheEntryOptions.SetSlidingExpiration(TimeSpan.FromMinutes(3));
            cacheEntryOptions.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);

            //CacheItemPolicy policy = new CacheItemPolicy();
            //policy.AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(Convert.ToDouble(10));

            string key = keyCache + "_" + keyName;
            cache.Set(key, itemCache, cacheEntryOptions);
        }

        public T Get<T>(string keyName)
        {
            string key = keyCache + "_" + keyName;
            return cache.Get<T>(key);
        }

        public void Remove(string keyName)
        {
            string key = keyCache + "_" + keyName;
            cache.Remove(key);
        }

        public void Remove(List<string> keyList)
        {
            foreach (string k in keyList)
            {
                string key = keyCache + "_" + k;
                cache.Remove(key);
            }

        }

        public void Reset()
        {
            cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
        }


    }
}
