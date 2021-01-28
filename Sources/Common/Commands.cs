using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace DxTBoxCore.Common
{
    public class Commands
    {
        public static readonly RoutedUICommand Submit = new RoutedUICommand(DxTBLang.Submit, "Submit", typeof(Commands));

    }
}
