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

namespace DxTBoxCore.Box_Progress.Basix
{
    /// <summary>
    /// Modele utilisé pour les box progress simple, ça permet de passer un objet qui n'a que des events
    /// </summary>
    public abstract class A_Progress : I_RProgressA, I_TProgress
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {
#if DEBUG
                Debug.WriteLine($"Erreur sur {propertyName}");
#endif
            }
        }
        /*
        // ---

        private static CancellationTokenSource tokenSource = new CancellationTokenSource();

        public CancellationToken CancelToken { get; set; } = tokenSource.Token;

        public bool IsPaused { get; set; }
        // ---
        public Func<CancellationToken, bool, object> TaskToRun
        { get; set; }
        */


        #region Progress

        private double _currentProgress;
        public double CurrentProgress
        {
            get => _currentProgress;
            set
            {
                try
                {
#if DEBUG
                    Debug.WriteLine($"[AM_ProgressC] CurrentProgress: {value}");
#endif
                    _currentProgress = value;
                    OnPropertyChanged();
                }

                catch
                {
#if DEBUG
                    Debug.WriteLine($"Erreur sur {nameof(CurrentProgress)}");
#endif
                }
            }
        }


        #endregion
        // ---

        public string NameCOPType { get; set; }

        private string _currentOp;
        public string CurrentOP
        {
            get => _currentOp;
            set
            {
#if DEBUG
                Debug.WriteLine($"[AM_ProgressC] CurrentOP: {value}");
#endif
                _currentOp = value;
                OnPropertyChanged();
            }
        }

        // ---

        private double _maxProgress;
        public double MaxProgress
        {
            get => _maxProgress;
            set
            {

#if DEBUG
                Debug.WriteLine($"[AM_ProgressC] MaxProgress: {value}");
#endif

                _maxProgress = value;
                OnPropertyChanged();

            }
        }



        /*
        public void StopTask()
        {

        }*/

        #region Virtual
        

        public virtual void SetProgress(object sender,double value)
        {
            CurrentProgress = value;
        }
        
        public virtual void SetStatus(object sender, string value)
        {
            CurrentOP = value;
        }

        public virtual void SetMaximum(object sender, double value)
        {
            MaxProgress = value;
        }
        
        
        #endregion
    }
}
