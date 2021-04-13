using DxTBoxCore.Box_Progress.Basix;
using DxTBoxCore.Box_Progress_2;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DxLocalTransf.Progress.ToImp;
using DxLocalTransf.Progress;

namespace DxTBoxCore.Box_Progress
{
    public class Merde : M_ProgressD, I_RProgress, I_AsyncProgress
    {
        /*
        public void Initialize<T>(T objet, Func<object> taskToRun)
        {
            TaskToRun = taskToRun;

            Objet = objet;

            Objet.UpdateProgress += UpdateCurrent;
            Objet.MaximumProgress += UpdateMax;
            Objet.UpdateStatus += SetStatus;

            // --- 

            Objet.UpdateProgressT += this.SetTotalProgress;
            Objet.UpdateStatusT += SetTotalStatus;
            Objet.MaximumProgressT += SetTotalMaximum;
        }
        */

        private double Max;

        private void UpdateMax(object sender, double value)
        {
            MaxProgress = 100;
            Max = value;
        }

        private void UpdateCurrent(object sender, double value)
        {
            CurrentProgress = value * Max / 100;
        }

        /*
public override void MaximumProgress(double value)
{
   MaxProgress = value;
}

/// <summary>
/// </summary>
/// <param name="value"></param>
/// <remarks>
/// Conversion en %
/// </remarks>
public override void UpdateProgress(double value)
{
   CurrentProgress = value;
}*/



    }
}
