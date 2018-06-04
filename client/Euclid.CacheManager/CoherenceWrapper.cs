using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tangosol.Net;
using Tangosol.Net.Cache;

namespace Euclid.CacheManager
{
    public class CoherenceWrapper<K, M> : CacheManagerBase, ICacheManager<K,M>
        where M : class, new() 

    {

        INamedCache _coherenceCache;

        #region Singleton

        private static CoherenceWrapper<K, M> instance;

        public CoherenceWrapper() {

            CacheFactory.DefaultCacheConfigPath = "config\\cache-config.xml";
            CacheFactory.DefaultPofConfigPath = "config\\pof-config.xml";
            //CacheFactory.DefaultOperationalConfigPath = "config\\op-config.xml";
            //CacheFactory.Configure("config\\cache-config.xml", "config\\op-config.xml");
            
            _coherenceCache = CacheFactory.GetCache("position");            
        }

        public static CoherenceWrapper<K, M> Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CoherenceWrapper<K, M>();
                }
                return instance;
            }
        }

        ~CoherenceWrapper()
        {
            //remove listener
            //release cache
            //CacheFactory.Shutdown();
        }


        #endregion

        public  M GetByKey(K key)
        {
            return (M)_coherenceCache[key];

        }

        public IList<M> GetAll()
        {
            return new List<M>();

        }

        public void Save(K key, M item)
        {
            _coherenceCache.Add(key, item);
        }

        public void Subscribe(ICacheListener listener)
        {
            _coherenceCache.AddCacheListener(listener);
        }

    }

    //public delegate void CacheChangeHandler(CacheEventArgs e);

   /* public class CacheChangeListener : ICacheListener
    {
        private CacheChangeHandler handler;

        public CacheChangeListener(CacheChangeHandler handler)
        {
            this.handler = handler;
        }

        public virtual void EntryInserted(CacheEventArgs eventArg)
        {
            handler?.Invoke(eventArg);
        }

        public virtual void EntryUpdated(CacheEventArgs eventArg)
        {
            handler?.Invoke(eventArg);
        }

        public virtual void EntryDeleted(CacheEventArgs eventArg)
        {
            handler?.Invoke(eventArg);
        }

    }*/
}
