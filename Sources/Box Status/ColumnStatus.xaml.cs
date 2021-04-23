using DxTBoxCore.Common;
using DxTBoxCore.Languages;
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

namespace DxTBoxCore.Box_Status
{
    /// <summary>
    /// Logique d'interaction pour GameStatus.xaml
    /// </summary>
    public partial class ColumnStatus : Window
    {
        private String _DefaultStyle = "/Themes/Basic.xaml";

        public string Message { get; set; }
        public Dictionary<string, bool?> States { get; set; }

        public Color TrueColor { get; set; }
        public Color FalseColor { get; set; }
        public Color NullColor { get; set; }
        public E_DxButtons Buttons;

        public ColumnStatus()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static bool? ShowDial(string message, string title, Dictionary<string, bool?> states, string trueC, string falseC, string nullC = "#FFFFFFFF", E_DxButtons buttons = E_DxButtons.Yes | E_DxButtons.No)
        {
            ColumnStatus cs = new ColumnStatus()
            {
                Title = title,
                States = states,
                Message = message,
                Buttons = buttons,
                TrueColor = (Color)ColorConverter.ConvertFromString(trueC),
                FalseColor = (Color)ColorConverter.ConvertFromString(falseC),
                NullColor = (Color)ColorConverter.ConvertFromString(nullC),
            };
            return cs.ShowDialog();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            if (Buttons.HasFlag(E_DxButtons.Yes))
                DefaultButton.Content = DxTBLang.Yes;

            if (Buttons.HasFlag(E_DxButtons.Ok))
                DefaultButton.Content = DxTBLang.OK;

            // ---

            if (Buttons.HasFlag(E_DxButtons.No))
            {
                CancelButton.Content = DxTBLang.No;
                CancelButton.Visibility = Visibility.Visible;
            }

            if (Buttons.HasFlag(E_DxButtons.Cancel))
            {
                CancelButton.Content = DxTBLang.Cancel;
                CancelButton.Visibility = Visibility.Visible;
            }
        }


        private void DefaultButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
            this.Close();
        }

    }
}
