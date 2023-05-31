namespace Infrastructurre.Context;
using Microsoft.Extensions.Configuration;
using System.Data;
using Npgsql;
public class DapperContext
{
    IConfiguration _configuration;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public IDbConnection CreateConnection()
    {
        return new NpgsqlConnection(_configuration.GetConnectionString("DefaultConnection"));
    }
}
