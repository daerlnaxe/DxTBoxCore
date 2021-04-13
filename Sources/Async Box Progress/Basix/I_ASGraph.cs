using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    /// <summary>
    /// Used by windows to implement a method to safe-thread close it
    /// </summary>
    interface I_ASGraph
    {
        /// <summary>
        /// Task Name to Show
        /// </summary>
        /// <example>
        /// File
        /// </example>
        public string TaskName { get; set; }

      //  public I_RProgress Model { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public I_Async Launcher { get; set; }

        /*/// <summary>
        /// Task runnig in background
        /// </summary>
        Task TaskRunning { get; }*/

        /// <summary>
        /// Model used that will signal progress
        /// </summary>
      //  ToImp.I_ASBase Model { get; }

        /// <summary>
        /// Function to close windows without exception (see example)
        /// </summary>
        /// <example>
        /// Dispatcher.Invoke(() => this.Close());
        /// </example>
        object AsyncClose();

        /// <summary>
        /// Method to manage closing when user click to close window
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <example>
        /// e.cancel = true //block closing
        /// </example>
        void OnClosing(object sender, CancelEventArgs e);

    }
}
