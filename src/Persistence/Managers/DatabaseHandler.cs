using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Dapper;
using Persistence.Contracts;
using Persistence.Models;
using System.Data.SQLite;
using Common;

namespace Persistence.Managers
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly IDatabaseConnectionFactory _connectionFactory;
        private readonly ConfigurationModel _configuration;

        public DatabaseHandler(IDatabaseConnectionFactory connectionFactory, ConfigurationModel configuration)
        {
            _configuration = configuration;
            _connectionFactory = connectionFactory;
        }

        public bool Init()
        {
            try
            {
                CreateDb();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }

            return true;
        }

        public async Task Insert(DbModel model)
        {
            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(
                $"INSERT INTO {Globals.TableName} (Longitude, Latitude, Temperature, DateTime)" +
                "VALUES (@Longitude, @Latitude, @Temperature, @DateTime);", model);
        }

        public async Task<IEnumerable<DbModel>> GetAllData()
        {
            IEnumerable<DbModel> result = new List<DbModel>();
            try
            {
                using var connection = _connectionFactory.GetConnection();
                result = await connection.QueryAsync<DbModel>($"SELECT rowid AS Id, Longitude, Latitude, Temperature, DateTime FROM {Globals.TableName};");
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            return result;
        }

        public async Task Clear()
        {
            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync($"Delete FROM {Globals.TableName}");
        }

        private void CreateDb()
        {
            if (System.IO.File.Exists(_configuration.DatabaseName)) return;
            Console.WriteLine("Just entered to create Sync DB");

            SQLiteConnection.CreateFile(_configuration.DatabaseName);

            using var connection = _connectionFactory.GetConnection();
            connection.Open();
            var sql = $"create table {Globals.TableName} (" +
                         "Longitude Real," +
                         "Latitude Real," +
                         "Temperature Real," +
                         "DateTime VARCHAR(100));";
            var command = connection.CreateCommand();
            command.CommandText = sql;
            command.ExecuteNonQuery();
        }
    }
}