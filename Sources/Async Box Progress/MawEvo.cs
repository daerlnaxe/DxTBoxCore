using DxLocalTransf.Progress;
using DxTBoxCore.Async_Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    class MawEvo: A_ProgressEphD, I_ASBase
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;

        public bool IsPaused { get; set; }



        public void RerouteSignal<U>(U objet) where U : I_AsyncSigD
        {
            objet.UpdateProgress += SetProgress;
            objet.UpdateStatus += SetStatus;
            objet.UpdateStatusNL += SetStatusNL;
            objet.MaximumProgress += SetMaximum;

            objet.UpdateProgressT += SetTotalProgress;
            objet.UpdateStatusT += SetTotalStatus;
            objet.UpdateStatusTNL += SetTotalStatusNL;
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
