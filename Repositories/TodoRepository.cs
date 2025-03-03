using Dapper;
using Microsoft.Data.Sqlite;

public class TodoRepository
{
    private readonly string _connectionString;

    public TodoRepository(DatabaseConfig config)
    {
        _connectionString = config.ConnectionString;
    }

    public async Task<IEnumerable<Todo>> GetAllTodos()
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryAsync<Todo>("SELECT * FROM Todos");
    }

    public async Task<Todo?> GetTodoById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.QueryFirstOrDefaultAsync<Todo>("SELECT * FROM Todos WHERE Id = @id", new { id });
    }

    public async Task<int> CreateTodo(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.ExecuteAsync(
            "INSERT INTO Todos (Name, Description, CreatedAt, DueDate, IsComplete) VALUES (@Name, @Description, @CreatedAt, @DueDate, @IsComplete)",
            todo
        );
    }

    public async Task<int> UpdateTodo(Todo todo)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.ExecuteAsync(
            "UPDATE Todos SET Name = @Name, Description = @Description, CreatedAt = @CreatedAt, DueDate = @DueDate, IsComplete = @IsComplete WHERE Id = @Id",
            todo
        );
    }

    public async Task<int> DeleteTodo(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        return await connection.ExecuteAsync("DELETE FROM Todos WHERE Id = @id", new { id });
    }
}
