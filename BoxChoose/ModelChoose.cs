using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public class ModelChoose : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }

        private string _startingFolder;
        /// <summary>
        /// Start from this directory
        /// </summary>
        public string StartingFolder
        {
            get => _startingFolder;
            set
            {
                if (string.IsNullOrEmpty(value))
                    value = null;
                _startingFolder = value;
                OnPropertyChanged();
            }
        }

    }
}
