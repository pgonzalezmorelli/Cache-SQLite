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
            database = db ?? DependencyService.Get<IDatabase<Cache>>();
        }

        public async Task<Cache> GetCache(Cache cache)
        {
            try
            {
                string selectQuery = string.Format("SELECT * FROM [Cache] WHERE Entity='{0}'", cache.Entity);
                var list = await database.Select(selectQuery);
                return list.FirstOrDefault();
            }
            catch (Exception ex)
            {
                return new Cache();
            }
        }

        public async Task AddCache(Cache cache)
        {
            //cache.Id = Guid.NewGuid();
            var res = await database.Insert(cache);
            //if (res == 0) throw new RepositoryException(Messages.Platform.RepositoryException);
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
