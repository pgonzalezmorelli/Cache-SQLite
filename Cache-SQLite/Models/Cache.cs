using System;
using SQLite;

namespace CacheSQLite.Models
{
    public class Cache : EntityBase
    {
        public string Entity { get; set; }

        public string Data { get; set; }

        public DateTime Updated { get; set; }

        public Cache(string entity){
            Entity = entity;
        }

        public Cache()
        {
        }

        public Cache(string entity, string json, DateTime updatedDate){
            Id = Guid.NewGuid();
            Entity = entity;
            Data = json;
            Updated = updatedDate;
        }
    }
}
