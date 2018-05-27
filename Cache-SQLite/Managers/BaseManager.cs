using System;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Managers
{
    public class BaseManager
    {
        #region Attributes & Properties

        protected readonly ICacheManager cacheManager;

        #endregion

        public BaseManager(ICacheManager cacheManager = null)
        {
            this.cacheManager = cacheManager ?? new CacheManager();
        }

        public virtual async Task<Cached<T>> GetFromCacheAsync<T>(Func<Task<T>> serviceCall, Func<Cached<T>, Exception, Task> onUpdate) where T : Cacheable
        {
            var cachedResponse = await cacheManager.GetAsync<T>();

            Cached<T> cachedData = new Cached<T>(null);

            if (cachedResponse != null)
            {
                cachedData = new Cached<T>(cachedResponse.Object, cachedResponse.Updated.Value);
            }
            GetFromService(serviceCall, onUpdate, cachedData);

            return await Task.FromResult(cachedData);
        }

        public virtual void GetFromService<T>(Func<Task<T>> serviceCall, Func<Cached<T>, Exception, Task> onUpdate, Cached<T> cachedData) where T : Cacheable
        {
            Task.Run(async () =>
            {
                try
                {
                    var result = await serviceCall();
                    if (result != null)
                    {
                        await cacheManager.PutAsync(result);
                    }
                    await onUpdate(new Cached<T>(result), null);
                }
                catch (Exception ex)
                {
                    await onUpdate(cachedData, ex);
                }
            });
        }
    }
}
