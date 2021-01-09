using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Encapsule a method to make it compatible with progress boxes
    /// </summary>
    public class Maou<T> : I_ASBaseC
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken { get; }

        public bool IsPaused { get ; set; }

        public event DoubleDel UpdateTotalProgress;
        public event StringDel UpdateTotalStatus;
        public event DoubleDel UpdateProgress;
        public event StringDel UpdateStatus;

        public Maou()
        {
            CancelToken = TokenSource.Token;
        }

        public Func<I_ASBase, T> ToRun{ get; set; }

        public virtual object Run(int timeSleep = 10)
        {
            return ToRun(this);
        }

        public void StopTask()
        {           
            TokenSource.Cancel();
        }


    }
 
}
