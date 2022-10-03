using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;

namespace WebApi.Databases
{
    public class LapisDataContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public LapisDataContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("WebApi");
        }

        public IDbConnection CreateConnection()
            => new MySqlConnection(_connectionString);

        public MySqlConnection CreateMySqlConnection()
            => new MySqlConnection(_connectionString);
    }
}
