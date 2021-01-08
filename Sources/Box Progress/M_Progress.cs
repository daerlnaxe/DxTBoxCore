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

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Modele utilisé pour les box progress simple
    /// </summary>
    public abstract class M_Progress : I_Progress, I_AsyncProgress
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
        public I_ASBase TaskToRun { get; protected set; }


        public virtual void SetTaskToRun<T>(T taskToRun) where T : I_ASBase
        {
            TaskToRun = taskToRun;
            TaskToRun.UpdateProgress += UpdateProgress;
            TaskToRun.UpdateStatus += UpdateStatus;
        }



        public Task TaskRunning { get; protected set; }




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
        /// <summary>
        /// Launch task
        /// </summary>
        public async virtual void Launch_Task(Func<object> Ending, int delay = 50)
        {

            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            TaskRunning = Task.Run(
                async
                () =>
                {
                    await Task.Delay(delay);
                    TaskToRun.Run();
                }
                            , TaskToRun.CancelToken);
            var kwa = TaskRunning.ContinueWith((ant) => Ending());

            //await TaskRunning;

            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);
        }


        public virtual void UpdateProgress(double value)
        {
            CurrentProgress = value;
        }

        public virtual void UpdateStatus(string value)
        {
            CurrentOP = value;
        }
        #endregion
    }
}
