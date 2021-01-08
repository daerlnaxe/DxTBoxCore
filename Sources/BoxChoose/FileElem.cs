using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public sealed class FileElem : I_ContChoose
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        private bool _isSelected;
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;
                Debug.WriteLine($"{Name}: IsSelected {value}");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Element Expand state
        /// </summary>
        /// <remarks>
        /// can't be expanded
        /// </remarks>
        public bool IsExpanded
        {
            get => false;
            set 
            { 
            }
        }

        public bool IsFocusable { get; set; }

        public E_IconFType Type => E_IconFType.File;

        public string Name { get; set; }
        public string Path { get; set; }

        public ObservableCollection<I_ContChoose> Children => null;

    }
}
