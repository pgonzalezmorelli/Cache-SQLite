using System;
using System.IO;
using CacheSQLite.Droid;
using CacheSQLite.Repositories;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteClient))]
namespace CacheSQLite.Droid
{
    public class SQLiteClient: IDatabaseConnection
    {
        public SQLiteAsyncConnection GetConnection(string filename)
        {
            string documentsPath = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            var fullPath = Path.Combine(documentsPath, filename);
            var conn = new SQLiteAsyncConnection(fullPath);
            return conn;
        }
    }
}
