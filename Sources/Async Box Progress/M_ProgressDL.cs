using DxLocalTransf.Progress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Model to communicate with a window progress managing a collection
    /// </summary>
    /// <remarks>
    /// Contient:
    ///     - Dynamic update
    /// Lock obligatoire pour ajouter à la collection
    /// C'est au niveau de la gestion de la collection que ça se distingue de M_ProgressC car il cumule
    /// le texte
    /// </remarks>
    /// 
    public class M_ProgressDL : A_ProgressLD, I_AsyncProgress, I_Initialize, I_ASBase
    {
        //lock object for synchronization;
        private static object _syncLock = new object();

        public I_AsyncSigD Objet { get; private set; }

        public CancellationTokenSource TokenSource => Objet.TokenSource;

        public CancellationToken CancelToken => Objet.CancelToken;

        public bool IsPaused
        {
            get => Objet.IsPaused;
            set => Objet.IsPaused = value;
        }

        // ---

        public int Delay { get; set; }

        public Task TaskRunning { get; protected set; }


        public Func<object> TaskToRun { get; protected set; }

        public M_ProgressDL()
        {

        }

        public static M_ProgressD Create<T>(T objet, Func<object> method) where T : I_AsyncSigD
        {
            M_ProgressD m = new M_ProgressD();

            m.Initialize(objet, method);
            return m;
        }

        public static Y Create<T, Y>(T objet, Func<object> method) where T : I_AsyncSigD
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

            Objet.UpdateProgressT += SetTotalProgress;
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
            await Task.Delay(delay);

            TaskRunning = Task.Run(
            
                () =>
                {
                    TaskToRun();
                }
                            , Objet.CancelToken);

            if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }

        }

        public override void SetStatus(object sender, string value)
        {
            AddToCollec(value);
        }

        public override void SetTotalStatus(object sender, string value)
        {
            TotalStatus = value;
            AddToCollec($">>> {value} <<<");
        }

        public void StopTask()
        {
            Objet.StopTask();
        }
    }
}
