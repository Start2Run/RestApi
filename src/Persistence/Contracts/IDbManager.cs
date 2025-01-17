﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Persistence.Models;

namespace Persistence.Contracts
{
    public interface IDbManager
    {
        Task Insert(DbModel model);
        Task<IEnumerable<DbModel>> GetAllData();
        Task Clear();
    }
}