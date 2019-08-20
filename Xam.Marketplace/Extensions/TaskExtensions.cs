using System;
using System.Threading.Tasks;

namespace Xam.Marketplace.Extensions
{
    /// <summary>
    /// Based on brminnick AsyncAwaitBestPractices <see cref="https://github.com/brminnick/AsyncAwaitBestPractices"/>
    /// </summary>
    public static class TaskExtensions
    {
#pragma warning disable RECS0165
        public static async void SafeFireAndForget(this Task task, bool continueOnCapturedContext = true, Action<Exception> onException = null)
#pragma warning restore RECS0165
        {
            try
            {
                await task.ConfigureAwait(continueOnCapturedContext);
            }
            catch (System.Exception ex) when (onException != null)
            {
                onException.Invoke(ex);
            }
        }
    }
}
