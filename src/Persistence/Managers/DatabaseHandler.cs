using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;
using Dapper;
using Microsoft.Data.Sqlite;
using Persistence.Contracts;
using Persistence.Models;
using System.Data.SQLite;
using Common;

namespace Persistence.Managers
{
    public class DatabaseHandler : IDatabaseHandler
    {
        private readonly ConfigurationModel _configuration;
        private string _connectionString;

        public DatabaseHandler(ConfigurationModel configuration)
        {
            _configuration = configuration;
        }

        public bool Init()
        {
            try
            {
                _connectionString = $"DataSource={_configuration.DatabaseName};";
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
            await using var connection = new SqliteConnection(_connectionString);

            await connection.ExecuteAsync($"INSERT INTO {Globals.TableName} (Longitude, Latitude, Temperature, DateTime)" +
                                          "VALUES (@Longitude, @Latitude, @Temperature, @DateTime);", model);
        }

        public async Task<IEnumerable<DbModel>> GetAllData()
        {
            IEnumerable<DbModel> result = new List<DbModel>();
            try
            {
                await using var connection = new SqliteConnection(_connectionString);
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
            await using var connection = new SqliteConnection(_connectionString);
            await connection.ExecuteAsync($"Delete FROM {Globals.TableName}");
        }

        private void CreateDb()
        {
            if (System.IO.File.Exists(_configuration.DatabaseName)) return;
            Console.WriteLine("Just entered to create Sync DB");

            SQLiteConnection.CreateFile(_configuration.DatabaseName);

            using var sqLite = new SQLiteConnection(_connectionString);
            sqLite.Open();
            var sql = $"create table {Globals.TableName} (" +
                         "Longitude Real," +
                         "Latitude Real," +
                         "Temperature Real," +
                         "DateTime VARCHAR(100));";
            var command = new SQLiteCommand(sql, sqLite);
            command.ExecuteNonQuery();
        }
    }
}