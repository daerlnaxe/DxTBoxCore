using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// 29/03/2021
    /// </summary>
    class M_ProgressExt<T, Y> : I_Progress, I_AsyncProgressExt
    {
        public double CurrentProgress { get; set; }
        public string NameCOPType { get; set; }
        public string CurrentOP { get; set; }
        public double MaxProgress { get; set; }
        public I_ASBase ClassToRun { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Func<object, object>[] MethodsToRun => throw new NotImplementedException();

        public Task TaskRunning => throw new NotImplementedException();

        public event PropertyChangedEventHandler PropertyChanged;

        public void UpdateStatus(string value)
        {
            throw new NotImplementedException();
        }
    }
}
