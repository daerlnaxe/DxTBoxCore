using DxLocalTransf;
using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Progress.Basix;
using System.Diagnostics;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    /*
     * Maw is the answer because Maw is Maw, get a Maw or become a Maw.
    */
    public class Maw : A_Progress, I_ASBase/*, I_TProgressD*/
    {

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken => TokenSource.Token;

        public bool IsPaused { get; set; }


        public virtual void RerouteSignal<T>(T objet)where T: I_AsyncSig
        {
            objet.UpdateProgress += SetProgress;
            objet.UpdateStatus += SetStatus;
            objet.MaximumProgress += SetMaximum;
        }

        public void StopTask()
        {
            TokenSource.Cancel();
            Debug.WriteLine("Maw, stop task asked");

            // throw new System.NotImplementedException();
        }

        /*
        public void SetProgress(object sender, double value)
        {
            UpdateProgress?.Invoke(sender, value);
        }

        public void SetStatus(object sender, string value)
        {
            UpdateStatus?.Invoke(sender, value);
        }

        public void SetMaximum(object sender, double value)
        {
            MaximumProgress?.Invoke(sender, value);
        }


        public void SetTotalProgress(object sender, double value)
        {
            UpdateProgressT?.Invoke(sender, value);
        }

        public void SetTotalStatus(object sender, string value)
        {
            UpdateStatusT?.Invoke(sender, value);
        }

        public void SetTotalMaximum(object sender, double value)
        {
            MaximumProgressT?.Invoke(sender, value);
        }


        */



    }
}
