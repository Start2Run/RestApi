using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiConnection.Contracts;
using Business.Contracts;
using Common.Models;
using Common.Models.RestApi;
using Newtonsoft.Json;
using Persistence.Contracts;
using Persistence.Models;

namespace Business.Managers
{
    public class MainManager : IMainManager
    {
        private readonly IRequestManager _requestManager;
        private readonly ISchedulerManager _schedulerManager;
        private readonly ConfigurationModel _configuration;
        private readonly IDbManager _dbManager;

        public MainManager(IRequestManager requestManager, ISchedulerManager schedulerManager, IDbManager dbManager, ConfigurationModel configuration)
        {
            _requestManager = requestManager;
            _schedulerManager = schedulerManager;
            _dbManager = dbManager;
            _configuration = configuration;
        }

        public void Init()
        {
            _schedulerManager.Start(DoWorkAsync);
        }

        private async Task DoWorkAsync()
        {
            var model = await _requestManager.GetRequestAsync(_configuration);

            var dbModels = GetDbModelFromRequest(model);

            try
            {
                foreach (var dbModel in dbModels)
                {
                    _dbManager.Insert(dbModel);
                    Console.WriteLine(JsonConvert.SerializeObject(dbModel, Formatting.Indented));
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static IEnumerable<DbModel> GetDbModelFromRequest(Root root)
        {
            return root == null ? new List<DbModel>() : root.data.Select(data => new DbModel() { Latitude = data.lat, Longitude = data.lon, Temperature = data.temp, DateTime = DateTime.Now});
        }

        public void Dispose()
        {
            _schedulerManager?.Dispose();
        }
    }
}