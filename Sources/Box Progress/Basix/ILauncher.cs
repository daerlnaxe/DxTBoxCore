using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress.Basix
{
    public interface ILauncher
    {
        /*CancellationTokenSource TokenSource { get; }

        /// <summary>
        /// Token to cancel Task
        /// </summary>
        CancellationToken CancelToken { get; }*/

        /// <summary>
        /// Used to pause Task
        /// </summary>   
        bool IsPaused { get; set; }

        /// <summary>
        /// Interface human machine, like a window ...
        /// </summary>
        public IGraphAs ProgressIHM { get; set; }


        /// <summary>
        /// Define if you want to autoclose the window at the end
        /// </summary>
        bool AutoClose { get; set; }

        /// <summary>
        /// Delay before to launch
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Task runnig in background
        /// </summary>
        public Task TaskRunning { get;  }

        public void Launch();

        public void Pause(int timeSleep);

        void StopTask();


    }
}
