using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace DxTBoxCore.Box_Progress
{
    public class TestProgressCollec : I_ASBaseC
    {
        public event DoubleDel UpdateProgress;
        public event DoubleDel UpdateTotalProgress;
        public event StringDel UpdateStatus;
        public event StringDel UpdateTotalStatus;


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
                    UpdateTotalStatus?.Invoke("New Task");
                    UpdateTotalProgress?.Invoke(i*10);

                    for (int j = 0; j < 50; j++)
                    {
                        while (IsPaused)
                            Thread.Sleep(100);

                        if (CancelToken.IsCancellationRequested)
                            return null;

                        UpdateProgress?.Invoke(j*2);
                        UpdateStatus?.Invoke($"{DxTBLang.File} {i}.{j}");
                        // db2.CurrentOP = $"{DxTBLang.File} {i}";


                        Thread.Sleep(timeSleep);
                    }

                }
            }
            catch(Exception exc)
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
