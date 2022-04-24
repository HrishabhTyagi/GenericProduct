using System;
using System.Collections.Generic;
using System.Runtime.Caching;
using System.Text;

namespace Common
{
    public class MemoryCacher
    {
        public MemoryCache cache;
        public MemoryCacher()
        {
            cache = MemoryCache.Default;
        }
        public T GetValue<T>(string key)
        {
            var result = (T) cache.Get(key);

            if (result != null)
                return result;
            return default;
        }
        public bool Add(string key, object value)
        {
            return cache.Add(key, value, DateTime.MaxValue);
        }

        public void Delete(string key)
        {
            if (cache.Contains(key))
                cache.Remove(key);
        }

        public void RemoveAllExistingCache()
        {
            foreach (var item in cache)
                cache.Remove(item.Key);
        }
    }
}
