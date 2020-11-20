using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Common.Models;

namespace Business.Managers
{
    public class SchedulerManager : ISchedulerManager
    {
        private readonly IConfigurationModel _configuration;
        private IDisposable _timer;

        public SchedulerManager(IConfigurationModel configuration)
        {
            _configuration = configuration;
        }
        public void Start(Func<Task> taskToExecute)
        {
            _timer = Observable
                .Interval(TimeSpan.FromSeconds(_configuration.PullIntervalInSeconds))
                .Subscribe(async x => await taskToExecute.Invoke());
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}