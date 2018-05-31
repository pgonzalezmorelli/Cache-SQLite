using System;

namespace CacheSQLite.Models
{
    public class Cached<T> where T : Cacheable
    {
        #region Attributes & Properties

        public T Data { get; set; }

        public DateTime? LastModification { get; set; }

        #endregion

        public Cached() : this(null, DateTime.Now) { }

        public Cached(T data) : this(data, DateTime.Now) { }

        public Cached(T data, DateTime lastModification)
        {
            Data = data;
            LastModification = lastModification;
        }

        public string LastModificationDescription()
        {
            if (!LastModification.HasValue) return null;

            var prefix = "Updated";

            var now = DateTime.Now;
            var minutes = (now - LastModification.Value).Minutes;
            var hours = (now - LastModification.Value).Hours;
            var days = (now - LastModification.Value).Days;

            if (minutes < 1)
            {
                return $"{prefix} less than a minute ago";
            }
            else if (minutes < 60)
            {
                return $"{prefix} {minutes} minutes ago";
            }
            else if (hours < 24)
            {
                return $"{prefix} {hours} hours ago";
            }
            else
            {
                return $"{prefix} {days} days ago";
            }
        }
    }
}
