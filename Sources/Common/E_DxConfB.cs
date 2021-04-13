using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.Common
{
    /// <summary>
    /// About conflicts
    /// </summary>
    [Flags]
    public enum E_DxConfB
    {
        Pass = 1,
        Rename = 2,
        OverWrite = 4,
        Trash = 8,
        Stop = 16,
        All = 5096
    }
}
