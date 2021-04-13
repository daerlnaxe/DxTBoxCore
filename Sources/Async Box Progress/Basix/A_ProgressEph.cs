using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
#if DEBUG
using System.Diagnostics;
#endif
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

        protected string _Status;
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



        public override void SetStatus(object sender, string value)
        {
            Status = value;
            _WriteToEnd = false;
        }

        public override void SetStatusNL(object sender, string value)
        {
            Status = value;
            _WriteToEnd = true;
        }


        #endregion
    }
}
