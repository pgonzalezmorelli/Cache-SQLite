using System;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Managers
{
    public interface ICacheManager
    {
        Task<CacheResponse<T>> GetCacheAsync<T>() where T : EntityBase;

        Task AddOrUpdateCacheAsync<T>(T entity);

        Task Clean();
    }
}
