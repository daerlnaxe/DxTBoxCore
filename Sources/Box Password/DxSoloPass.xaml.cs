using DxTBoxCore.Languages;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Box_Password
{
    /// <summary>
    /// Logique d'interaction pour DxSoloPass.xaml
    /// </summary>
    public partial class DxSoloPass : Window, INotifyDataErrorInfo, INotifyPropertyChanged
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;
        public event PropertyChangedEventHandler PropertyChanged;



        /// <summary>
        /// Define min length to validate 
        /// </summary>
        public ushort PasswordLength { get; set; } = 8;

        private bool _ErrorPassword;
        public bool ErrorPassword
        {
            get => _ErrorPassword;
            set
            {
                _ErrorPassword = value;
                //   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ErrorPassword)));
            }
        }

        #region Validation
        private Dictionary<string, string> _Errors = new Dictionary<string, string>();

        private void AddError(string k, string val)
        {
            if (_Errors.ContainsKey(k))
                return;

            _Errors.Add(k, val);
        }

        public bool HasErrors => _Errors.Any();

        public IEnumerable GetErrors(string propertyName)
        {
            if (propertyName == nameof(ErrorPassword))
                return _Errors;

            return null;
        }

        #endregion
        /// <summary>
        /// Explanatory Note
        /// </summary>
        public string Invite { get; set; }

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

        public DxSoloPass()
        {
            InitializeComponent();
            DataContext = this;
        }


        public static string ShowModal(string invite = null)
        {
            DxSoloPass window = new DxSoloPass()
            {
                Invite = invite
            };

            string res = null;
            if (window.ShowDialog() == true)
            {

                res = window.PasswordBox.Password;
            }

            //eturn String.IsNullOrEmpty(res) ? null : res;
            return res;
            //return null;
        }


        private void Button_Submit(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }


        private void PasswordExt_MouseLeave(object sender, MouseEventArgs e)
        {
            Check_PasswordRules();
            Keyboard.ClearFocus();
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            Check_PasswordRules();
        }

        private void Check_PasswordRules()
        {

            if (PasswordBox.Password.Length < PasswordLength)
            {
                //ErrorPassword = true;
                AddError("Taille", DxTBLang.SizeShort + PasswordLength);
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ErrorPassword)));
                SubmitEnabled = false;

                Debug.WriteLine("signalé eeerr");
            }
            else
            {
                //ErrorPassword = false;
                _Errors.Remove("Taille");
                ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(nameof(ErrorPassword)));
                Debug.WriteLine("signalé Noneer");

                SubmitEnabled = true;
            }
        }

    }
}
