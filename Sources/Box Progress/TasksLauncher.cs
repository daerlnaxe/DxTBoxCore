using AsyncProgress;
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
    public class TasksLauncher : ALauncher
    {
        /*public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;*/
        public override IGraphAs ProgressIHM { get; set; }

        public override I_ASBase Objet { get; protected set; }

        /// <summary>
        /// Tâche à lancer
        /// </summary>
        public Func<object, object>[] MethodsToRun { get; set; }

        private List<Task> _Tasks { get; set; } = new List<Task>();

        public override Task TaskRunning => _Tasks.Find((x) => x.Status == TaskStatus.Running);

        public override bool IHMLaunched { get; protected set; }

        // ---

        public static Func<object, object>[] CreateTasks(params Func<object, object>[] p)
        {
            return p;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Ending"></param>
        /// <param name="delay"></param>
        public override bool? Launch(I_ASBase objet)
        {
            Objet = objet;

            ProgressIHM.Loaded += Blee_Loaded;
            ProgressIHM.Closing += Blee_Closing;
            ProgressIHM.Closed += Blee_Closed;

            bool res= false;
            Task prevTask = null;
            foreach (var t in MethodsToRun)
            {
                if (!_Tasks.Any())
                    prevTask = Task.Run
                    (
                        async () =>
                        {
                            Debug.WriteLine("taskLauncher avant timer");
                            while (!IHMLaunched)
                            {
                                await Task.Delay(LoopDelay);
                            }
                            Debug.WriteLine("taskLauncher après timer");
                            t(res);
                        },
                        Objet.CancelToken
                     );
                else
                {
                    prevTask = prevTask.ContinueWith((ant) => t(res), TaskContinuationOptions.OnlyOnRanToCompletion);
                }

                _Tasks.Add(prevTask);
            }
            /*if (Ending != null)
            {
                var kwa = TaskRunning.ContinueWith((ant) => Ending());
            }*/
            var last = _Tasks.Last();
            if (AutoCloseWindow)
            {

                last.ContinueWith((ant) => this.CloseBlee());
            }

            last.ContinueWith((ant) => ProgressIHM.TaskFinished = true);

            if (ProgressIHM is Window)
                return ((Window)ProgressIHM).ShowDialog();

            // job
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            //await TaskRunning;
            Debug.WriteLine("Task Stopped");
            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);

            return false;
        }


        private void Blee_Loaded(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine("Blee_Loaded, launch task");
            IHMLaunched = true;
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
        }

        /// <summary>
        /// Demandé par l'utilisateur OU à la fin
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public async virtual void Blee_Closing(object sender, System.ComponentModel.CancelEventArgs e)
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
                    while (!Objet.IsInterrupted)
                    {
                        await Task.Delay(100);
                    }
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
            Objet.StopTask();

            if (ProgressIHM is Window)
                ((Window)ProgressIHM).Title += " - Closing";

            if (!WindowClosing)
                ProgressIHM.Close();


        }

  
    }
}
