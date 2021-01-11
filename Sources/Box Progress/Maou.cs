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

        public event DoubleDel UpdateProgress;
        public event StringDel UpdateStatus;
        public event DoubleDel MaximumProgress;

        public event DoubleDel UpdateProgressT;
        public event StringDel UpdateStatusT;
        public event DoubleDel MaximumProgressT;

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
            Debug.WriteLine($"{nameof(SayUpdateProgress)}: {updateProgress}");
            UpdateProgress?.Invoke(updateProgress);
        }

        /// <summary>
        /// Signal current status
        /// </summary>
        /// <param name="updateStatus"></param>
        public void SayUpdateStatus(string updateStatus)
        {
            Debug.WriteLine($"{nameof(SayUpdateStatus)}: {updateStatus}");
            UpdateStatus?.Invoke(updateStatus);
        }

        public void SayMaximumProgress(Double totalProgress)
        {
            Debug.WriteLine($"{nameof(SayMaximumProgress)}: {totalProgress}");
            MaximumProgress?.Invoke(totalProgress);
        }
        // ---

        /// <summary>
        /// Signal update progress Total
        /// </summary>
        /// <param name="updateProgressT"></param>
        public void SayUpdateProgressT(Double updateProgressT)
        {
            Debug.WriteLine($"{nameof(SayUpdateProgressT)}: {updateProgressT}");
            UpdateProgressT?.Invoke(updateProgressT);    
        }

        public void SayUpdateStatusT(string updateStatusT)
        {
            Debug.WriteLine($"{nameof(SayUpdateStatusT)}: {updateStatusT}");
            UpdateStatusT?.Invoke(updateStatusT);
        }

        /// <summary>
        /// Signal Maximum for Total
        /// </summary>
        /// <param name="totalProgressT"></param>
        public void SayMaximumProgressT(Double totalProgressT)
        {
            Debug.WriteLine($"{nameof(SayMaximumProgressT)}: {totalProgressT}");
            MaximumProgressT?.Invoke(totalProgressT);
        }


        public void StopTask()
        {
            TokenSource.Cancel();
        }


    }

}
