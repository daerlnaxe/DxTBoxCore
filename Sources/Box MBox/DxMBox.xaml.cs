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

namespace DxTBoxCore.MBox
{
    /// <summary>
    /// Logique d'interaction pour MessageBox.xaml
    /// </summary>
    public partial class DxMBox : Window
    {
        //DxMBox_Res DialogResult;
        public DxButtons buttons;

        public string Image
        {
            get;
            set;
        } = "/DxTBoxCore;component/Resources/question.png";

        public string Titre
        {
            get;
            set;
        }

        public string MainMessage
        {
            get;
            set;
        }

        public string OptionnalMessage
        {
            get;
            set;
        }


        public DxMBox()
        {
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (buttons.HasFlag(DxButtons.YesNo))
            {
                btValid.Content = DxTBLang.Yes;
                btAnnul.Content = DxTBLang.No;


            }
            else if (buttons.HasFlag(DxButtons.OkCancel))
            {
                btValid.Content = "Ok";
                btAnnul.Content = DxTBLang.Cancel;
            }
            else if (buttons.HasFlag(DxButtons.Ok))
            {
                btValid.Content = "Ok";
                btAnnul.Visibility = Visibility.Collapsed;
            }
        }

        public static bool? ShowDial(string message, string title = "Information", DxButtons buttons = DxButtons.Ok, string optMessage = null)
        {
            DxMBox daBox = new DxMBox()
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
