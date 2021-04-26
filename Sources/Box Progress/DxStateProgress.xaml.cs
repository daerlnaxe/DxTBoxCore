using AsyncProgress;
using AsyncProgress.Basix;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Box_Progress.Basix;
using DxTBoxCore.Languages;
using System.Diagnostics;
using System.Windows;
using System.Windows.Input;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Logique d'interaction pour DxAsStateP.xaml
    /// </summary>
    public partial class DxStateProgress : Window, IGraphAs
    {
        public bool TaskFinished { get; set; }
        /*
        public I_ASBase TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.SetTaskToRun(value);
        }*/

        public I_RProgressD Model { get; set; }

        public DxStateProgress()
        {
            InitializeComponent();
        }

        public DxStateProgress(A_ProgressPersistD model)
        {
            Model = model;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

        private void CanEx_Ok(object sender, CanExecuteRoutedEventArgs e)
        {/*
            if(Model != null)
            e.CanExecute = Launcher.TaskRunning.Status == TaskStatus.RanToCompletion;*/
            e.CanExecute = TaskFinished;
        }

        private void Exec_Ok(object sender, ExecutedRoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }



        public object AsyncClose()
        {
            Debug.WriteLine("AsyncClose");
            Dispatcher.Invoke(() => this.Close());
            return null;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
