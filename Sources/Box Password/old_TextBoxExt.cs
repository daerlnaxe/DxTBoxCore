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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;

namespace DxTBoxCore.Box_Password
{
    /// <summary>
    /// Suivez les étapes 1a ou 1b puis 2 pour utiliser ce contrôle personnalisé dans un fichier XAML.
    ///
    /// Étape 1a) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans le projet actif.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:DxTBoxCore.Box_Password"
    ///
    ///
    /// Étape 1b) Utilisation de ce contrôle personnalisé dans un fichier XAML qui existe dans un autre projet.
    /// Ajoutez cet attribut XmlNamespace à l'élément racine du fichier de balisage où il doit 
    /// être utilisé :
    ///
    ///     xmlns:MyNamespace="clr-namespace:DxTBoxCore.Box_Password;assembly=DxTBoxCore.Box_Password"
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
    ///     <MyNamespace:TextBoxExt/>
    ///
    /// </summary>
    public class TextBoxExt : Control
    {
        //public event PropertyChangedEventHandler PropertyChanged;


        static TextBoxExt()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxExt),
                        new FrameworkPropertyMetadata(typeof(TextBoxExt)));


        }



        #region
        /*public static new readonly DependencyProperty TextProperty =
          DependencyProperty.Register(nameof(Text), typeof(string), typeof(TextBoxExt), new
              PropertyMetadata("", new PropertyChangedCallback(OnTextChanged)));
        */

       // private string _Text;

        /*
        public new string Text
        {
            get => _Text;
            set
            {
                _Text = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Text)));
            }
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxExt passExt = d as TextBoxExt;
            passExt.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            //throw new NotImplementedException();
        }*/
        #endregion
    }
}
