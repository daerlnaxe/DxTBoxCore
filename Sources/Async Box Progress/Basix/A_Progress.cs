using AsyncProgress;
using AsyncProgress.Cont;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    public abstract class A_Progress : I_RProgress, I_TProgress
    {
        public abstract void SetStatus(object sender, StateArg value);
        
        // ---

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public string NameCOPType { get; set; }


        #region Progress

        private double _Progress;
        public virtual double Progress
        {
            get => _Progress;
            set
            {
                Debug.WriteLine($"[{nameof(Progress)}]: {value}");
                _Progress = value;

                OnPropertyChanged();
            }
        }


        #endregion

        // ---

        protected string _Status;
        public virtual string Status
        {
            get { return _Status; }
            set
            {
                _Status = value;
                OnPropertyChanged();
            }
        }

        // ---

        private double _Maximum;
        public virtual double MaxValue
        {
            get => _Maximum;
            set
            {

                Debug.WriteLine($"[{nameof(MaxValue)}]: {value}");

                _Maximum = value;
                OnPropertyChanged();

            }
        }


        // ---

        public void SetProgress(object sender, ProgressArg m)
        {
            MaxValue = m.Total;
            Progress = m.Progress;
        }

        


    }
}
