using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DxTBoxCore.Box_Progress.Basix
{
    public abstract class ALauncher: ILauncher
    {
        public virtual int LoopDelay { get; set; } = 500;
        public virtual bool IsPaused { get; set; }
        public virtual bool AutoCloseWindow { get; set; }


        public virtual bool WindowClosing { get; protected set; }


        public virtual Task TaskRunning { get; }


        public abstract bool IHMLaunched { get; protected set; }
        public abstract I_ASBase Objet { get; protected set; }
        public abstract IGraphAs ProgressIHM { get; set; }

        public abstract bool? Launch(I_ASBase objet);
        public abstract void Pause(int timeSleep);
        public abstract void StopTask();
    }
}
