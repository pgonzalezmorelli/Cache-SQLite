using System;
using System.IO;
using CacheSQLite.iOS;
using CacheSQLite.Repositories;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLiteClient))]
namespace CacheSQLite.iOS
{
    public class SQLiteClient: IDatabaseConnection
    {
        public SQLiteAsyncConnection GetConnection(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string fileFolder = Path.Combine(libFolder, filename);
            return new SQLiteAsyncConnection(fileFolder);
        }
    }
}
