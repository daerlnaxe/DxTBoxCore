using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace DxTBoxCore.Box_Progress.Basix
{
    abstract class ALauncher: ILauncher
    {
        public virtual int Delay { get; set; }
        public virtual bool IsPaused { get; set; }
        public virtual bool AutoClose { get; set; }

        public virtual bool WindowClosing { get; protected set; }

        public virtual I_ASBase Objet { get; set; }

        public virtual Task TaskRunning { get; }

        public abstract IGraphAs ProgressIHM { get; set; }

        public abstract void Launch();
        public abstract void Pause(int timeSleep);
        public abstract void StopTask();
    }
}
