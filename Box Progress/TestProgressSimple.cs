using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    public class TestProgressSimple : I_ASBase
    {
        public event DoubleDel UpdateProgress;
        public event StringDel UpdateStatus;

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();
        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }

        public TestProgressSimple()
        {
            CancelToken = TokenSource.Token;
        }

        public object Run(int timeSleep = 100)
        {
            for (int i = 0; i < 100; i++)
            {
                while (IsPaused)
                    Thread.Sleep(100);

                if (CancelToken.IsCancellationRequested)
                    return null;

                UpdateProgress?.Invoke(i);
                UpdateStatus?.Invoke($"{DxTBLang.File} {i}");
                // db2.CurrentOP = $"{DxTBLang.File} {i}";


                Thread.Sleep(timeSleep);

            }
            UpdateProgress?.Invoke(100);
            //db2.AsyncClose();

            return null;
        }

        public void StopTask()
        {
            TokenSource.Cancel();

#if DEBUG
            Debug.WriteLine($"[AM_ProgressC] Cancel token requested");
#endif
        }
    }
}
