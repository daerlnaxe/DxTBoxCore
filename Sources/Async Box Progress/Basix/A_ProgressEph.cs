using AsyncProgress;
using AsyncProgress.Cont;
#if DEBUG
using System.Diagnostics;
#endif

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    /// <summary>
    /// Modele utilisé pour les box progress simple, ça permet de passer un objet qui n'a que des events
    /// </summary>
    /// <remarks>
    /// Emploi d'un booléen pour savoir si on doit écrire à la suite ou non
    /// </remarks>
    public abstract class A_ProgressEph : A_Progress, I_RProgress, I_TProgress
    {
        private bool _WriteToEnd = false;

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// Ne linker directement que si on veut ignorer le mode sans ligne de retour
        /// </remarks>
        public override string Status
        {
            get => _Status;
            set
            {
                if (_WriteToEnd)
                {
                    Debug.WriteLine($"[{nameof(_Status)}] : {value}");
                    _Status = value;

                }
                else
                {
                    _Status += value;
                    Debug.Write(value);
                }
                OnPropertyChanged();
            }
        }

    



        /*
        public void StopTask()
        {

        }*/

        #region Virtual



        public override void SetStatus(object sender, StateArg arg)
        {
            Status = arg.Message;
            _WriteToEnd = arg.EndOfLine;
        }



        #endregion
    }
}
