using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Box_Collec
{
    /// <summary>
    /// Window to show elements to select/remove
    /// </summary>
    public partial class BCElements : Window
    {
        public MCElements Model { get; set; }

        public static readonly RoutedUICommand AllCommand = new RoutedUICommand("All", "AllCommand", typeof(BCElements));
        public static readonly RoutedUICommand NoneCommand = new RoutedUICommand("None", "NoneCommand", typeof(BCElements));


        public BCElements()
        {
            InitializeComponent();

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
        }

  
  

        private void RemoveChosen_DblClick(object sender, MouseButtonEventArgs e)
        {
            string element =(string)((ListViewItem)sender).Tag;
            Model.ChosenElements.Remove(element);
            Model.DiscardedElements.Add(element);
        }

        private void RemoveDicarded_DblClick(object sender, MouseButtonEventArgs e)
        {
            string element =(string)((ListViewItem)sender).Tag;
            Model.DiscardedElements.Remove(element);
            Model.ChosenElements.Add(element);

        }

        private void Can_All(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Model != null)
            e.CanExecute = Model.DiscardedElements.Any();
        }

        private void Exec_All(object sender, ExecutedRoutedEventArgs e)
        {
            Model.ChooseAll();
        }

        private void Can_None(object sender, CanExecuteRoutedEventArgs e)
        {
            if(Model!= null)
            e.CanExecute = Model.ChosenElements.Any();
        }

        private void Exec_None(object sender, ExecutedRoutedEventArgs e)
        {
            Model.RemoveAllChosen();

        }

        private void Validate_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }
    }
}
