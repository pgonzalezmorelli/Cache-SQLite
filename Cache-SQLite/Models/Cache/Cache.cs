using System;

namespace CacheSQLite.Models
{
    public class Cache : EntityBase
    {
        #region Attributes & Properties

        public string Entity { get; set; }

        public string Data { get; set; }

        public DateTime? Updated { get; set; }

        #endregion

        public Cache()
        {
        }

        public Cache(string entity)
        {
            Entity = entity;
        }

        public Cache(string entity, string json, DateTime updatedDate)
        {
            Entity = entity;
            Data = json;
            Updated = updatedDate;
        }
    }
}
