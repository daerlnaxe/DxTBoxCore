using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Used by windows to implement a method to safe-thread close it
    /// </summary>
    [Obsolete("voir I_ASGraph2")]
    interface I_ASGraph
    {
        /// <summary>
        /// Task Name to Show
        /// </summary>
        /// <example>
        /// File
        /// </example>
        public string TaskName { get; set; }

        /// <summary>
        /// Function to launch task (see example)
        /// </summary>
        /// <example>
        /// Model.Launch_Task();
        /// Model.TaskRunning.ContinueWith((ant) => this.AsyncClose());
        /// </example>
        //void Execute_Code();

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
