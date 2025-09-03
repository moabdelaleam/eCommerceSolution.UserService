using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Data;

namespace eCommerce.Infrastructure.DbContext;

internal class DapperDbContext
{
    private readonly IConfiguration _configuration;
    private readonly IDbConnection _connection;
    public DapperDbContext(IConfiguration configuration)
    {
        _configuration = configuration;
        string? connectionStr5ing = _configuration.GetConnectionString("PostgresConnection");

        // Create a new NpgsqlConnection with the retrieved connection string
        _connection = new NpgsqlConnection(connectionStr5ing);
    }

    public IDbConnection DbConnection => _connection;
}