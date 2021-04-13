using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.Box_Progress.Basix
{
    /// <summary>
    /// Classe abstraite qui implémente les propriétés et méthodes pour siganler les progrès
    /// </summary>
    public class A_ProgressD : A_Progress, I_RProgressD, I_TProgressD
    {
        private double _currentTotal;
        public virtual double CurrentTotal
        {
            get => _currentTotal;
            set
            {
#if DEBUG
                Debug.WriteLine($"[M_ProgressC] {nameof(CurrentTotal)}: {value}");
#endif
                _currentTotal = value;
                OnPropertyChanged();
            }
        }


        private string _totalStatus;
        public virtual string TotalStatus
        {
            get => _totalStatus;
            set
            {
#if DEBUG
                Debug.WriteLine($"[M_ProgressC] {nameof(TotalStatus)}: {value}");
#endif
                _totalStatus = value;
                OnPropertyChanged();
            }
        }

        private double _maxProgressT;
        /// <summary>
        /// </summary>
        /// <remarks>
        /// Normalement inutile de mettre à jour en temps réel
        /// </remarks>
        public virtual double MaxProgressT
        {
            get => _maxProgressT;
            set
            {
                _maxProgressT = value;
#if DEBUG
                Debug.WriteLine($"[M_ProgressC] {nameof(MaxProgressT)}: {value}");
#endif
                OnPropertyChanged();
            }
        }


        #region Virtual
        /*protected virtual void Initialize<T>(T objet, Func<Object> taskToRun) where T : ToImp.I_ASBase
        {
            TaskToRun = taskToRun;

            Objet = objet;
            objet.UpdateProgress += (x) => CurrentProgress = x;
            objet.UpdateStatus += UpdateStatus;
            objet.MaximumProgress += MaximumProgress;
        }*/


        public virtual void SetTotalProgress(object sender, double value)
        {
            CurrentTotal = value;
        }

        public virtual void SetTotalStatus(object sender, string value)
        {
            TotalStatus = value;
        }

        public void SetTotalMaximum(object sender, double value)
        {
            MaxProgressT = value;
        }
        #endregion



    }
}
