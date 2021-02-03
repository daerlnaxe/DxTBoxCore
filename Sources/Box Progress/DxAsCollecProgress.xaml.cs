using DxTBoxCore.Common;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        #region No signal
        public string TaskName { get; set; }

        /// <summary>
        /// Maximum for the current bar progress
        /// </summary>
        public int MaxCP { get; set; } = 100;

        /// <summary>
        /// Maximum for the total bar progress
        /// </summary>
        public int MaxTP { get; set; } = 100;

        #endregion

        public M_ProgressC Model { get; private set; } = new ModelProgressC();

        
        public I_ASBase TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.SetTaskToRun(value);
        }


        public DxAsCollecProgress(string name)
        {
            TaskName = name;

            InitializeComponent();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Execute_Code();
        }

        private async void Execute_Code()
        {
  
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
            Debug.WriteLine("AsyncClose");
            Dispatcher.Invoke(() => this.Close());
            return null;
        }


        public void OnClosing(object sender, CancelEventArgs e)
        {
            if(Model.TaskRunning.Status == TaskStatus.Faulted)
            {
                MBox.DxMBox.ShowDial(Model.TaskRunning.Exception.Message, DxTBLang.Error);
                DialogResult = false;
                return;
            }

            // Fermeture normale
            if (Model.TaskRunning.Status == TaskStatus.Canceled
                || Model.TaskRunning.Status == TaskStatus.RanToCompletion
                || Model.TaskToRun.TokenSource.IsCancellationRequested)
            {
                DialogResult = true;
                return;
            }
                        
            Model.TaskToRun.IsPaused = true;
            e.Cancel = true;

            if (MBox.DxMBox.ShowDial(DxTBLang.Q_Want_Close, DxTBLang.Question, E_DxButtons.Yes | E_DxButtons.No) == true)
            {
                Model.TaskToRun.StopTask();
                Model.TaskRunning.ContinueWith(antecedant => Dispatcher.Invoke(() => this.Close()));
                DialogResult = false;
            }
            
            Model.TaskToRun.IsPaused = false;
        }

    }
}
