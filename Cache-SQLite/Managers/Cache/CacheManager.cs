using System;
using System.Threading.Tasks;
using CacheSQLite.Models;
using CacheSQLite.Repositories;
using Newtonsoft.Json;

namespace CacheSQLite.Managers
{
    public class CacheManager : ICacheManager
    {
        #region Attributes & Properties

        private readonly ICacheRepository cacheRepository;

        #endregion

        public CacheManager(ICacheRepository cacheRepository = null)
        {
            this.cacheRepository = cacheRepository ?? new CacheRepository();
        }

        public async Task<CachedResponse<T>> GetAsync<T>() where T : Cacheable
        {
            var cachedObject = await cacheRepository.GetCache(new Cache(typeof(T).Name));
            if (cachedObject != null)
            {
                var obj = cachedObject.Data != null ? JsonConvert.DeserializeObject<T>(cachedObject.Data) : null;
                return new CachedResponse<T>(obj, cachedObject.Updated);
            }
            return null;
        }

        public async Task PutAsync<T>(T entity) where T : Cacheable
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

        public Task RemoveAsync<T>(T entity) where T : Cacheable
        {
            throw new NotImplementedException();
        }

        public async Task RemoveAll()
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
