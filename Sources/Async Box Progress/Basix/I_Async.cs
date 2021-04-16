using AsyncProgress;
using System;
using System.Threading.Tasks;

namespace DxTBoxCore.Async_Box_Progress.Basix
{

    /// <summary>
    /// Classe utilisé par les modèles
    /// </summary>
    /// <remarks>
    /// Intègre I_RProgress
    /// </remarks>
    public interface I_Async: I_ASBase
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

        /// <summary>
        /// Objet contenant la méthode
        /// </summary>
        //I_ASBase Objet { get; }

        /// <summary>
        /// Tâche à lancer
        /// </summary>
        Func<object> TaskToRun { get; }

        /// <summary>
        /// Delay before to launch
        /// </summary>
        public int Delay { get; set; }

        /// <summary>
        /// Task runnig in background
        /// </summary>
        Task TaskRunning { get; }

        public void Launch_Task(Func<object> Ending, int delay = 50);


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
