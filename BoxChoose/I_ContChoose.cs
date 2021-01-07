using System.Collections.ObjectModel;
using System.ComponentModel;

namespace DxTBoxCore.BoxChoose
{
    public interface I_ContChoose : INotifyPropertyChanged
    {

        /// <summary>
        /// Element is Selected
        /// </summary>
        /// <remarks>
        /// Activer le mode two way
        /// </remarks>
        public bool IsSelected { get; set; }
      
        /// <summary>
        /// Element Expand state
        /// </summary>
        /// <remarks>
        /// Activer le mode two way
        /// </remarks>
        public bool IsExpanded { get; set; }

        /// <summary>
        /// Permit to avoid selection on files if unwanted
        /// </summary>
        public bool IsFocusable { get; }

        /// <summary>
        /// Type of element
        /// </summary>
        public E_IconFType Type { get; }

        /// <summary>
        /// Name showed
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }



        //   public bool AccessGranted { get; set; } = true;
        /// <summary>
        /// Collection of children
        /// </summary>
        public ObservableCollection<I_ContChoose> Children { get; }



    }
}
