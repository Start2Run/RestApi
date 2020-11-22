using System.Data;

namespace Persistence.Contracts
{
    public interface IDatabaseConnectionFactory
    {
        IDbConnection GetConnection();
    }
}