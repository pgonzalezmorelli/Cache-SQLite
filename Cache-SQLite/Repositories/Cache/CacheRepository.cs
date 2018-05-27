using System;
using System.Linq;
using System.Threading.Tasks;
using CacheSQLite.Models;
using Xamarin.Forms;

namespace CacheSQLite.Repositories
{
    public class CacheRepository : ICacheRepository
    {
        private IDatabase<Cache> database;

        public CacheRepository(IDatabase<Cache> db = null)
        {
            database = db ?? new Database<Cache>();
        }

        public async Task<Cache> GetCache(Cache cache)
        {
            try
            {
                string selectQuery = string.Format("SELECT * FROM [Cache] WHERE Entity='{0}'", cache.Entity);
                var list = await database.Select(selectQuery);
                return list.FirstOrDefault();
            }
            catch (Exception)
            {
                return new Cache();
            }
        }

        public async Task AddCache(Cache cache)
        {
            cache.Id = Guid.NewGuid();
            var res = await database.Insert(cache);
        }

        public async Task UpdateCache(Cache cache)
        {
            await database.Update(cache);
        }

        public async Task RemoveAll()
        {
            await database.DeleteAll();
        }
    }
}
