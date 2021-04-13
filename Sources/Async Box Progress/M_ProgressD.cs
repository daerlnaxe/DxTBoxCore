using DxTBoxCore.Box_Progress.Basix;
using DxLocalTransf.Progress.ToImp;
using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using DxLocalTransf;

namespace DxTBoxCore.Box_Progress
{

    public class M_ProgressD : A_ProgressD, I_AsyncProgress , I_ASBase
    {

        protected I_AsyncSigD Objet { get; set; }

        // --- I_AsBase

        public CancellationTokenSource TokenSource => Objet.TokenSource;
        public CancellationToken CancelToken => Objet.CancelToken;

        public bool IsPaused
        {
            get => Objet.IsPaused;
            set => Objet.IsPaused =value;
        }

        // ---

        public int Delay { get; set; }

        public Task TaskRunning { get; protected set; }


        public Func<object> TaskToRun { get; protected set; }
              

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="Y">What you want as class inheriting of this</typeparam>
        /// <param name="objet"></param>
        /// <param name="method"></param>
        /// <returns></returns>
        public static M_ProgressD Create<T/*, Y*/>(T objet, Func<object> method) where T : I_AsyncSigD
        {
            M_ProgressD m = new M_ProgressD();

            m.Initialize(objet, method);
            return m;
        }

        public static Y Create<T,Y>(T objet, Func<object> method) where T : I_AsyncSigD
                                                                   where Y : I_Initialize, new()
        {
            Y m = new Y();
            m.Initialize(objet, method);
            return m;
        }

        public void Initialize<T>(T objet, Func<object> taskToRun) where T : I_AsyncSigD
        {
            TaskToRun = taskToRun;

            Objet = objet;

            Objet.UpdateProgress += SetProgress;
            Objet.UpdateStatus += SetStatus;
            Objet.MaximumProgress += SetMaximum;

            // --- 

            Objet.UpdateProgressT +=  SetTotalProgress;
            Objet.UpdateStatusT += SetTotalStatus;
            Objet.MaximumProgressT += SetTotalMaximum;
        }

        // <summary>
        /// Launch task
        /// </summary>
        /// <param name="Ending">
        /// Method to run at the end
        /// </param>
        public async void Launch_Task(Func<object> Ending, int delay = 50)
        {
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

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

            //await TaskRunning;

            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);
        }

        public void StopTask()
        {
            Objet.StopTask();
        }
    }
}
