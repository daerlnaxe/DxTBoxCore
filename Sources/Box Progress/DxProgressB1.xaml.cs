using AsyncProgress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Logique d'interaction pour ProgressBar1.xaml
    /// </summary>
    public partial class DxProgressB1 : Window, IGraphAs
    {
        #region Hide Close button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        public bool HideCloseButton { get; set; }

        #endregion
        public I_RProgress Model { get; internal set; }

        public bool TaskFinished { get; set; }


        public DxProgressB1()
        {
            InitializeComponent();
        }

        public DxProgressB1(I_RProgress model)
        {
            Model = model;
            InitializeComponent();

        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = Model;
            // Remove close button
            if (HideCloseButton)
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);

            }
        }




        #region statique version
        private static DxProgressB1 _StatWindow;

        public static void ModalShow(string name, double max)
        {
            //_StatWindow = new DxProgressB1(max);
            //_StatWindow.Title = name;
            //_StatWindow.ShowDialog();
        }

        public static void CloseIt()
        {
            _StatWindow.Close();
        }

        public static void AsyncCloseIt()
        {
            _StatWindow.Dispatcher.BeginInvoke((Action)(() => _StatWindow.Close()));
        }

        #endregion


    }
}
