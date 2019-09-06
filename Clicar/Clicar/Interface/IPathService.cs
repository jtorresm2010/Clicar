namespace Clicar.Interface
{
    using SQLite;
    public interface IPathService
    {
        string GetDatabasePath();
        SQLiteConnection GetConnectionWithDatabase();
    }
}
