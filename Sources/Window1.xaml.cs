using DxTBoxCore.Box_Password;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using DxTBoxCore.Box_Password;
using System.Collections.ObjectModel;

namespace DxTBoxCore
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Value;
        public ref string Value =>ref _Value;

        KeyValuePair<string, int> one = new KeyValuePair<string, int>("mee",0);
        KeyValuePair<string, int> two = new KeyValuePair<string, int>("bluu",1);

        public List<KeyValuePair<string, int>> Caca { get; set; } = new List<KeyValuePair<string, int>>();
        
        public ObservableCollection<string> Caca2 { get; set; } = new ObservableCollection<string>() 
        { 
            "mee2",
            "bluu2",
        };

        private object _SelectedItem;
        public object SelectedItem
        {
            get => _SelectedItem;
            set
            {
                Debug.WriteLine($"set: {value}");
                _SelectedItem = value;
            }
        }

        /// <summary>
        /// Lanceur
        /// </summary>
        [STAThread]
        public static void Main()
        {
            
            new Window1().ShowDialog();

        }

        

        public Window1()
        {
            Caca.Add(one);
            Caca.Add(two);
            SelectedItem = two;
            InitializeComponent();
            tbAC1.pUP.IsOpen = true;
            DataContext = this;
            //tbAC1.AvailableItems2 = Caca2;
        }
        private void Open_ChooseFile(object sender, RoutedEventArgs e)
        {
            ChooseFile cf = new ChooseFile()
            {

            };
            cf.ShowDialog();
        }

        /// <summary>
        /// Open a Box to Choose a Folder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_ChooseFolder(object sender, RoutedEventArgs e)
        {
            /*ChooseFolder cf = new ChooseFolder()
            {
            };
            cf.ShowDialog();*/
            TreeChoose cfe = new TreeChoose()
            {
                Model = new M_ChooseFolder()
                {
                    StartingFolder = @"C:",
                    HideWindowsFolder = true,
                    PathCompareason = StringComparison.CurrentCultureIgnoreCase,
                    //Mode = ChooseMode.Folder

                }
            };

            cfe.ShowDialog();
        }

        private void Simule_DoubleProgress(object sender, System.Windows.RoutedEventArgs e)
        {

            var db2 = new DxAsCollecProgress(DxTBLang.File)
            {

                /*TaskToRun = new Maou<bool>()
                {
                    ToRun = Foo

                },*/
                TaskToRun = new TestProgressCollec(),
            };


            //db2.Execute_Code();
            //db2.Show();
            db2.ShowDialog();
            //    db2.Model.TaskRunning.Wait();

        }

        private void Simule_SimpleProgress(object sender, RoutedEventArgs e)
        {
            DxAsCollecProgress db2;

            db2 = new DxAsCollecProgress(DxTBLang.File)
            {
                TaskToRun = new Maou<string, object>()
                {
                    ToRun = Foo,
                    Param = "Il est passé par ici"
                },
            };
            db2.ShowDialog();
        }

        /// <summary>
        /// Une méthode juste pour montrer
        /// </summary>
        /// <param name="tP">Utilisée par le model implémenté</param>
        /// <returns>
        /// Ce que vous voulez, Maou est generique, si vous devez retourner 'void' laissez sur 'object'
        /// </returns
        private object Foo(I_ASBase tP, string mee = null)
        {
            Debug.WriteLine(mee);

            while (!tP.CancelToken.IsCancellationRequested)   // Stop le programme si l'annulation est requise
            {
                while (tP.IsPaused) // Pause la tâche lorsque l'utiilsateur appuie sur le bouton de fermeture de la fenêtre
                {
                    Debug.WriteLine("I stop the world and melt with you");
                    Thread.Sleep(100);
                }
                Debug.WriteLine("Moving forward using all my breath");
                Thread.Sleep(100);
            }

            Debug.WriteLine("Dream of better lives the kind which never hates");
            return null;
        }

        /// <summary>
        /// Show PasswordBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Open_PasswordBox(object sender, RoutedEventArgs e)
        {
            DxSoloPass.ShowModal();

        }

        private void Open_PasswordDBox(object sender, RoutedEventArgs e)
        {
            DxDoublePass.ShowModal();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {

        }

        /// <summary>
        /// Initialize les TextBox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InitTB_Click(object sender, RoutedEventArgs e)
        {
            _Value = "merde";
            //tBExt.Text = Value;
            //tBExt.SetText(ref _Value);
         //   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));

            _Value = "rien";
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        private void PasswordDec_AskDectryption(object sender, RoutedEventArgs e)
        {
            PasswordDec pass = (PasswordDec)sender;
            pass.ClearPassword = pass.EncPassword;
                  }


        private void tbAC2_AskToAdd(object sender, RoutedEventArgs e)
        {
            Caca2.Add("Glabou");
        }
    }
}
