using System.Data;
using Common.Models;
using Microsoft.Data.Sqlite;
using Persistence.Contracts;

namespace Persistence.Managers
{
    public class DatabaseConnectionFactory : IDatabaseConnectionFactory
    {
        private readonly IConfigurationModel _configuration;

        public DatabaseConnectionFactory(IConfigurationModel configuration)
        {
            _configuration = configuration;
        }

        public IDbConnection GetConnection()
        {
            return new SqliteConnection($"DataSource={_configuration.DatabaseName};");
        }
    }
}