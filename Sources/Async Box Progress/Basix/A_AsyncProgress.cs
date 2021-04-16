using System;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    /// <summary>
    /// Classe servant à hériter
    /// </summary>
    public abstract class A_AsyncProgress : A_ProgressEph, I_Async
    {
        public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;

        public virtual bool IsPaused { get; set; }
        public virtual bool IsInterrupted { get; set; }
        public virtual bool CancelFlag { get; protected set; }

        public virtual Func<object> TaskToRun { get; protected set; }

        public virtual int Delay { get; set; } = 10;

        public virtual Task TaskRunning { get; protected set; }



        // <summary>
        /// Launch task
        /// </summary>
        /// <param name="Ending">
        /// Method to run at the end
        /// </param>
        public virtual async void Launch_Task(Func<object> Ending, int delay = 50)
        {
            if (TaskToRun == null)
                throw new Exception("LaunchTask : you forgot to set a method to launch");
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            TaskRunning = Task.Run(
               async () =>
                    {
                        await Task.Delay(delay);
                        TaskToRun();
                    }
                    , CancelToken);

            if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }

            //await TaskRunning;

            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);
        }

        public abstract void Pause(int timeSleep);

        public virtual void StopTask()
        {
            TokenSource.Cancel();
        }
    }
}
