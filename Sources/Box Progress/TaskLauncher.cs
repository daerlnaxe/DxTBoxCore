﻿using DxLocalTransf.Progress;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DxTBoxCore.Box_Progress
{
    class TaskLauncher : ALauncher
    {
        /*public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;*/


        public override IGraphAs ProgressIHM { get; set; }

        /// <summary>
        /// Tâche à lancer
        /// </summary>
        /// <example>
        /// ex : () => tpc.Run(50)
        /// </example>
        public Func<object> MethodToRun { get; set; }
        public new Task TaskRunning { get; protected set; }


        // ---

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ending"></param>
        /// <param name="delay"></param>
        public override void Launch()
        {
            ProgressIHM.Loaded += Blee_Loaded;
            ProgressIHM.Closing += Blee_Closing;
            ProgressIHM.Closed += Blee_Closed;

            if (ProgressIHM is Window)
                ((Window)ProgressIHM).ShowDialog();

            // job
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            //await TaskRunning;
            Debug.WriteLine("Task Stopped");
            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);
        }


        private void Blee_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Blee_Loaded, launch task");

            /*TaskRunning = Task.Run(
               async () =>
               {
                   await Task.Delay(Delay);
                   TasksToRun();
               }
                    , Objet.CancelToken);

            /*if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }*/


            TaskRunning = Task.Run
                (
                    () =>
                    {
                        MethodToRun();
                    },
                    Objet.CancelToken
                );


            if (AutoClose)
                TaskRunning.ContinueWith((ant) => this.CloseBlee());

            TaskRunning.ContinueWith((ant) => ProgressIHM.TaskFinished = true);
        }

        /// <summary>
        /// Demandé par l'utilisateur OU à la fin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Blee_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowClosing = true;

            switch (TaskRunning.Status)
            {
                case TaskStatus.RanToCompletion:
                    // nothing                    
                    break;
                case TaskStatus.Canceled:
                    break;
                case TaskStatus.Running:
                case TaskStatus.WaitingForActivation:
                    // interruption
                    StopTask();

                    break;
                default:
                    Debug.WriteLine($"Not Managed {TaskRunning.Status}");
                    break;
            }
        }

        private void CloseBlee()
        {
            ProgressIHM.Dispatcher?.Invoke(() => ProgressIHM.Close());
            /*
            Application.Current.Dispatcher
            Application.Current.Dispatcher?.Invoke
                (
                    () => blee.Close()
                );*/
        }


        private void Blee_Closed(object sender, EventArgs e)
        {
            //throw new NotImplementedException();
        }



        public override void Pause(int timeSleep = 100)
        {
            while (IsPaused)
                Thread.Sleep(100);
        }


        public override void StopTask()
        {
            Objet.TokenSource.Cancel();

            if (!WindowClosing)
                ProgressIHM.Close();


        }
    }
}