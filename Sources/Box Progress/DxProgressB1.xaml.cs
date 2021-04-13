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
    public partial class DxProgressB1 : Window
    {
        #region Hide Close button
        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        #endregion

        /// <summary>
        /// Signal update to the xaml for a property
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        public double MaxProgress { get; set; }
        private double _CurrentProgress { get; set; }
        private string _CurrentOP { get; set; }

        public DxProgressB1()
        {
            InitializeComponent();
            DataContext = this;
        }

        public DxProgressB1(double max)
        {
            MaxProgress = max;
            InitializeComponent();
            DataContext = this;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Remove close button
            var hwnd = new WindowInteropHelper(this).Handle;
            SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
        }


        #region progression
        public double CurrentProgress
        {
            get { return _CurrentProgress; }
            set
            {
                Console.WriteLine($"[DxProgressB1] CurrentProgress: {value}");
                _CurrentProgress = value;
                if (PropertyChanged != null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentProgress"));
            }
        }

        public static double SCurrentProgress
        {
            get { return _StatWindow.CurrentProgress; }
            set { _StatWindow.CurrentProgress = value; }
        }

        #endregion

        #region opérations
        public string CurrentOP
        {
            get { return _CurrentOP; }
            set
            {
                Console.WriteLine($"[DxProgressB1] CurrentOp: {value}");
                _CurrentOP = value;
                if (PropertyChanged != null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentOP"));
            }
        }

        public static string SCurrentOP
        {
            get { return _StatWindow.CurrentOP; }
            set { _StatWindow.CurrentOP = value; }
        }
        #endregion



        #region statique version
        private static DxProgressB1 _StatWindow;

        public static void ModalShow(string name, double max)
        {
            _StatWindow = new DxProgressB1(max);
            _StatWindow.Title = name;
            _StatWindow.ShowDialog();
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
