using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// (Pas terminé)
    /// </summary>
    /// <remarks>
    /// Permet de lancer une tâche asynchrone en fond
    /// </remarks>

    public partial class DxAsProgress : Window, I_ASGraph
    {

        public ModelProgress Model
        {
            get;
            private set;
        } = new ModelProgress();


        /// <summary>
        /// Task Name to Show
        /// </summary>
        /// <example>
        /// File
        /// </example>
        public string TaskName { get; set; }

        /// <summary>
        /// Function to launch in background
        /// </summary>
       /* public Func<CancellationToken, bool, object> TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.TaskToRun = value;
        }*/

        public I_ASBase TaskToRun
        {
            get => Model.TaskToRun;
            set => Model.SetTaskToRun(value);
        }

        /*
        /// <summary>
        /// Update Progress
        /// </summary>
        /// <remarks>
        /// Compatible Asynchrone normalement
        /// </remarks>
        /// 
        public double AsyncProgress
        {
            get => Model.CurrentProgress;
            set => Model.CurrentProgress = value;

        /// <summary>
        /// Update current operation
        /// </summary>
        public string CurrentOP
        {
            get => Model.CurrentOP;
            set => Model.CurrentOP = value;
        }
        }*/

        // private bool _finished;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="name">Name to give to TaskName</param>
        public DxAsProgress(string name)
        {
            TaskName = name;

            InitializeComponent();
            DataContext = Model;
        }


        public void Execute_Code()
        {
            Model.Launch_Task(this.AsyncClose);
        //    Model.TaskRunning.ContinueWith((ant) => this.AsyncClose());
        }

        
        public object AsyncClose()
        {
            //_finished = true;
            Dispatcher.Invoke(() => this.Close());
            return null;
        }


        public void OnClosing(object sender, CancelEventArgs e)
        {
            // Fermeture normale
            if (Model.TaskRunning.Status != TaskStatus.Running || Model.TaskToRun.TokenSource.IsCancellationRequested)
                return;

            //Model.IsPaused = true;
            Model.TaskToRun.IsPaused = true;
            e.Cancel = true;

            if (MBox.DxMBox.ShowDial(DxTBLang.Q_Want_Close, DxTBLang.Question, Common.DxButtons.YesNo) == true)
            {
                Model.TaskToRun.StopTask();
                Model.TaskRunning.ContinueWith(antecedant => Dispatcher.Invoke(() => this.Close()));

            }
            //Model.IsPaused = false;
            Model.TaskToRun.IsPaused = false;
            /*
            //tokenSource2.Cancel();
            if (!_finished)
            {
                e.Cancel = true;

                Model.TaskRunning.ContinueWith(antecedant => this.AsyncClose());
            }*/
        }

    }
}
