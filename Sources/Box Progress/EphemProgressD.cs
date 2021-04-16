using AsyncProgress;
using DxTBoxCore.Async_Box_Progress.Basix;

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

            objet.UpdateProgressT += this.SetTotalProgress;
            objet.UpdateStatusT += this.SetTotalStatus;
        }

    }
}
