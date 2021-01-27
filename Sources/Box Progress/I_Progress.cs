using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Properties interface for simple progress datacontext
    /// </summary>
    interface I_Progress: INotifyPropertyChanged
    {
       // event PropertyChangedEventHandler PropertyChanged;
        
        /// <summary>
        /// Current position
        /// </summary>
        double CurrentProgress { get; set; }

        /// <summary>
        /// Current operation
        /// </summary>
        string CurrentOP { get; set; }

        /// <summary>
        /// Maximum progression possible
        /// </summary>
        double MaxProgress { get; set; }


        /// <summary>
        /// Set Current Progress
        /// </summary>
        /// <param name="value"></param>
        void UpdateProgress(double value);

        /// <summary>
        /// Set Current operation
        /// </summary>
        /// <param name="value"></param>
        void UpdateStatus(string value);



    }
}
