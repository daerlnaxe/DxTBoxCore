using DxTBoxCore.BoxChoose;
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

namespace DxTBoxCore
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        /// <summary>
        /// Lanceur
        /// </summary>
        [STAThread]
        public static void Main()
        {
            new Window1().ShowDialog();
        }



        public Window1()
        {
            InitializeComponent();
        }
        private void Open_ChooseFile(object sender, RoutedEventArgs e)
        {
            ChooseFile cf = new ChooseFile()
            {

            };
            cf.ShowDialog();
        }
        private void Open_ChooseFolder(object sender, RoutedEventArgs e)
        {
            /*ChooseFolder cf = new ChooseFolder()
            {
            };
            cf.ShowDialog();*/
            Choose_Experiment cfe = new Choose_Experiment()
            {
                Model = new M_ChooseFolder()
                {
                    StartingFolder = @"C:",
                    HideWindowsFolder = true,
                },
            };

            cfe.ShowDialog();
        }


    }
}
