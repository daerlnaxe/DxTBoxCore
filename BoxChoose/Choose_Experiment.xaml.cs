using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.BoxChoose
{
    /// <summary>
    /// Logique d'interaction pour Choose_Experiment.xaml
    /// </summary>
    public partial class Choose_Experiment : Window
    {
        public A_ModelChoose Model { get; set; } = new A_ModelChoose();

        /// <summary>
        /// Name of Save Button
        /// </summary>
        public string SaveButtonName { get; set; }

        /// <summary>
        /// Name of Cancel Button
        /// </summary>
        public string CancelButtonName { get; set; }

        public Choose_Experiment()
        {  
            InitializeComponent();
            DataContext = Model;
        }









        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            //   LinkResult = FilePath.Text;
            DialogResult = true;
        }

        private void btAnnul_Click(object sender, RoutedEventArgs e)
        {
            // LinkResult = null;
            DialogResult = false;
        }

        private void mee(object sender, RoutedEventArgs e)
        {
            ContFChoose item = ((TreeViewItem)sender).DataContext as ContFChoose;
            Model.Populate_Folder(item);
        }

        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

        }
    }
}
