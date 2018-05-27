using System;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Managers
{
    public interface ICacheManager
    {
        Task<CachedResponse<T>> GetAsync<T>() where T : Cacheable;

        Task PutAsync<T>(T entity) where T : Cacheable;

        Task RemoveAsync<T>(T entity) where T : Cacheable;

        Task RemoveAll();
    }

    public class CachedResponse<T> where T : Cacheable
    {
        #region Attributes & Properties

        public T Object { get; set; }

        public DateTime? Updated { get; set; }

        #endregion

        public CachedResponse() { }

        public CachedResponse(T obj, DateTime? updatedDate)
        {
            Object = obj;
            Updated = updatedDate;
        }
    }
}
