using AsyncProgress;
using AsyncProgress.Cont;
using System.Diagnostics;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    /// <summary>
    /// Classe abstraite qui implémente les propriétés et méthodes pour siganler les progrès
    /// </summary>
    public class A_ProgressEphD : A_ProgressEph, I_RProgressD, I_TProgressD
    {
        private double _currentTotal;
        public virtual double ProgressTotal
        {
            get => _currentTotal;
            set
            {
#if DEBUG
                Debug.WriteLine($"[{nameof(ProgressTotal)}]: {value}");
#endif
                _currentTotal = value;
                OnPropertyChanged();
            }
        }


        private string _TotalStatus;
        private bool _WriteToEndT;
        public virtual string TotalStatus
        {
            get => _TotalStatus;
            set
            {
                if (_WriteToEndT)
                {
                    Debug.WriteLine($"[{nameof(TotalStatus)}] CurrentProgress: {value}");
                    _TotalStatus = value;

                }
                else
                {
                    _TotalStatus += value;
                    Debug.Write(value);
                }
                _TotalStatus = value;
                OnPropertyChanged();
            }
        }

        private double _maxProgressT;
        /// <summary>
        /// </summary>
        /// <remarks>
        /// Normalement inutile de mettre à jour en temps réel
        /// </remarks>
        public virtual double MaximumTotal
        {
            get => _maxProgressT;
            set
            {
                _maxProgressT = value;
#if DEBUG
                Debug.WriteLine($"[{nameof(MaximumTotal)}]: {value}");
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


        public virtual void SetTotalProgress(object sender, ProgressArg arg)
        {
            MaximumTotal = arg.Total;
            ProgressTotal = arg.Progress;
        }

        public virtual void SetTotalStatus(object sender, StateArg arg)
        {
            TotalStatus = arg.Message;
            _WriteToEndT = arg.EndOfLine;
        }



        #endregion



    }
}
