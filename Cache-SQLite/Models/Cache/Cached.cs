using System;

namespace CacheSQLite.Models
{
    public class Cached<T>
    {
        #region Attributes & Properties

        public T Data { get; set; }

        public DateTime LastModification { get; set; }

        #endregion

        public Cached(T data) : this(data, DateTime.Now) { }

        public Cached(T data, DateTime lastModification)
        {
            Data = data;
            LastModification = lastModification;
        }

        public string LastModificationDescription()
        {
            var prefix = "Actualizado hace";

            var now = DateTime.Now;
            var minutes = (now - LastModification).Minutes;
            var hours = (now - LastModification).Hours;
            var days = (now - LastModification).Days;

            if (minutes < 1)
            {
                return $"{prefix} menos de un minuto";
            }
            else if (minutes < 60)
            {
                return $"{prefix} {minutes} minutos";
            }
            else if (hours < 24)
            {
                return $"{prefix} {hours} horas";
            }
            else
            {
                return $"{prefix} {days} días";
            }
        }
    }
}
