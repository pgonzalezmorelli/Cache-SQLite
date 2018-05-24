using System;
using System.Threading.Tasks;
using CacheSQLite.Models;
using CacheSQLite.Repositories;
using Newtonsoft.Json;

namespace CacheSQLite.Managers
{
    public class CacheManager
    {
        private readonly ICacheRepository cacheRepository;

        public CacheManager(ICacheRepository cacheRepository)
        {
            this.cacheRepository = cacheRepository;
        }

        public async Task<CacheResponse<T>> GetCacheAsync<T>() where T : EntityBase
        {
            var cachedObject = await cacheRepository.GetCache(new Cache(typeof(T).Name));
            if (cachedObject != null)
            {
                var obj = JsonConvert.DeserializeObject<T>(cachedObject.Data);
                return new CacheResponse<T>(obj, cachedObject.Updated);
            }
            return null;
        }

        public async Task AddOrUpdateCacheAsync<T>(T entity)
        {

            var storedCache = await cacheRepository.GetCache(GetNewCache<T>());
            if (storedCache != null)
            {
                ReplaceData<T>(ref storedCache, entity);
                await cacheRepository.UpdateCache(storedCache);
            }
            else
                await cacheRepository.AddCache(GetNewCache<T>(entity));
        }

        public async Task Clean()
        {
            await cacheRepository.RemoveAll();
        }

        #region Utilities
        private void ReplaceData<T>(ref Cache cache, T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            cache.Data = json;
            cache.Updated = DateTime.Now;
        }

        private Cache GetNewCache<T>(T entity)
        {
            var json = JsonConvert.SerializeObject(entity);
            return new Cache(typeof(T).Name, json, DateTime.Now);
        }

        private Cache GetNewCache<T>()
        {
            return new Cache(typeof(T).Name);
        }
        #endregion
    }
}
