using DxLocalTransf.Progress;
using DxTBoxCore.Async_Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Permet d'utiliser un objet qui sert de passerelle entre des signaux et la progression
    /// </summary>
    public class EphemProgressD: A_ProgressEphD
    {
        public EphemProgressD(I_SigProgressD objet)
        {
            objet.UpdateProgress += this.SetProgress;
            objet.UpdateStatus += this.SetStatus;
            objet.UpdateStatusNL += this.SetStatusNL;
            objet.MaximumProgress += this.SetMaximum;

            objet.UpdateProgressT += this.SetTotalProgress;
            objet.UpdateStatusT += this.SetTotalStatus;
            objet.UpdateStatusTNL += this.SetTotalStatusNL;
            objet.MaximumProgressT += this.SetTotalMaximum;
        }

    }
}
