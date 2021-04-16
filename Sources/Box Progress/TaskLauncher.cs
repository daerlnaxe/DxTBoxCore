using AsyncProgress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace DxTBoxCore.Box_Progress
{
    public class TaskLauncher : ALauncher
    {
        /*public virtual CancellationTokenSource TokenSource { get; set; } = new CancellationTokenSource();

        public virtual CancellationToken CancelToken => TokenSource.Token;*/

        public override bool IHMLaunched { get; protected set; }

        public override IGraphAs ProgressIHM { get; set; }
        public override I_ASBase Objet { get; protected set; }

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
        public override bool? Launch(I_ASBase objet)
        {
            Objet = objet;

            ProgressIHM.Loaded += Blee_Loaded;
            ProgressIHM.Closing += Blee_Closing;
            ProgressIHM.Closed += Blee_Closed;

            TaskRunning = Task.Run
            (
               async () =>
               {
                   Debug.WriteLine("taskLauncher2 avant timer");
                   while (!IHMLaunched)
                   {
                       await Task.Delay(LoopDelay);
                   }
                   Debug.WriteLine("taskLauncher2 après timer");
                   MethodToRun();
               },
               Objet.CancelToken
            );


            if (AutoCloseWindow)
                TaskRunning.ContinueWith((ant) => this.CloseBlee());

            TaskRunning.ContinueWith((ant) => ProgressIHM.TaskFinished = true);

            bool? res = false;


            if (ProgressIHM is Window)
            {
                return res = ((Window)ProgressIHM).ShowDialog();
            }

            // job
            //base.TaskRunning = Task.Run(() => base.TaskToRun(base.CancelToken, test), base.TaskToRun.CancelToken);

            //await TaskRunning;
            Debug.WriteLine("Task Stopped");
            //TaskRunning.ContinueWith((ant) => TaskToRun.Run(), TaskToRun.CancelToken);

            return res;
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
            switch (TaskRunning.Status)
            {
                case TaskStatus.RanToCompletion:
                    break;
                case TaskStatus.Canceled:
                    break;
                case TaskStatus.Running:
                case TaskStatus.WaitingForActivation:
                    // interruption - On bloque la fermeture de la fenêtre, on s'en chargera plus tard
              //      e.Cancel = true;
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
