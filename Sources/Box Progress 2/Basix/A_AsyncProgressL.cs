using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress.Basix
{
    /// <summary>
    /// Classe servant à hériter, les M_Progress ne doivent pas en hériter
    /// </summary>
    public abstract class A_AsyncProgressL : A_ProgressL, I_Async
    {
        /*      private ObservableCollection<string> _collec = new ObservableCollection<string>();

              public new ObservableCollection<string> CurrentOP
              {
                  get { return _collec; }
                  set
                  {
                      //  Debug.WriteLine("Debug: List Set");
                      _collec = value;
                      OnPropertyChanged();
                  }

          }
        */

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

        }
        public virtual void StopTask()
        {
            TokenSource.Cancel();
        }
    }
}
