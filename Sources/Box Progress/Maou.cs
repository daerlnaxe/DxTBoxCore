using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Linq;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Encapsule a method to make it compatible with progress boxes
    /// </summary>
    public class Maou<T> : I_ASBaseC
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }

        public event DoubleDel UpdateTotalProgress;
        public event StringDel UpdateTotalStatus;
        public event DoubleDel UpdateProgress;
        public event StringDel UpdateStatus;

        public Maou()
        {
            CancelToken = TokenSource.Token;
        }

        public Func<I_ASBaseC, T> ToRun { get; set; }

        public virtual object Run(int timeSleep = 10)
        {
            return ToRun(this);
        }

        /// <summary>
        /// Signal current progress
        /// </summary>
        /// <param name="updateProgress"></param>
        public void SayUpdateProgress(Double updateProgress)
        {
            UpdateProgress?.Invoke(updateProgress);
        }

        /// <summary>
        /// Signal current status
        /// </summary>
        /// <param name="updateStatus"></param>
        public void SayUpdateStatus(string updateStatus)
        {
            UpdateStatus?.Invoke(updateStatus);
        }

        /// <summary>
        /// Signale total progress
        /// </summary>
        /// <param name="totalProgress"></param>
        public void SayUpdateTotalProgress(Double totalProgress)
        {
            UpdateTotalProgress?.Invoke(totalProgress);    
        }

        public void SayUpdateTotalStatus(string updateTotalStatus)
        {
            UpdateTotalStatus?.Invoke(updateTotalStatus);
        }



        public void StopTask()
        {
            TokenSource.Cancel();
        }


    }

}
