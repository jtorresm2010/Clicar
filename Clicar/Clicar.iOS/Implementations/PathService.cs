using Clicar.Interface;
using SQLite;
using System;
using System.IO;

[assembly: Xamarin.Forms.Dependency(typeof(Clicar.iOS.Implementations.PathService))]
namespace Clicar.iOS.Implementations
{
    public class PathService : IPathService
    {
        public SQLiteConnection GetConnectionWithDatabase()
        {
            string filename = "Clicar.db3";
            string documentpath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(documentpath, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            string pathdb = Path.Combine(libFolder, filename);
            SQLiteConnection database = new SQLiteConnection(pathdb);
            return database;
        }
        public string GetDatabasePath()
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Databases");
            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }
            return Path.Combine(libFolder, "Clicar.db3");
        }
    }
}