using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Encapsule a method to make it compatible with progress boxes
    /// </summary>
    /// <typeparam name="T">Object you want to pass</typeparam>
    /// <typeparam name="Y">Object you want to get</typeparam>
    public partial class Maou<T, Y> : Maou<Y>
    {
        /// <summary>
        /// Parameter to pass to the method
        /// </summary>
        public T Param { get; set; }

        /// <summary>
        /// Method to Run
        /// </summary>
        public new Func<I_ASBase, T, Y> ToRun{ get; set; }

        /// <summary>
        /// Run the method
        /// </summary>
        /// <param name="timeSleep"></param>
        /// <returns>
        /// Your type of object
        /// </returns>
        public new object Run(int timeSleep = 10)
        {
            return ToRun(this, Param);
        }


    }
}
