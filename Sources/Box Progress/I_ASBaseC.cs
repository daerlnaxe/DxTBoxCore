using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.Box_Progress
{
    public interface I_ASBaseC : I_ASBase
    {
        /// <summary>
        /// Current value for the Total state of the task
        /// </summary>
        public event DoubleDel UpdateProgressT;

        /// <summary>
        /// Current state for the Total status of the task
        /// </summary>
        public event StringDel UpdateStatusT;

        /// <summary>
        /// Maximum for the Total of the task
        /// </summary>
        public event DoubleDel MaximumProgressT;
    }
}
