using DxLocalTransf;
using DxTBoxCore.Common;
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

namespace DxTBoxCore.Box_Decisions
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class MBDecisionD : Window
    {
        public M_Decision Model { get; set; }

        /// <summary>
        /// Buttons to show
        /// </summary>
        public E_DxConfB Buttons { get; set; }

        public MBDecisionD()
        {
            InitializeComponent();
            DataContext = null;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;

            // Buttons 
            if (!Buttons.HasFlag(E_DxConfB.Pass))
                btPass.Visibility = Visibility.Collapsed;
            if (!Buttons.HasFlag(E_DxConfB.Rename))
                btRename.Visibility = Visibility.Collapsed;
            if (!Buttons.HasFlag(E_DxConfB.OverWrite))
                btOverWrite.Visibility = Visibility.Collapsed;
            if (!Buttons.HasFlag(E_DxConfB.Trash))
                btTrash.Visibility = Visibility.Collapsed;
            if (!Buttons.HasFlag(E_DxConfB.Stop))
                btStop.Visibility = Visibility.Collapsed;

            if(Buttons.HasFlag(E_DxConfB.All))
            {
                btPass.Visibility =  btRename.Visibility = btOverWrite.Visibility = btTrash.Visibility = btStop.Visibility = Visibility.Visible;
            }

        }

        public static E_Decision Get_Answer()
        {
            MBDecisionD window = new MBDecisionD()
            {
                Model = new M_Decision()
            };

            if (window.ShowDialog() == true)
            {
                return window.Model.Decision;
            }

            return E_Decision.Stop;
        }



        private void Pass_Click(object sender, RoutedEventArgs e)
        {
            if (Model.All)
            {
                Model.Decision = E_Decision.PassAll;
            }
            else
            {
                Model.Decision = E_Decision.Pass;
            }
            DialogResult = true;
        }

        private void Rename_Click(object sender, RoutedEventArgs e)
        {
            if (Model.All)
            {
                Model.Decision = E_Decision.RenameAll;
            }
            else
            {
                Model.Decision = E_Decision.Rename;
            }
            DialogResult = true;
        }

        private void OverWrite_Click(object sender, RoutedEventArgs e)
        {
            if (Model.All)
            {
                Model.Decision = E_Decision.OverWriteAll;
            }
            else
            {
                Model.Decision = E_Decision.OverWrite;
            }
            DialogResult = true;
        }

        private void Trash_Click(object sender, RoutedEventArgs e)
        {
            if (Model.All)
            {
                Model.Decision = E_Decision.TrashAll;
            }
            else
            {
                Model.Decision = E_Decision.Trash;
            }
            DialogResult = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            if (Model.All)
            {
                Model.Decision = E_Decision.StopAll;
            }
            else
            {
                Model.Decision = E_Decision.Stop;

            }
        }

        public static E_Decision ShowDial(string source, string destination, string message, E_DxConfB buttons = E_DxConfB.All)
        {
            MBDecisionD mbd = new MBDecisionD()
            {
                Buttons = buttons,
                Model = new M_Decision()
                {                    
                    Message = message,
                    Source = source,
                    Destination = destination                    
                }
            };

            mbd.ShowDialog();
            return mbd.Model.Decision;
        }
    }
}
