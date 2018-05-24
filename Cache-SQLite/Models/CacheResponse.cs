using System;
namespace CacheSQLite.Models
{
    public class CacheResponse<T> where T : EntityBase
    {
        public T Object { get; set; }

        public DateTime Updated { get; set; }

        public CacheResponse() { }

        public CacheResponse(T obj, DateTime updatedDate)
        {
            Object = obj;
            Updated = updatedDate;
        }
    }
}
