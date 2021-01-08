using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.Box_Progress
{
    public interface I_ASBaseC: I_ASBase
    {
        public event DoubleDel UpdateTotalProgress;
        public event StringDel UpdateTotalStatus;
    }
}
