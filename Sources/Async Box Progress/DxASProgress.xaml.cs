using AsyncProgress;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Async_Box_Progress
{
    /// <summary>
    /// Logique d'interaction pour W_ASProgress.xaml
    /// </summary>
    [Obsolete]

    public partial class DxAsProgress : Window, I_ASGraph
    {
        #region Hide Close button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public bool HideCloseButton { get; set; }
        #endregion

        /// <summary>
        /// Task Name to Show
        /// </summary>
        /// <example>
        /// File
        /// </example>
        public string TaskName { get; set; }

        public I_RProgress Model { get; set; }

        public I_Async Launcher { get; set; }



        public DxAsProgress()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Model == null)
                throw new ArgumentNullException(nameof(Model));

            if (HideCloseButton)
            {
                // Remove close button
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            }


            DataContext = Model;

            Launcher.Launch_Task(this.AsyncClose);
        }

        public object AsyncClose()
        {
            Debug.WriteLine("AsyncClose");
            Dispatcher.Invoke(
                () =>
                {
                    DialogResult = true;
                    Debug.WriteLine("Dialog result set to true");
                    this.Close();
                }
                );

            return null;
        }


        public void OnClosing(object sender, CancelEventArgs e)
        {
            Debug.WriteLine("On closing");
            if (Launcher.TaskRunning.Status == TaskStatus.Faulted)
            {
                MBox.DxMBox.ShowDial(Launcher.TaskRunning.Exception.Message, DxTBLang.Error);
                DialogResult = false;
                return;
            }

            // Fermeture normale
            if (Launcher.TaskRunning.Status == TaskStatus.Canceled
                || Launcher.TaskRunning.Status == TaskStatus.RanToCompletion
                || Launcher.TokenSource.IsCancellationRequested)
            {
                DialogResult = true;
                return;
            }

            Launcher.IsPaused = true;
            e.Cancel = true;

            if (MBox.DxMBox.ShowDial(DxTBLang.Q_Want_Close, DxTBLang.Question, E_DxButtons.Yes | E_DxButtons.No) == true)
            {
                Launcher.StopTask();
                Launcher.TaskRunning.ContinueWith(antecedant => Dispatcher.Invoke(() => this.Close()));
                DialogResult = false;
            }

            Launcher.IsPaused = false;
        }
    }
}
