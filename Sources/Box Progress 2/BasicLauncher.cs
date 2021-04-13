using DxLocalTransf.Progress;
using DxTBoxCore.Box_Progress.Basix;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{

    /// <summary>
    /// Modifie le fonctionnement pour rendre traversant et atteindre les fonctions asynchrones de l'objet
    /// </summary>
    /// <remarks>
    /// Ne doit pas hériter des A_AsyncProgress car ils utilisent en plus la progression
    /// </remarks>
    public class BasicLauncher<T> : A_Async where T : I_ASBase
    {
        protected I_ASBase Objet { get; set; }

        // --- I_AsBase

        public override CancellationTokenSource TokenSource => Objet.TokenSource;

        public override CancellationToken CancelToken => Objet.CancelToken;

        public override bool IsPaused
        {
            get => Objet.IsPaused;
            set => Objet.IsPaused = value;
        }

        public BasicLauncher(T objet, Func<object> methodToTun)
        {
            Objet = objet;
            TaskToRun = methodToTun;
        }


        public static BasicLauncher<T> Create(T objet, Func<object> methodToRun)
        {
            return new BasicLauncher<T>(objet, methodToRun);

        }


        public override void StopTask()
        {
            Objet.StopTask();
        }
    }
}
