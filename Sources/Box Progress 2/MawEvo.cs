using DxLocalTransf.Progress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace DxTBoxCore.Box_Progress
{
    class MawEvo:Maw, I_RProgressD
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


        public new void RerouteSignal<U>(U objet) where U : I_RProgressD
        {
            base.RerouteSignal(objet);

            objet.CurrentTotal += CurrentTotal;
            objet.TotalStatus += TotalStatus;
            objet.MaxProgressT += MaxProgressT;
            //objet.pro

        }
    }
}
