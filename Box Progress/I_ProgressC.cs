using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{
    interface I_ProgressC: I_Progress
    {
        /// <summary>
        /// Position of total progression
        /// </summary>
        double CurrentTotal { get; set; }
    }
}
