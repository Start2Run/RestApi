using System;
using System.Reactive.Linq;
using System.Threading.Tasks;
using Business.Contracts;
using Common.Models;

namespace Business.Managers
{
    public class SchedulerManager : ISchedulerManager
    {
        private readonly ConfigurationModel _configuration;
        private IDisposable _timer;

        public SchedulerManager(ConfigurationModel configuration)
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