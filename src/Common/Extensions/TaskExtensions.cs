using System.Threading.Tasks;

namespace Common.Extensions
{
    public static class TaskExtensions
    {
        public static void FireAndForget(this Task task)
        {
            if (!task.IsCompleted || task.IsFaulted)
            {
                _ = ForgetAwaited(task);
            }

            async static Task ForgetAwaited(Task task)
            {
                try
                {
                    await task.ConfigureAwait(false);
                }
                catch
                {
                    // Nothing to do here
                }
            }
        }
    }
}
