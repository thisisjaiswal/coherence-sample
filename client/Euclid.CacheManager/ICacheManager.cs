using System.Collections.Generic;
using Tangosol.Net.Cache;

namespace Euclid.CacheManager
{
    public interface ICacheManager<K,M>
    {        
        M GetByKey(K key);
        IList<M> GetAll();
        void Save(K key, M item);
        void Subscribe(ICacheListener listener);
    }
}