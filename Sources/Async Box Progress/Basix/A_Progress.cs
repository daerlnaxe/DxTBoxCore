using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    public abstract class A_Progress : I_RProgress, I_TProgress
    {
        public abstract string Status { get; set; }

        public abstract void SetStatus(object sender, string value);

        public abstract void SetStatusNL(object sender, string value);

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

        private double _Maximum;
        public virtual double MaxValue
        {
            get => _Maximum;
            set
            {

                Debug.WriteLine($"[MaxProgress]: {value}");

                _Maximum = value;
                OnPropertyChanged();

            }
        }

        // ---

        public virtual void SetProgress(object sender, double value)
        {
            Progress = value;
        }

        public virtual void SetMaximum(object sender, double value)
        {
            MaxValue = value;
        }


    }
}
