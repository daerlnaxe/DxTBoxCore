using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{
    interface I_AsyncProgress
    {               
        /*
        /// <summary>
        /// Function to launch in background
        /// </summary>
        /// <remarks>
        /// Need a Cancellation token and a boolean to pause function
        /// </remarks>
        Func<CancellationToken, bool, object> TaskToRun { get; }
        */

        I_ASBase TaskToRun { get; }        

        /// <summary>
        /// Task runnig in background
        /// </summary>
        Task TaskRunning { get; }

        /// <summary>
        /// Call Token to stop task
        /// </summary>
        // void StopTask();

        /// <summary>
        /// Launch task
        /// </summary>
        // void Launch_Task();


    }
}
