public class DatabaseConfig
{
    public string ConnectionString { get; }

    public DatabaseConfig(IConfiguration configuration)
    {
        ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
    }
}
