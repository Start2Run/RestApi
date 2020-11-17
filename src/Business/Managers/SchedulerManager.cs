using System;
using System.Configuration;
using System.Reactive.Linq;
using System.Threading.Tasks;
using ApiConnection.Contracts;
using Business.Contracts;
using Common.Models;
using Newtonsoft.Json;

namespace Business.Managers
{
    public class SchedulerManager: ISchedulerManager
    {
        private IRequestManager _requestManager;
        private ConfigurationModel _configuration;
        private IDisposable _timer;

        public SchedulerManager(IRequestManager requestManager, ConfigurationModel configuration)
        {
            _requestManager = requestManager;
            _configuration = configuration;

        }
        public void Start()
        {
            _timer = Observable
                .Interval(TimeSpan.FromSeconds(_configuration.PullIntervalInSeconds))
                .Subscribe(async x => await DoWork());
        }

        private async Task DoWork()
        {
            var model = await _requestManager.GetRequestAsync(_configuration);
            Console.WriteLine(JsonConvert.SerializeObject(model, Formatting.Indented));
        }
    }
}