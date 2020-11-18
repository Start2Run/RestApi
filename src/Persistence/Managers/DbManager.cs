using System.Collections.Generic;
using System.Linq;
using Persistence.Contracts;
using Persistence.Models;

namespace Persistence.Managers
{
    public class DbManager : IDbManager
    {
        public void Insert(DbModel model)
        {
            using var db = new WeatherInformationContext();
            db.Add(model);
            db.SaveChanges();
        }

        public IEnumerable<DbModel> GetAllData()
        {
            using var db = new WeatherInformationContext();
            return db.DbModels.OrderBy(b => b.Id).ToList();
        }

        public void Delete(DbModel model)
        {
            using var db = new WeatherInformationContext();
            db.Remove(model);
            db.SaveChanges();
        }
    }
}