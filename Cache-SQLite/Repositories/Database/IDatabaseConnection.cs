using System;
using SQLite;

namespace CacheSQLite.Repositories
{
    public interface IDatabaseConnection
    {
        SQLiteAsyncConnection GetConnection(string path);
    }
}
