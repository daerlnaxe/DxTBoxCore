using DxTBoxCore.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;


namespace DxTBoxCore.Box_MBox
{
    /// <summary>
    /// Logique d'interaction pour DxCaution.xaml
    /// </summary>
    public partial class DxCaution : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public bool HideNextTime { get; set; }

        public E_DxButtons Buttons { get; set; }

        private Visibility _cancelV = Visibility.Collapsed;
        public Visibility CancelV
        {
            get => _cancelV;
            set
            {
                _cancelV = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CancelV)));
            }
        }

        private Visibility _NoV = Visibility.Collapsed;
        public Visibility NoV
        {
            get => _NoV;
            set
            {
                _NoV = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(NoV)));
            }
        }


        private Visibility _OkV = Visibility.Collapsed;
        public Visibility OkV
        {
            get => _OkV;
            set
            {
                _OkV = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(OkV)));
            }
        }

        private Visibility _YesV = Visibility.Collapsed;
        public Visibility YesV 
        { 
            get => _YesV;
            set 
            {
                _YesV = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(YesV)));
            }
        }


        public bool Result { get; private set; }
        public DxCaution()
        {
            InitializeComponent();
            DataContext = this;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Buttons.HasFlag(E_DxButtons.Ok))
                OkV = Visibility.Visible;
            if (Buttons.HasFlag(E_DxButtons.Cancel))
                CancelV = Visibility.Visible;
            if (Buttons.HasFlag(E_DxButtons.Yes))
                YesV = Visibility.Visible;
            if (Buttons.HasFlag(E_DxButtons.No))
                NoV = Visibility.Visible;
        }


        public static bool ShowModal(string file, string format, E_DxButtons buttons = E_DxButtons.Ok)
        {
            DxCaution win = new DxCaution()
            {
                Buttons = buttons
            };

            return win.ShowDialog() == true ? true : false;
        }

        public static bool ShowModal(Stream st, string format, string buttonText = null, E_DxButtons buttons = E_DxButtons.Ok)
        {

            DxCaution win = new DxCaution()
            {
                Buttons = buttons
            };

            win.LoadStream(st, format);

            #region obsolete
            /*string text = null;
            using (st)
            {

                using (StreamReader sr = new StreamReader(st))
                {

                    string s = null;
                    while ((s = sr.ReadLine()) != null)
                    {
                        text += s;
                    }
                }
            }*/
            #endregion

            win.ShowDialog();


            return win.HideNextTime;
        }


        public void LoadStream(Stream st, string format)
        {
            using (st)
            {
                TextRange range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
                range.Load(st, format);

            }
        }

        /*
        private void LoadDocument()
        {
            TextRange range;
            FileStream fStream;
            if (File.Exists(DocPath))
            {
                range = new TextRange(richTB.Document.ContentStart, richTB.Document.ContentEnd);
                fStream = new FileStream(DocPath, FileMode.OpenOrCreate);
                range.Load(fStream, DataFormats.XamlPackage);
                fStream.Close();
            }
        }
        */

        private void Ok_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


    }
}
