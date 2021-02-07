using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DxTBoxCore.Controls
{
    /// <summary>
    /// Suivez les étapes 1a ou 1b puis 2 pour utiliser ce contrôle personnalisé dans un fichier XAML.
    ///
    /// Étape 1a) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans le projet actif.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:DxTBoxCore.Controls"
    ///
    ///
    /// Étape 1b) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans un autre projet.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:DxTBoxCore.Controls;assembly=DxTBoxCore.Controls"
    ///
    /// Vous devrez également ajouter une référence du projet contenant le fichier XAML
    /// à ce projet et regénérer pour éviter des erreurs de compilation :
    ///
    ///     Cliquez avec le bouton droit sur le projet cible dans l'Explorateur de solutions, puis sur
    ///     "Ajouter une référence"->"Projets"->[Recherchez et sélectionnez ce projet]
    ///
    ///
    /// Étape 2)
    /// Utilisez à présent votre contrôle dans le fichier XAML.
    ///
    ///     <MyNamespace:Textbox_AC/>
    ///
    /// </summary>
    [TemplatePart(Name = Textbox_AC.ElementWriteText, Type = typeof(TextBox))]
    [TemplatePart(Name = Textbox_AC.ElementAvailableItems, Type = typeof(ListBox))]
    [TemplatePart(Name = Textbox_AC.ElementFilteredItems, Type = typeof(ListBox))]
    public class Textbox_AC : ItemsControl
    {
        ObservableCollection<string> SelectedElements { get; set; } = new ObservableCollection<string>();

        #region const
        private const string ElementWriteText = "PART_WriteText";
        private const string ElementAvailableItems = "Part_AvailableItems";
        private const string ElementFilteredItems = "Part_FilteredItems";
        #endregion const

        #region datas
        private TextBox _WriteTextTextBox;
        private ListBox _HiddenListBox;
        #endregion datas

        static Textbox_AC()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(Textbox_AC),
                new FrameworkPropertyMetadata(typeof(Textbox_AC)));
        }


        public Textbox_AC()
        {
            //this.IsReadOnly = false;

            //base.Text = "testc";
            /*
            SelectedElements = new ObservableCollection<string>()
            {
                "Toto",
                "Tata",
                "Titi",
                "Tete"
            };*/
            // Graphical_Cons();
            
        }

        

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _WriteTextTextBox = GetTemplateChild(ElementWriteText) as TextBox;
            _HiddenListBox = GetTemplateChild(ElementAvailableItems) as ListBox;
            _HiddenListBox = GetTemplateChild(ElementFilteredItems) as ListBox;
        }




            private void Graphical_Cons()
        {
            // popup
            Popup pop = new Popup()
            {
                PlacementTarget = this,
                Placement = PlacementMode.Bottom,
                IsOpen = true,
            };

            // content
            ListBox lb = new ListBox()
            {
                ItemsSource = SelectedElements
            };

            //
            pop.Child = lb;
        }

    }
}
