using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Linq;
using DxLocalTransf;
using DxLocalTransf.Progress.ToImp;
using DxLocalTransf.Progress;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Encapsule a method to make it compatible with progress boxes
    /// </summary>
    public class Maou<T> : I_ASBaseC/*, I_AsyncProgressC*/
    {
        /*public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken { get; }

        public Task TaskRunning { get; private set; }


        public int Delay { get; set; } = 10;

        public I_ASBaseC Objet => this;
        

        public Func<object> TaskToRun { get; private set; }

        public bool IsPaused { get; set; }
        

        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;

        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;
        /*
        public Maou()
        {
            CancelToken = TokenSource.Token;
        }*/

        public static Maou<T> Create( Func<object> taskToRun)
        {
            Maou<T> maw = new Maou<T>();
            maw.TaskToRun = taskToRun;
            

            //maw.Initialize(this, taskToRun);

            return maw;
        }



        //public Func<I_ASBaseC, T> ToRun { get; set; }


        /*
        public virtual object Run(int timeSleep = 10)
        {
            return ToRun(this);
        }*/

        public void Launch_Task(Func<object> Ending=null, int delay = 50)
        {
            TaskRunning = Task.Run(
              async
              () =>
              {
                  await Task.Delay(delay);
                  TaskToRun();
              }
                          , Objet.CancelToken);

            if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }
        }

        /// <summary>
        /// Signal current progress
        /// </summary>
        /// <param name="updateProgress"></param>
        public void SayUpdateProgress(object sender, Double updateProgress)
        {
            Debug.WriteLine($"{nameof(SayUpdateProgress)}: {updateProgress}");
            UpdateProgress?.Invoke(sender, updateProgress);
        }

        /// <summary>
        /// Signal current status
        /// </summary>
        /// <param name="updateStatus"></param>
        public void SayUpdateStatus(string updateStatus)
        {
            Debug.WriteLine($"{nameof(SayUpdateStatus)}: {updateStatus}");
            UpdateStatus?.Invoke(this, updateStatus);
        }

        public void SayMaximumProgress(Double totalProgress)
        {
            Debug.WriteLine($"{nameof(SayMaximumProgress)}: {totalProgress}");
            MaximumProgress?.Invoke(this, totalProgress);
        }
        // ---

        /// <summary>
        /// Signal update progress Total
        /// </summary>
        /// <param name="updateProgressT"></param>
        public void SayUpdateProgressT(Double updateProgressT)
        {
            Debug.WriteLine($"{nameof(SayUpdateProgressT)}: {updateProgressT}");
            UpdateProgressT?.Invoke(this, updateProgressT);
        }

        public void SayUpdateStatusT(string updateStatusT)
        {
            Debug.WriteLine($"{nameof(SayUpdateStatusT)}: {updateStatusT}");
            UpdateStatusT?.Invoke(this, updateStatusT);
        }

        /// <summary>
        /// Signal Maximum for Total
        /// </summary>
        /// <param name="totalProgressT"></param>
        public void SayMaximumProgressT(Double totalProgressT)
        {
            Debug.WriteLine($"{nameof(SayMaximumProgressT)}: {totalProgressT}");
            MaximumProgressT?.Invoke(this, totalProgressT);
        }


        public void StopTask()
        {
            TokenSource.Cancel();
        }

        void I_AsyncProgressC.Initialize<T1>(T1 objet, Func<object> taskToRun)
        {
            throw new NotImplementedException();
        }
    }

}
