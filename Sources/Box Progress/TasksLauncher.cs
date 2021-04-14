using DxLocalTransf.Progress;
using DxTBoxCore.Async_Box_Progress.Basix;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace DxTBoxCore.Box_Progress
{
    class TasksLauncher : ALauncher
    {
        /*public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;*/
        public override IGraphAs ProgressIHM { get; set; }


        /// <summary>
        /// Tâche à lancer
        /// </summary>
        public Func<object>[] MethodsToRun { get; set; }

        private List<Task> _Tasks { get; set; } = new List<Task>();

        public override Task TaskRunning => _Tasks.Find((x) => x.Status == TaskStatus.Running);



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

            Task prevTask = null;
            foreach (var t in MethodsToRun)
            {
                if (!_Tasks.Any())
                    prevTask = Task.Run
                    (
                        () =>
                            {
                                t();
                            },
                        Objet.CancelToken
                     );
                else
                {
                    prevTask = prevTask.ContinueWith((ant) => t());
                }

                _Tasks.Add(prevTask);
            }
            /*if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }*/
            var last = _Tasks.Last();
            if (AutoClose)
            {

                last.ContinueWith((ant) => this.CloseBlee());
            }

            last.ContinueWith((ant) => ProgressIHM.TaskFinished = true);

        }

        /// <summary>
        /// Demandé par l'utilisateur OU à la fin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public virtual void Blee_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            WindowClosing = true;

            var taskRunning = TaskRunning;
            if (taskRunning == null)
                return;

            switch (taskRunning.Status)
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

        internal static Func<object>[] CreateTasks(params Func<object>[] p)
        {
            return p;

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
