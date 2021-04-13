using DxLocalTransf.Progress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    class MawEvo: A_ProgressD, I_RProgressD, I_TProgressD
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;

        public bool IsPaused { get; set; }



        public void RerouteSignal<U>(U objet) where U : I_AsyncSigD
        {


            objet.UpdateProgress += SetProgress;
            objet.UpdateStatus += SetStatus;
            objet.MaximumProgress += SetMaximum;

            objet.UpdateProgressT += SetTotalProgress;
            objet.UpdateStatusT += SetTotalStatus;
            objet.MaximumProgressT += SetTotalMaximum;

        }




        public void StopTask()
        {
            TokenSource.Cancel();
            Debug.WriteLine("Maw, stop task asked");

            // throw new System.NotImplementedException();
        }

    }
}
