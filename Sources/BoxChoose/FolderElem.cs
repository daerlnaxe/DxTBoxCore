using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public sealed class FolderElem : I_ContChoose
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public E_IconFType Type { get; }

        public string Name { get; set; }

        public string Path { get; set; }

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

        private bool _isExpanded;
        public bool IsExpanded
        {
            get => _isExpanded;
            set
            {
                _isExpanded = value;
                Debug.WriteLine($"{Name}: IsExpanded {value}");
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Permit to avoid selection on files if unwanted
        /// </summary>
        /// <remarks>
        /// Always true for folders type
        /// </remarks>
        public bool IsFocusable => true;


        public ObservableCollection<I_ContChoose> Children { get; set; }


        public FolderElem(E_IconFType type)
        {
            Type = type;
            // Children = new List<ContFChoose>();
        }
    }
}
