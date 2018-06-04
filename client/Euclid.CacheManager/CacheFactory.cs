using Euclid.CacheManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Euclid.CacheManager
{
    public class CacheFactory<K,M>
         where M : class, new()
    {
        public static ICacheManager<K, M> GetCacheManager()
        {
            return new CoherenceWrapper<K, M>();

        }
    }
}
