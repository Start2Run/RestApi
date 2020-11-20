using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Contracts;
using Persistence.Models;

namespace Persistence.Managers
{
    public class DatabaseManager : IDbManager
    {
        private readonly IDatabaseHandler _handler;
        public DatabaseManager(IDatabaseHandler handler)
        {
            _handler = handler;
        }

        public bool Init()
        {
            return _handler.Init();
        }

        public async Task Insert(DbModel model)
        {
            await _handler.Insert(model);
        }

        public async Task<IEnumerable<DbModel>> GetAllData()
        {
            return await _handler.GetAllData();
        }

        public async Task Clear()
        {
            await _handler.Clear();
        }
    }
}