using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Windows.Threading;

namespace DxTBoxCore.Async_Box_Progress.Basix
{
    /// <summary>
    /// Model to communicate with a window progress managing a collection
    /// </summary>
    /// <remarks>
    /// Contient:
    ///     - Dynamic update
    /// Lock obligatoire pour ajouter à la collection
    /// C'est au niveau de la gestion de la collection que ça se distingue de M_ProgressC car il cumule
    /// le texte
    /// </remarks>
    /// 
    public abstract class A_ProgressPersist : A_Progress, I_TProgress
    {
        protected string _Status;
        public override string Status
        {
            get { return _Status; }
            set
            {
                _Status += value;
                OnPropertyChanged();
            }
        }


        public override void SetStatus(object sender, string value)
        {
            Status = $"{value} ";
        }

        public override void SetStatusNL(object sender, string value)
        {
            Status = $"{value}\r\n";
        }


        /*
            public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            try
            {

                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
            catch
            {
#if DEBUG
                Debug.WriteLine($"Erreur sur {propertyName}");
#endif
            }
        }

        //lock object for synchronization;
        private static object _syncLock = new object();


        public string NameCOPType { get; set; }


        private ObservableCollection<string> _collec = new ObservableCollection<string>();
        private double _currentProgress;
        public double CurrentProgress
        {
            get => _currentProgress;
            set
            {
                try
                {
#if DEBUG
                    Debug.WriteLine($"[{nameof(A_ProgressL)}] CurrentProgress: {value}");
#endif
                    _currentProgress = value;
                    OnPropertyChanged();
                }

                catch
                {
#if DEBUG
                    Debug.WriteLine($"Erreur sur {nameof(CurrentProgress)}");
#endif
                }
            }
        }


/*
        public virtual ObservableCollection<string> CurrentOP
        {
            get { return _collec; }
            set
            {
                //  Debug.WriteLine("Debug: List Set");
                _collec = value;
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
        }
            */
        /*
    public A_ProgressL()
    {
        // Important to make a thread-safe update of the collection
        BindingOperations.EnableCollectionSynchronization(_collec, _syncLock);
    }

    public void AddToCollec(string value)
    {
        lock (_syncLock)
        {
            CurrentOP.Add(value);
        }
        OnPropertyChanged(nameof(Collec));   
    }*/
        /*
    public virtual void SetProgress(object sender, double value)
    {
        CurrentProgress = value;
    }

    public virtual void SetStatus(object sender, string value)
    {            
        AddToCollec($"{value}");
    }

    public virtual void SetMaximum(object sender, double value)
    {
        MaxProgress = value;
    }

        */
    }
}
