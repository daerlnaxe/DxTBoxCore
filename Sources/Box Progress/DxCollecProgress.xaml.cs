using DxTBoxCore.Collec;
using DxTBoxCore.Container;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace DxTBoxCore.Box_Progress
{
    /// <summary>
    /// Logique d'interaction pour DxCollecProgress.xaml
    /// </summary>
    /// <remarks>
    /// Lot of modifications in 2021, perhaps problems will exist
    /// </remarks>
    public partial class DxCollecProgress : Window
    {
        public MProgressCollec Model { get; set; } = new MProgressCollec();


        public bool StopIt { get; set; }
        public ObservableCollection<DxTaskInfo> Elements { get; set; }


        public DxCollecProgress()
        {
            InitializeComponent();
            DataContext = Model;
        }

        #region Current progressBar
        /* 2021
         * 
        private string _CurrentOP { get; set; }
        
        /// <summary>
        /// Current opération for an element
        /// </summary>
        public string CurrentOP
        {
            get { return _CurrentOP; }
            set
            {
                Console.WriteLine($"[DxCollecProgress] CurrentOp: {value}");
                _CurrentOP = value;
                if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentOP"));
            }
        }

        private double _MaxProgress { get; set; }
        
        /// <summary>
        /// MaxNumber task for an element
        /// </summary>
        public double MaxProgress
        {
            get { return _MaxProgress; }
            set
            {
                Console.WriteLine($"[DxCollecProgress] MaxProgress: {value}");
                _MaxProgress = value;
                if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MaxProgress"));

            }
        }
        

        private double _CurrentProgress { get; set; }

        /// <summary>
        /// Current position for an element
        /// </summary>
        public double CurrentProgress
        {
            get { return _CurrentProgress; }
            set
            {
                Console.WriteLine($"[DxCollecProgress] CurrentProgress: {value}");
                _CurrentProgress = value;
                if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentProgress"));
            }
        }
        */
        #endregion


        #region 

        public int MaxTotal { get; set; }

/* 2021
        private int _CurrentTotal { get; set; }

        /// <summary>
        /// Position on the total bar progress
        /// </summary>
        public int CurrentTotal
        {
            get { return _CurrentTotal; }
            set
            {
                Console.WriteLine($"[DxCollecProgress] CurrentTotal: {value}");
                _CurrentTotal = value;

                if (PropertyChanged != null)
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CurrentTotal"));
            }
        }
*/

        public void SetCurrentFinished(string result)
        {
            int pos = Model.CurrentTotal - 1;
            lvElements.SelectedIndex = pos;
            Elements[pos].Finished = true;
            Elements[pos].Result = result;
        //    if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elements"));

        }

        public void AsyncSetCurrentFinished(string result)
        {
            this.Dispatcher.BeginInvoke((Action)(()=> SetCurrentFinished(result)));
        }


        #endregion


        /*
                public void SetColumns(Dictionary<string, string> colonnes)
                {
                    Headers.Columns.Clear();
                    foreach (KeyValuePair<string,string> kvp in colonnes)
                    {
                        GridViewColumn gvc = new GridViewColumn();
                        gvc.Header = kvp.Key;
                        gvc.DisplayMemberBinding = new Binding(kvp.Value);

                        Headers.Columns.Add(gvc);
                    }

                    GridViewColumn state = new GridViewColumn();
                    state.Header = "State";
                    Headers.Columns.Add(
                }
        */
        /// <summary>
        /// Ajoute les élements à la liste
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        public void SetCollection<T>(List<T> elements, string property)
        {
            /*GridViewColumn gvc = new GridViewColumn();
            gvc.Header = "Taches";           

            Console.WriteLine("SetCollection");*/
            // lvElements.Items.Clear();
            Elements = new ObservableCollection<DxTaskInfo>();
            foreach (T element in elements)
            {
                var val = typeof(T).GetProperty(property).GetValue(element).ToString();
                Console.WriteLine(val);
                Elements.Add(new DxTaskInfo() { TaskName= val});
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="elements"></param>
        public void SetCollection<T>(T[] elements, string property)
        {
            Elements = new ObservableCollection<DxTaskInfo>();
            foreach (T element in elements)
            {
                var val = typeof(T).GetProperty(property).GetValue(element).ToString();
                Console.WriteLine(val);
                Elements.Add(new DxTaskInfo() { TaskName = val });
            }
        }



        public void SetDisplay(string property)
        {
            throw new NotImplementedException();
        }

        private void StopAll_Click(object sender, RoutedEventArgs e)
        {
            StopIt = true;
        }

        public void AsyncClose()
        {
            this.Dispatcher.BeginInvoke((Action)(() => this.Close()));
        }

    }
}
