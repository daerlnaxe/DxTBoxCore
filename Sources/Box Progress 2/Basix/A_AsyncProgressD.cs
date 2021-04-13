using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress.Basix
{
    /// <summary>
    /// Classe servant à hériter, les M_Progress ne doivent pas en hériter
    /// </summary>
    public abstract class A_AsyncProgressD : A_ProgressD, I_AsyncProgress /*, I_AsyncProgressD*/
    {
        public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;


        public virtual bool IsPaused { get; set; }

        public virtual Func<object> TaskToRun { get; protected set; }

        public virtual int Delay { get; set; } = 10;

        public virtual Task TaskRunning { get; protected set; }


        // <summary>
        /// Launch task
        /// </summary>
        /// <param name="Ending">
        /// Method to run at the end
        /// </param>
        public virtual async void Launch_Task(Func<object> Ending, int delay = 50)
        {

            await Task.Delay(delay);
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            TaskRunning = Task.Run(
                () =>
                {
                    TaskToRun();
                }
                            , CancelToken);

            if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }

            //await TaskRunning;

            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);
        }

        public virtual void StopTask()
        {
            TokenSource.Cancel();
        }


        /*
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

        /*
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

        */

    }
}

