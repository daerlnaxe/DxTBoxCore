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
    /// <remarks>You must assign a model</remarks>
    public partial class TreeChoose : Window, INotifyPropertyChanged
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

        public string LinkResult => Model.LinkResult;
 

        /// <summary>
        /// Name of Save Button
        /// </summary>
        public string SaveButtonName { get; set; }

        /// <summary>
        /// Name of Cancel Button
        /// </summary>
        public string CancelButtonName { get; set; }


        public TreeChoose(A_ModelChoose model = null)
        {
            // Model = model;
            InitializeComponent();
            Mouse.OverrideCursor = Cursors.AppStarting;

            // DataContext = model;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            Model.Recherche();
            Mouse.OverrideCursor = null;

        }

        private void SelectedItem(object sender, RoutedPropertyChangedEventArgs<object> e)
        {            
            //ContFChoose item = ((TreeViewItem)sender).DataContext as ContFChoose;
            var item = ((TreeView)sender).SelectedItem as I_ContChoose;

            if (item == null)
                return;

            if (Model.Mode == ChooseMode.All)
            {
                Model.LinkResult = item.Path;                
                Debug.WriteLine("All choisi");
            }
            else if (item.Type == E_IconFType.File && Model.Mode == ChooseMode.File)
            {
                Model.LinkResult = item.Path;                
                Debug.WriteLine("Fichier choisi");
            }           
            
            else if (item.Type != E_IconFType.File && Model.Mode == ChooseMode.Folder)
            {
                Debug.WriteLine("Dossier choisi");
                Model.LinkResult = item.Path;

            }
            /*
            else if (item.Type == E_IconFType.File && Model.Mode == ChooseMode.Folder)
            {
                e.Handled = true;
            }*/

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
            //Voir si méthode utile à créer dans le modele 
            /*
            I_ContChoose item = ((TreeViewItem)sender).DataContext as I_ContChoose;
            item.Children.Clear();
            item.Children.Add(new FolderElem(E_IconFType.Dummy) );*/
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

        // --- 

        private void btAnnul_Click(object sender, RoutedEventArgs e)
        {
            // LinkResult = null;
            DialogResult = false;
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            //   LinkResult = FilePath.Text;
            DialogResult = true;
        }

    }
}
