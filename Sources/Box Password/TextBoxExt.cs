using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
    public class TextBoxExt : TextBox//, INotifyPropertyChanged
    {
        // public event PropertyChangedEventHandler PropertyChanged;

        [Obsolete("Ne fonctionne pas encore comme souhaité")]
        static TextBoxExt()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof(TextBoxExt),
                new FrameworkPropertyMetadata(typeof(TextBoxExt)));
        }

        #region Text
        public new static readonly DependencyProperty TextProperty =
            DependencyProperty.Register("Text", typeof(string), typeof(TextBoxExt), new
                PropertyMetadata(string.Empty, new PropertyChangedCallback(OnTextChanged)));


        //private string _TextField;

        public new string Text
        {
            get => (string)GetValue(TextProperty);
            /*set
            {
                SetValue(TextProperty, value);
            }*/
        }

        private async void KillIt()
        {
            for (int i = 0; i < 5; i++)
            {
                await Task.Delay(1000);
            }
            SetValue(TextProperty, null);
        }

        private static void OnTextChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxExt passExt = d as TextBoxExt;
            passExt.OnTextChanged(e);
        }

        private void OnTextChanged(DependencyPropertyChangedEventArgs e)
        {
            //Text = (string)e.NewValue;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// Normalement consomme
        /// </remarks>
        public void SetText(ref string value)
        {
            SetValue(TextProperty, value);
            value = null;
            KillIt();
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof( Text)));
        }
        #endregion

        #region Timer


        // Register ZHeightProperty dependency property
        /*private static FrameworkPropertyMetadata meta = 
            new FrameworkPropertyMetadata(15,   // default = 1
            FrameworkPropertyMetadataOptions.AffectsArrange | FrameworkPropertyMetadataOptions.AffectsMeasure |
                FrameworkPropertyMetadataOptions.AffectsParentArrange | FrameworkPropertyMetadataOptions.AffectsParentMeasure
                );*/

        public static readonly DependencyProperty AgeProperty =
            DependencyProperty.RegisterAttached("Age", typeof(int), typeof(TextBoxExt)); 

        static PropertyMetadata ageMetadata =
            new PropertyMetadata(0, null, new CoerceValueCallback(CoerceAge));

        private static object CoerceAge(DependencyObject d, object baseValue)
        {
            //throw new NotImplementedException();
            return 15;
        }

        public static void SetAge(DependencyObject depObj, int value)
        {
            depObj.SetValue(AgeProperty, value);
        }

        
        //public static readonly DependencyProperty DelayProperty2 = new atta
        /*
        [Description("Maximum delay before erasing"), Category("Security")]
        [DefaultValue(25)]
        public int Delay
        {
            get => (int)GetValue(DelayProperty);
            set => SetValue(DelayProperty, value);
        }*/

        private static void OnDelayChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            TextBoxExt passExt = d as TextBoxExt;
            passExt.OnDelayChanged(e);
        }

        private void OnDelayChanged(DependencyPropertyChangedEventArgs e)
        {
            // throw new NotImplementedException();
        }

        public static readonly DependencyProperty TickBrushProperty =
    DependencyProperty.RegisterAttached(
        "TickBrush",
        typeof(Brush),
        typeof(TextBoxExt),
        new FrameworkPropertyMetadata(Brushes.Black));

        #endregion Timer;
    }
}
