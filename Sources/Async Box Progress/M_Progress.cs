using DxTBoxCore.Box_Progress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DxLocalTransf.Progress;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Ancien M_Progress
    /// </summary>
    /// <remarks>
    /// Classe à utiliser si l'on ne peut pas hériter, permet d'encapsuler
    ///  - Lie les signaux de l'objet aux propriétés
    /// </remarks>
    public class M_Progress : A_Progress, I_AsyncProgress, I_ASBase
    {
        protected I_ASBase Objet { get; set; }

        public Func<object> TaskToRun { get; protected set; }

        public virtual int Delay { get; set; } = 10;

        public Task TaskRunning { get; protected set; }

        // --- I_AsBase

        public CancellationTokenSource TokenSource => Objet.TokenSource;

        public CancellationToken CancelToken => Objet.CancelToken;

        public bool IsPaused
        {
            get => Objet.IsPaused;
            set => Objet.IsPaused = value;
        }

        // --- 

        protected virtual void Initialize<T>(T objet, Func<object> taskToRun) where T : I_AsyncSig
        {
            TaskToRun = taskToRun;

            Objet = objet;
            objet.UpdateProgress += (x, y) => CurrentProgress = y;
            objet.UpdateStatus += SetStatus;
            objet.MaximumProgress += SetMaximum;
        }

        public static M_Progress Create<T>(T objet, Func<object> method) where T : I_AsyncSig
        {
            M_Progress m = new M_Progress();
            m.Initialize(objet, method);

            return m;

        }

        // <summary>
        /// Launch task
        /// </summary>
        /// <param name="Ending">
        /// Method to run at the end
        /// </param>
        public virtual async void Launch_Task(Func<object> Ending, int delay = 50)
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
            throw new NotImplementedException();
        }
    }
}
