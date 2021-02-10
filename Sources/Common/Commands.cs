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
        public static readonly RoutedUICommand Edit = new RoutedUICommand(DxTBLang.Edit, "Edit", typeof(Commands));
        public static readonly RoutedUICommand Delete = new RoutedUICommand(DxTBLang.Delete, "Delete", typeof(Commands));
        public static readonly RoutedUICommand Remove = new RoutedUICommand(DxTBLang.Remove, "Remove", typeof(Commands));

    }
}
