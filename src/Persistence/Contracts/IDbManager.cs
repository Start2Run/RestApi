using System.Collections.Generic;
using Persistence.Models;

namespace Persistence.Contracts
{
    public interface IDbManager
    {
        void Insert(DbModel model);
        IEnumerable<DbModel> GetAllData();
        void Delete(DbModel model);
    }
}