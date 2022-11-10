using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

using System.Runtime.InteropServices;

namespace DxTBoxCore.Common
{
    public static class ShellIcons
    {
        const string ShellIconsLib = @"C:\WINDOWS\System32\shell32.dll";

        static public Icon HardDrive 
        {
            get { return GetIcon(8); }
        }

        static public Icon test(int num)
        {
            return GetIcon(num);
        } 

        [DllImport("shell32.dll", EntryPoint = "ExtractIcon")]
        extern static IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);

        static public Icon GetIcon(int index)
        {
            IntPtr Hicon = ExtractIcon(
               IntPtr.Zero, ShellIconsLib, index);
            Icon icon = Icon.FromHandle(Hicon);
            return icon;
        }


    }
}
