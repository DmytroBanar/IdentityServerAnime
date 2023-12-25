using System.Data;
using Npgsql;

namespace WebApplication1.Context
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PostgresConnection"); // Переконайтеся, що ви маєте правильну назву підключення в appsettings.json
        }
        public IDbConnection CreateConnection()
        => new NpgsqlConnection(_connectionString);
    }
}
