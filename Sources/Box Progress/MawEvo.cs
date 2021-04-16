using AsyncProgress;
using DxTBoxCore.Async_Box_Progress.Basix;
using System;
using System.Diagnostics;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    public class MawEvo : A_ProgressEphD, I_ASBase
    {
        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;

        public bool IsPaused { get; set; }

        public bool IsInterrupted { get; set; }

        public bool CancelFlag => throw new NotImplementedException();

        public MawEvo()
        {
            Debug.WriteLine("Using Maw without retouting");
        }

        public MawEvo(I_AsyncSigD objet)
        {
            RerouteSignal(objet);

        }

        public void RerouteSignal<U>(U objet) where U : I_AsyncSigD
        {
            objet.UpdateProgress += SetProgress;
            objet.UpdateStatus += SetStatus;

            objet.UpdateProgressT += SetTotalProgress;
            objet.UpdateStatusT += SetTotalStatus;
        }


        public void StopTask()
        {
            TokenSource.Cancel();
            Debug.WriteLine("Maw, stop task asked");

            // throw new System.NotImplementedException();
        }

        public void Pause(int timeSleep)
        {
            throw new NotImplementedException();
        }
    }
}
