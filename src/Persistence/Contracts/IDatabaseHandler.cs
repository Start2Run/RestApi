using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Contracts
{
    public interface IDatabaseHandler
    {
        bool Init();
        Task Insert(DbModel model);
        Task<IEnumerable<DbModel>> GetAllData();
        Task Clear();
    }
}