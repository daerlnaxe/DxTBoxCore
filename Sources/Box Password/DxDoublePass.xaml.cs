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
using System.Windows.Shapes;
using System.ComponentModel;
using System.Collections;
using System.Linq;
using DxTBoxCore.Languages;
using System.Diagnostics;

namespace DxTBoxCore.Box_Password
{
    /// <summary>
    /// Logique d'interaction pour DxDoublePass.xaml
    /// </summary>
    public partial class DxDoublePass : Window, INotifyDataErrorInfo, INotifyPropertyChanged
    {
        #region Events

        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        /// <summary>
        /// Explanatory Note
        /// </summary>
        public string Invite { get; set; }

        /// <summary>
        /// Define min length to validate 
        /// </summary>
        public ushort PasswordLength { get; set; } = 8;


        #region Errors
        //private bool _Password1erro
        /// <summary>
        /// Binding pour le password 1
        /// </summary>
        public bool PasswordHError { get; set; }
        /// <summary>
        /// Binding pour le password 2
        /// </summary>
        public bool PasswordBError { get; set; }

        private Dictionary<string, string> _ErrorsH = new Dictionary<string, string>();
        private Dictionary<string, string> _ErrorsB = new Dictionary<string, string>();

        private void Add_Error(ref Dictionary<string, string> errorDic, string key, string value)
        {
            if (!errorDic.ContainsKey(key))
                errorDic.Add(key, value);
        }

        public bool HasErrors => _ErrorsH.Any() || _ErrorsB.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            //   return new Dictionary<string, string>() { { "mle", "kkkkkkkkkkkkkkkkkkkkadzzz" } };

            switch (propertyName)
            {
                case nameof(PasswordHError):
                    return _ErrorsH;

                case nameof(PasswordBError):
                    return _ErrorsB;
            }

            return null;
        }

        #endregion

        public DxDoublePass()
        {
            InitializeComponent();
            DataContext = this;
        }


        private bool _SubmitEnabled;
        /// <summary>
        /// Binding to enable/disable button submit
        /// </summary>
        public bool SubmitEnabled
        {
            get => _SubmitEnabled;
            set
            {
                _SubmitEnabled = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SubmitEnabled)));
            }
        }

        public static string ShowModal()
        {
            DxDoublePass window = new DxDoublePass();
            window.ShowDialog();

            string res = window.passwordBoxH.Password;

            return String.IsNullOrEmpty(res) ? null : res;
            //return null;
        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void passwordBoxH_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            Check_PasswordRules();
            Check_SimilarPass();
        }

        private void passwordBoxB_MouseLeave(object sender, MouseEventArgs e)
        {
            Keyboard.ClearFocus();
            Check_PasswordRules();
            Check_SimilarPass();
        }

        private void Check_PasswordRules()
        {
            if (passwordBoxH.Password.Length < PasswordLength)
            {
                //ErrorPassword = true;
                Add_Error(ref _ErrorsH, "Taille", DxTBLang.SizeShort + PasswordLength);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(PasswordHError)));
                SubmitEnabled = !HasErrors;

                Debug.WriteLine("signalé eeerr");
            }
            else
            {
                //ErrorPassword = false;
                _ErrorsH.Remove("Taille");
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(PasswordHError)));
                Debug.WriteLine("signalé Noneer");

                SubmitEnabled = !HasErrors;
            }
        }

        private void Check_SimilarPass()
        {
            if(passwordBoxH.Password != passwordBoxB.Password)
            {
                Add_Error(ref _ErrorsB, "Similar", DxTBLang.Pass_Different);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(PasswordBError)));
                Debug.WriteLine("Error 2 Err");

                SubmitEnabled = !HasErrors;
            }
            else
            {
                _ErrorsB.Remove("Similar");
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(PasswordBError)));
                Debug.WriteLine("Error 2 NonErr");

                SubmitEnabled = !HasErrors;
            }
         
        }


    }
}
