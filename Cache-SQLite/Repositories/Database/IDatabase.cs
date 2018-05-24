using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CacheSQLite.Models;

namespace CacheSQLite.Repositories
{
    public interface IDatabase<T> where T : EntityBase
    {
        Task<int> Insert(T item);

        Task<int> Delete(T item);

        Task<bool> DeleteAll();

        Task<List<T>> Select();

        Task<T> Select(Guid identifier);

        Task<List<T>> Select(string query, params object[] parameters);

        Task<T> GetFirst();

        Task<int> Update(T item);
    }
}
