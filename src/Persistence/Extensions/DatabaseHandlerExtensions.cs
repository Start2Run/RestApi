using System.Data;
namespace Persistence.Extensions
{
    public static class DatabaseHandlerExtensions
    {
        public static void CreateTableIfNotExists(this IDbConnection connection, string tableName)
        {
            connection.Open();
            var sql = $"CREATE TABLE IF NOT EXISTS [{tableName}] (" +
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