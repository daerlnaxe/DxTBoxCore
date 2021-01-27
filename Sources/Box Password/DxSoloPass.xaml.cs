using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
    public partial class DxSoloPass : Window, INotifyDataErrorInfo
    {
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Define min length to validate 
        /// </summary>
        public ushort PasswordLength { get; set; } = 8;


        private bool ErrorPassword;
        public bool HasErrors => ErrorPassword;


        /// <summary>
        /// Explanatory Note
        /// </summary>
        public string Invite { get; set; }



        public DxSoloPass()
        {
            InitializeComponent();
        }


        public static string ShowModal()
        {
            DxSoloPass window = new DxSoloPass();
            window.ShowDialog();

            //todo string res = window.passBox.Password.ToString();

            //todo return String.IsNullOrEmpty(res) ? null : res;
            return null;
        }



        private void passB_KeyUp(object sender, KeyEventArgs e)
        {

        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Submit(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordExt_ShowPassClick(object sender, RoutedEventArgs e)
        {

        }

        private void PasswordExt_MouseLeave(object sender, MouseEventArgs e)
        {
            Check_PasswordRules();
        }

        private void Check_PasswordRules()
        {

            if (PasswordBox.Password.Length < PasswordLength)
            {
                PasswordBox.FireError();
            }
            else
            {
                PasswordBox.ResetError();
            }
        }



        #region Validation

        public IEnumerable GetErrors(string propertyName)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
