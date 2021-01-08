using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Container
{
    public class DxTaskInfo : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public string TaskName { get; set; }

        private bool _Finished;
        public bool Finished
        {
            get { return _Finished; }
            set
            {
                _Finished = value;
                if (PropertyChanged != null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Finished"));
            }
        }

        private string _Result;
        public string Result
        {
            get { return _Result; }
            set
            {
                _Result = value;
                if (PropertyChanged != null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Result"));
            }
        }

    }
}
