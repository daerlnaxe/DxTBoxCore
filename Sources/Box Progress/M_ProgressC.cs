using System;
using System.Collections.Generic;
using System.ComponentModel;

using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
#if DEBUG
using System.Diagnostics;
#endif

namespace DxTBoxCore.Box_Progress
{
    public abstract class M_ProgressC: M_Progress, I_ProgressC
    {
        /*
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /*
        private double _currentProgress;
        public double CurrentProgress
        {
            get => _currentProgress;
            set
            {
#if DEBUG
                Debug.WriteLine($"[AM_ProgressC] CurrentProgress: {value}");
#endif
                _currentProgress = value;
                OnPropertyChanged();
            }
        }

        private string _currentOp;
        public string CurrentOP
        {
            get => _currentOp;
            set
            {
#if DEBUG
                Debug.WriteLine($"[AM_ProgressC] CurrentOP: {value}");
#endif
                _currentOp = value;
                OnPropertyChanged();
            }
        }

        private double _maxProgress;
        public double MaxProgress
        {
            get => _maxProgress;
            set
            {
#if DEBUG
                Debug.WriteLine($"[AM_ProgressC] MaxProgress: {value}");
#endif

                _maxProgress = value;
                OnPropertyChanged();
            }
        }*/

        private double _currentTotal;
        public double CurrentTotal
        {
            get => _currentTotal;
            set
            {
#if DEBUG
                Debug.WriteLine($"[M_ProgressC] {nameof(CurrentTotal)}: {value}");
#endif
                _currentTotal = value;
                OnPropertyChanged();
            }
        }


        private string _totalStatus;
        public string TotalStatus
        {
            get => _totalStatus;
            set
            {
#if DEBUG
                Debug.WriteLine($"[M_ProgressC] {nameof(TotalStatus)}: {value}");
#endif
                _totalStatus = value;
                OnPropertyChanged();
            }
        }

        #region Virtual
        public override void SetTaskToRun<T>(T taskToRun)
        {
            I_ASBaseC task2Run = (I_ASBaseC)taskToRun;
            base.TaskToRun = taskToRun;
            base.TaskToRun.UpdateProgress += UpdateProgress;
            base.TaskToRun.UpdateStatus += UpdateStatus;
            task2Run.UpdateTotalProgress += UpdateTotalProgress;
            task2Run.UpdateTotalStatus += UpdateTotalStatus;
        }

        protected virtual void UpdateTotalProgress(double value)
        {
            CurrentTotal = value;
        }
        protected virtual void UpdateTotalStatus(string value)
        {
            TotalStatus = value;
        }
        #endregion



    }
}
