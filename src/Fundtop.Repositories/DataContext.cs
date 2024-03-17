using Dapper;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using System.Data;
using System.Data.SQLite;

namespace Fundtop.Repositories
{
    public class DataContext : IDisposable
    {
        private readonly IConfiguration _configuration;
        private readonly IDbConnection _connection;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;

            var databaseDefault = _configuration["Database:Default"];
            var databaseConnection = _configuration[$"Database:Connection:{databaseDefault}"];
            switch (databaseDefault.ToUpper())
            {
                case "MYSQL":
                    _connection = new MySqlConnection(databaseConnection);
                    break;
                case "SQLITE":
                    DefaultTypeMap.MatchNamesWithUnderscores = true;
                    _connection = new SQLiteConnection(databaseConnection);
                    break;
                default:
                    throw new NotSupportedException($"Database type {databaseDefault} not supported.");
            }

            try
            {
                _connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Database connection error:{ex.Message}");
            }
        }

        public IDbConnection GetConnection()
        {
            return _connection;
        }

        public void Dispose()
        {
            if (_connection.State != ConnectionState.Closed)
            {
                _connection.Close();
            }
            _connection.Dispose();
        }
    }
}
