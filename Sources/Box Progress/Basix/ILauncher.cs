using AsyncProgress;
using DxTBoxCore.Async_Box_Progress.Basix;
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

        public I_ASBase Objet { get;  }


        /// <summary>
        /// Define if you want to autoclose the window at the end
        /// </summary>
        bool AutoCloseWindow { get; set; }

        /// <summary>
        /// Show if ihm is loaded
        /// </summary>
        public bool IHMLaunched { get; }

        /// <summary>
        /// Delay before to launch
        /// </summary>
        public int LoopDelay { get; set; }


        /// <summary>
        /// Task runnig in background
        /// </summary>
        public Task TaskRunning { get;  }

        public bool? Launch(I_ASBase objet);

        public void Pause(int timeSleep);

        void StopTask();


    }
}
