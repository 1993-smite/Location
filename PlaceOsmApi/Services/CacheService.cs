using System;
using System.Runtime.Caching;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PlaceOsmApi.Services
{
    public abstract class CacheService<TItem>
    {
        public const string cacheKey = "ItineroOsmCacheKey";
        public MemoryCache cache;
        public TimeSpan period;

        public CacheService(MemoryCache memoryCache, TimeSpan? periodCache = null)
        {
            cache = memoryCache ?? MemoryCache.Default ;
            period = periodCache.HasValue ? periodCache.Value : new TimeSpan(2, 0, 0, 0);
        }

        public TItem GetItem(Func<TItem> func)
        {
            TItem item = (TItem)cache?.Get(cacheKey);
            if (item == null)
            {
                item = func.Invoke();

                cache?.Set(cacheKey, item, DateTimeOffset.Now.Add(period));
            }

            return item;
        }
    }
}
