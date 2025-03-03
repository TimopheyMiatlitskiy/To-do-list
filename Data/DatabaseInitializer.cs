using Microsoft.Data.Sqlite;
using Dapper;

public static class DatabaseInitializer
{
    public static void Initialize(DatabaseConfig config)
    {
        using var connection = new SqliteConnection(config.ConnectionString);
        connection.Execute(@"
            CREATE TABLE IF NOT EXISTS Todos (
                Id INTEGER PRIMARY KEY AUTOINCREMENT,
                Name TEXT NOT NULL,
                Description TEXT,
                CreatedAt INTEGER NOT NULL,
                DueDate TEXT,
                IsComplete INTEGER NOT NULL CHECK(IsComplete IN (0, 1))
            );
        ");
    }
}
