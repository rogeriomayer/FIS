using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FMC.Shortener.Utils
{
    public abstract class AbstractCache<T> : IMemoryCache where T : class, new()
    {
        public abstract ICacheEntry CreateEntry(object key);
        public abstract void Remove(object key);
        public abstract bool TryGetValue(object key, out object value);
        public abstract void Dispose();
    }
}
