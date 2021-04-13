using DxLocalTransf;
using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    public class TestProgressCollec : I_AsyncSigD
    {
        public event DoubleHandler UpdateProgress;
        public event MessageHandler UpdateStatus;
        public event DoubleHandler MaximumProgress;

        public event DoubleHandler UpdateProgressT;
        public event MessageHandler UpdateStatusT;
        public event DoubleHandler MaximumProgressT;

        public CancellationTokenSource TokenSource { get; } = new CancellationTokenSource();

        public CancellationToken CancelToken { get; }

        public bool IsPaused { get; set; }


        public TestProgressCollec()
        {
            CancelToken = TokenSource.Token;
        }



        public object Run(int timeSleep)
        {
            if (timeSleep < 10)
                throw new Exception($"Timesleep trop bas: {timeSleep}");

            try
            {
                // Thread.Sleep(500);

                // Boucle Totale
                for (int i = 0; i < 10; i++)
                {
                    UpdateStatusT?.Invoke(this, "New Task");
                    UpdateProgressT?.Invoke(this, i * 10);

                    for (int j = 0; j < 50; j++)
                    {
                        while (IsPaused)
                            Thread.Sleep(100);

                        if (CancelToken.IsCancellationRequested)
                            return null;

                        UpdateProgress?.Invoke(this, j * 2);
                        UpdateStatus?.Invoke(this, $"{DxTBLang.File} {i}.{j}");
                        // db2.CurrentOP = $"{DxTBLang.File} {i}";


                        Thread.Sleep(timeSleep);
                    }

                    UpdateProgress?.Invoke(this, 100);
                }

                UpdateProgressT?.Invoke(this, 100);
                Thread.Sleep(100);
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
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
