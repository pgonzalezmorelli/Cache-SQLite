using System;

namespace CacheSQLite.Models
{
    public class Result<T>
    {
        public T Data { get; set; }
        public DateTime LastModification { get; set; }

        public Result(T data)
        {
            Data = data;
            LastModification = DateTime.Now;
        }

        public Result(T data, DateTime lastModification)
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
