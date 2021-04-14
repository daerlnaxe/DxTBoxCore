using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Async_Box_Progress.Basix;
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
    public partial class DxStateProgress : Window, IGraphAs
    {

        public static readonly RoutedUICommand OkCommand = new RoutedUICommand(DxTBLang.OK, "OK", typeof(DxStateProgress));

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
