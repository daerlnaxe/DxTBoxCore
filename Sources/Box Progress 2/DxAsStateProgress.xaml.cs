using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Progress.Basix;
using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Logique d'interaction pour DxAsStateP.xaml
    /// </summary>
    public partial class DxAsStateProgress : Window, I_ASGraph
    {

        public string TaskName { get; set; }

        public static readonly RoutedUICommand OkCommand = new RoutedUICommand(DxTBLang.OK, "OK", typeof(DxAsStateProgress));

        /*
        public I_ASBase TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.SetTaskToRun(value);
        }*/

        public bool AutoClose { get; set; } = false;

        I_RProgressLD Model { get; set; }

        public I_AsyncProgress Launcher { get; set; }

        public DxAsStateProgress()
        {
            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            if (AutoClose)
                Launcher.Launch_Task(Ending: this.AsyncClose);
            else
                Launcher.Launch_Task(null);
        }

        private void CanEx_Ok(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Model != null)
            e.CanExecute = Launcher.TaskRunning.Status == TaskStatus.RanToCompletion;
        }

        private void Exec_Ok(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            Launcher.TokenSource.Cancel();
        }


        public object AsyncClose()
        {
            Debug.WriteLine("AsyncClose");
            Dispatcher.Invoke(() => this.Close());
            return null;
        }


        public void OnClosing(object sender, CancelEventArgs e)
        {
            if (Launcher.TaskRunning.Status == TaskStatus.Faulted)
            {
                MBox.DxMBox.ShowDial(Launcher.TaskRunning.Exception.Message, DxTBLang.Error);
                DialogResult = false;
                return;
            }

            // Fermeture normale
            if (Launcher.TaskRunning.Status == TaskStatus.Canceled
                || Launcher.TaskRunning.Status == TaskStatus.RanToCompletion
                || Launcher.CancelToken.IsCancellationRequested)
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

        private void Window_StateChanged(object sender, EventArgs e)
        {

        }
    }
}
