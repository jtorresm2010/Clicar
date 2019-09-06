using Clicar.Interface;
using SQLite;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Clicar.Droid.Implementations.PathService))]
namespace Clicar.Droid.Implementations
{
    public class PathService : IPathService
    {
        public SQLiteConnection GetConnectionWithDatabase()
        {
            string filename = "Clicar.db3";
            string documentpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string path = Path.Combine(documentpath, filename);
            SQLiteConnection database = new SQLiteConnection(path);
            return database;
        }
        public string GetDatabasePath()
        {
            string path = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(path, "Clicar.db3");
        }
    }
}