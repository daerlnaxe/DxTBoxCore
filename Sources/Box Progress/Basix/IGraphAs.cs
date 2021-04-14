using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows;
using System.Windows.Threading;

namespace DxTBoxCore.Box_Progress.Basix
{
    public interface IGraphAs
    {
        public event RoutedEventHandler Loaded;
        public event CancelEventHandler Closing;
        public event EventHandler Closed;

        public Dispatcher Dispatcher { get; }

        /// <summary>
        /// To know when task is finished
        /// </summary>
        bool TaskFinished { get; set; }

        public void Close();
    }
}
