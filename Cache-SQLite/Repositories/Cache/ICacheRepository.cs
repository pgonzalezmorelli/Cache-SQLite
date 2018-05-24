using System;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Repositories
{
    public interface ICacheRepository
    {
        Task<Cache> GetCache(Cache cache);
        Task AddCache(Cache cache);
        Task UpdateCache(Cache cache);
        Task RemoveAll();
    }
}
