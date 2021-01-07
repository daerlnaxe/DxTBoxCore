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
    /// Logique d'interaction pour Choose_Experiment.xaml
    /// </summary>
    public partial class Choose_Experiment : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private A_ModelChoose _model;
        /// <summary>
        /// Datacontext
        /// </summary>
        public A_ModelChoose Model
        {
            get => _model;
            set
            {
                _model = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Model)));
            }
        }

        /// <summary>
        /// Name of Save Button
        /// </summary>
        public string SaveButtonName { get; set; }

        /// <summary>
        /// Name of Cancel Button
        /// </summary>
        public string CancelButtonName { get; set; }



        public Choose_Experiment(A_ModelChoose model = null)
        {
            // Model = model;
            InitializeComponent();
            // DataContext = model;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Model.Recherche();
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

        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            
            //ContFChoose item = ((TreeViewItem)sender).DataContext as ContFChoose;
            var item = ((TreeView)sender).SelectedItem as ContFChoose;

            if (item == null)
                return;

            if (Model.Mode == ChooseMode.All)
            {
                Model.LinkResult = item.Path;
                Debug.WriteLine("All choisi");
            }
            else if (item.Type == E_IconFType.File && Model.Mode == ChooseMode.File)
            {
                Debug.WriteLine("Fichier choisi");
            }
            else if (item.Type != E_IconFType.File && Model.Mode == ChooseMode.Folder)
            {
                Debug.WriteLine("Dossier choisi");
                Model.LinkResult = item.Path;

            }
        }


        private void mee(object sender, RoutedEventArgs e)
        {
            ContFChoose item = ((TreeViewItem)sender).DataContext as ContFChoose;
            Model.Populate_Folder(item);

            // Important, évite la propagation sur les parents
            // Si non activé: redondance cyclique si couplé avec mode two way
            e.Handled = true;
        }


        private void meeclose(object sender, RoutedEventArgs e)
        {

        }

        private void PasteExecuted(object sender, ExecutedRoutedEventArgs e)
        {

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
    }
}
