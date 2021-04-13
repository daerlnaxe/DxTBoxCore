using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress.Basix
{
    class A_AsyncProgressLD : A_ProgressLD, I_AsyncProgress
    {
        public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;


        public virtual bool IsPaused { get; set; }

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

            await Task.Delay(delay);
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            TaskRunning = Task.Run(
                () =>
                {
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

        public virtual void StopTask()
        {
            TokenSource.Cancel();
        }
    }
}
