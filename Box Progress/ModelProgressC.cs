using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Model to communicate with a window progress managing a collection
    /// </summary>
    /// <remarks>
    /// Contient:
    ///     - Dynamic update
    /// Lock obligatoire pour ajouter à la collection
    /// </remarks>
    public class ModelProgressC : M_ProgressC
    {
        //lock object for synchronization;
        private static object _syncLock = new object();
        public ObservableCollection<string> _collec = new ObservableCollection<string>();

        public ObservableCollection<string> Collec
        {
            get { return _collec; }
            set
            {
                //  Debug.WriteLine("Debug: List Set");
                _collec = value;
                OnPropertyChanged();
            }
        }

        public ModelProgressC()
        {
            // Important to make a thread-safe update of the collection
            BindingOperations.EnableCollectionSynchronization(_collec, _syncLock);
        }

        public void AddToCollec(string value)
        {
            lock (_syncLock)
            {
                Collec.Add(value);
            }
            OnPropertyChanged(nameof(Collec));

            Debug.WriteLine("Debug: AddToCollec");
        }

        public override void UpdateStatus(string value)
        {
            AddToCollec(value);
        }
        
        protected override void UpdateTotalStatus(string value)
        {
            TotalStatus = value;
            AddToCollec($">>> {value} <<<");
        }
    }
}
