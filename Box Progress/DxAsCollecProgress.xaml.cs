using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    /// Logique d'interaction pour DxAsCollecProgress.xaml
    /// </summary>
    /// <remarks>
    /// Permet le traitement en asynchrone d'une tâche
    /// </remarks>
    public partial class DxAsCollecProgress : Window, I_ASGraph
    {
        public string TaskName { get; set; }


        public ModelProgressC Model { get; private set; } = new ModelProgressC();

        public I_ASBase TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.SetTaskToRun(value);
        }

        public DxAsCollecProgress(string name)
        {
            TaskName = name;

            InitializeComponent();
            DataContext = Model;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Execute_Code();
        }

        private async void Execute_Code()
        {
            //this.Show();
            Model.Launch_Task(this.AsyncClose);            
        }

        /*
        public new bool? ShowDialog()
        {
            Execute_Code();
            return base.ShowDialog();
        }*/

        public object AsyncClose()
        {
            Dispatcher.Invoke(() => this.Close());
            return null;
        }


        public void OnClosing(object sender, CancelEventArgs e)
        {
            // Fermeture normale
            if (Model.TaskRunning.Status == TaskStatus.Canceled
                || Model.TaskRunning.Status == TaskStatus.WaitingToRun
                || Model.TaskToRun.TokenSource.IsCancellationRequested)
                return;
                        
            Model.TaskToRun.IsPaused = true;
            e.Cancel = true;

            if (MBox.DxMBox.ShowDial(DxTBLang.Q_Want_Close, DxTBLang.Question, Common.DxButtons.YesNo) == true)
            {
                Model.TaskToRun.StopTask();
                Model.TaskRunning.ContinueWith(antecedant => Dispatcher.Invoke(() => this.Close()));
            }
            
            Model.TaskToRun.IsPaused = false;
        }

    }
}
