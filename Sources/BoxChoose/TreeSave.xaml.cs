using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
    /// Logique d'interaction pour TreeSave.xaml
    /// </summary>
    public partial class TreeSave : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private M_SaveFile _model;
        /// <summary>
        /// Datacontext
        /// </summary>
        public M_SaveFile Model
        {
            get => _model;
            set
            {
                _model = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Model)));
            }
        }

        public string LinkResult => Model.LinkResult;


        public TreeSave()
        {
            InitializeComponent();
            Mouse.OverrideCursor = Cursors.AppStarting;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Model.Recherche();
            Mouse.OverrideCursor = null;
        }

        private void StartFolder_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.G && (Keyboard.Modifiers & (ModifierKeys.Control)) == ModifierKeys.Control)
            {
                Mouse.OverrideCursor = Cursors.Wait;
                Model.Recherche();
                Mouse.OverrideCursor = null;
            }
        }

        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            //ContFChoose item = ((TreeViewItem)sender).DataContext as ContFChoose;
            var item = ((TreeView)sender).SelectedItem as I_ContChoose;

            if (item == null)
                return;

            Model.AssignValues(item.Path, item.Type);


        }




        /// <summary>
        /// Activé à l'expansion d'un élément
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void mee(object sender, RoutedEventArgs e)
        {
            I_ContChoose item = ((TreeViewItem)sender).DataContext as I_ContChoose;
            Model.Populate_Folder(item);

            // Important, évite la propagation sur les parents
            // Si non activé: redondance cyclique si couplé avec mode two way
            e.Handled = true;
        }

        private void meeclose(object sender, RoutedEventArgs e)
        {

        }

        private void btAnnul_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }


        private void btOk_Click(object sender, RoutedEventArgs e)
        {
         //   Model.LinkResult += $@"\{Model.FileValue}";
            DialogResult = true;

        }
    }
}
