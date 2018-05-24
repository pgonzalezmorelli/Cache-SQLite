using System;
using SQLite;

namespace CacheSQLite.Models
{
    public class EntityBase
    {
        [PrimaryKey, AutoIncrement]
        public Guid Id { get; set; }
    }
}
