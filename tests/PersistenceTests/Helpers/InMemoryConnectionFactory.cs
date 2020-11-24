using System.Data;
using System.Data.SQLite;
using Persistence.Contracts;

namespace PersistenceTests.Helpers
{
    internal class InMemoryConnectionFactory : IDatabaseConnectionFactory
    {
        public IDbConnection GetConnection()
        {
            return new SQLiteConnection("Data Source=InMemorySample;Mode=Memory;Cache=Shared");
        }
    }
}