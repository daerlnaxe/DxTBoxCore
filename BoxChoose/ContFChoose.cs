using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public class ContFChoose : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private bool _isSelected;
        /// <summary>
        /// Element is Selected
        /// </summary>
        /// <remarks>
        /// Activer le mode two way
        /// </remarks>
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
        /// <summary>
        /// Element is Expanded
        /// </summary>
        /// <remarks>
        /// Activer le mode two way
        /// </remarks>
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

        public E_IconFType Type { get; }

        public string Name { get; set; }

        public string Path { get; set; }



        //   public bool AccessGranted { get; set; } = true;

        public ObservableCollection<ContFChoose> Children { get; set; }

        public ContFChoose(E_IconFType type)
        {
            Type = type;
            // Children = new List<ContFChoose>();
        }

    }
}
