using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading;
using System.Windows;
using System.Windows.Threading;

namespace DxTBoxCore.Box_Progress
{
    public delegate void DoubleDel(Double value);
    public delegate void StringDel(string value);

    /// <summary>
    /// Base class to implement in object that will be used for Tasks to make compatible with progression windows
    /// </summary>
    public interface I_ASBase
    {
        /// <summary>
        /// Say progress
        /// </summary>
        public event DoubleDel UpdateProgress;

        /// <summary>
        /// Says current part of Task
        /// </summary>
        public event StringDel UpdateStatus;

        CancellationTokenSource TokenSource { get; }

        /// <summary>
        /// Token to cancel Task
        /// </summary>
        CancellationToken CancelToken { get; }

        /// <summary>
        /// Used to pause Task
        /// </summary>
        bool IsPaused { get; set; }

        /// <summary>
        /// Run the method
        /// </summary>
        /// <param name="timeSleep"></param>
        /// <returns>
        /// Your type of object
        /// </returns>
        object Run(int timeSleep = 10);

        void StopTask();


        public void Pause(int timeSleep = 100)
        {
            while (IsPaused)
                Thread.Sleep(100);
        }
    }
}
