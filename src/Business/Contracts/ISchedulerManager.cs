using System;
using System.Threading.Tasks;

namespace Business.Contracts
{
    public interface ISchedulerManager : IDisposable
    {
        void Start(Func<Task> taskToExecute);
    }
}