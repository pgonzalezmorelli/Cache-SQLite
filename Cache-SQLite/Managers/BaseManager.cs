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

        public BaseManager(ICacheManager cacheManager)
        {
            this.cacheManager = cacheManager;
        }

        public virtual async Task<Result<T>> GetFromCacheAsync<T>(Func<Task<T>> serviceCall, Func<Result<T>, Exception, Task> onUpdate) where T : EntityBase
        {
            var cachedResponse = await cacheManager.GetCacheAsync<T>();

            Result<T> cachedData = new Result<T>(null);

            if (cachedResponse != null)
            {
                cachedData = new Result<T>(cachedResponse.Object, cachedResponse.Updated);
            }
            GetFromService(serviceCall, onUpdate, cachedData);

            return await Task.FromResult(cachedData);
        }

        public virtual void GetFromService<T>(Func<Task<T>> serviceCall, Func<Result<T>, Exception, Task> onUpdate, Result<T> cachedData) where T : EntityBase
        {
            Task.Run(async () =>
            {
                try
                {
                    var result = await serviceCall();
                    if (result != null)
                    {
                        await cacheManager.AddOrUpdateCacheAsync<T>(result);
                    }
                    await onUpdate(new Result<T>(result), null);
                }
                catch (Exception ex)
                {
                    await onUpdate(cachedData, ex);
                }
            });
        }
    }
}
