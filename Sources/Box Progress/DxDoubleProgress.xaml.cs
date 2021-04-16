using AsyncProgress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// 
    /// </summary>
    /// <remarks>
    /// </remarks>
    /// <date>
    /// 29/03/2021
    /// </date>
    public partial class DxDoubleProgress : Window, IGraphAs
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


        public I_RProgressD Model { get; set; }

        public bool TaskFinished { get; set; }

        public DxDoubleProgress()
        {
            InitializeComponent();
        }

        public DxDoubleProgress(I_RProgressD model)
        {
            Model = model;
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (HideCloseButton)
            {
                // Remove close button
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            }

            DataContext = Model;
        }
    }
}
