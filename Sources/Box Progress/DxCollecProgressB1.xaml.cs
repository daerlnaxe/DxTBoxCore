using AsyncProgress.Basix;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Box_Progress.Basix;
using DxTBoxCore.Collec;
using DxTBoxCore.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Logique d'interaction pour DxCollecProgress.xaml
    /// </summary>
    /// <remarks>
    /// Lot of modifications in 2021, perhaps problems will exist
    /// </remarks>
    public partial class DxCollecProgressB1 : Window, IGraphAs
    {
        public bool TaskFinished { get; set; }

        public A_ProgressEphD Model { get; set; }

        public DxCollecProgressB1(A_ProgressEphD model)
        {
            Model = model;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }


        private void StopAll_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void AsyncClose()
        {
            this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
        }


    }
}
