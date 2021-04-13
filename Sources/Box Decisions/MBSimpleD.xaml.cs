using DxTBoxCore.Languages;
using DxTBoxCore.Common;
using System;
using System.Collections.Generic;
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

namespace DxTBoxCore.Box_Decisions
{
    /// <summary>
    /// Logique d'interaction pour MessageBox.xaml
    /// </summary>
    [Obsolete]
    public partial class MBSimpleD : Window
    {
        //DxMBox_Res DialogResult;
        public E_DxButtons buttons;

        public string Image
        {
            get;
            set;
        } = "/DxTBoxCore;component/Resources/question.png";


        public string MainMessage { get; set; }

        public string OptionnalMessage { get; set; }
        
        public static bool All { get; set; }
        /*
        public Visibility AnnulVisible 
        { 
            get;
            set;
        } = Visibility.Collapsed;*/

        public MBSimpleD()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (buttons.HasFlag(E_DxButtons.Yes))
                btValid.Content = DxTBLang.Yes;

            if (buttons.HasFlag(E_DxButtons.Ok))
                btValid.Content = DxTBLang.OK;

            // ---

            if (buttons.HasFlag(E_DxButtons.No))
            {
                btAnnul.Content = DxTBLang.No;
                btAnnul.Visibility = Visibility.Visible;
            }

            if (buttons.HasFlag(E_DxButtons.Cancel))
            {
                btAnnul.Content = DxTBLang.Cancel;
                btAnnul.Visibility = Visibility.Visible;
            }
        }

        public static bool? ShowDial(string message, string title = "Information", E_DxButtons buttons = E_DxButtons.Ok, string optMessage = null)
        {
            MBSimpleD daBox = new MBSimpleD()
            {
                Title = title,
                MainMessage = message,
                OptionnalMessage = optMessage
            };
            //daBox.Title = title;
            //daBox.MessagePart.Text = message;
            daBox.buttons = buttons;

            return daBox.ShowDialog();
        }       


        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


    }



}
