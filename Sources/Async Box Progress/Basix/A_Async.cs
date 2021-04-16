using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    public abstract class A_Async : I_Async
    {
        public virtual Func<object> TaskToRun { get; protected set; }

        public virtual int Delay { get; set; }

        public virtual Task TaskRunning { get; protected set; }

        public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;

        public virtual bool IsPaused { get; set; }
        public virtual bool IsInterrupted { get; set; }

        public virtual bool CancelFlag { get; protected set; }

        public virtual void Launch_Task(Func<object> Ending, int delay = 50)
        {
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

        public abstract void StopTask();
    }
}
