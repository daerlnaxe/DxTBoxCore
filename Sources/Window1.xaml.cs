using DxLocalTransf.Progress;
using DxLocalTransf.Progress.ToImp;
using DxTBoxCore.Box_Password;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.BoxChoose;
using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Threading;
using System.Windows;

namespace DxTBoxCore
{
    /// <summary>
    /// Logique d'interaction pour Window1.xaml
    /// </summary>
    public partial class Window1 : Window, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _Value;
        public ref string Value => ref _Value;

        KeyValuePair<string, int> one = new KeyValuePair<string, int>("Test1", 0);
        KeyValuePair<string, int> two = new KeyValuePair<string, int>("blue1", 1);

        public List<KeyValuePair<string, int>> FirstList { get; set; } = new List<KeyValuePair<string, int>>();

        public ObservableCollection<string> FirstCollection { get; set; } = new ObservableCollection<string>()
        {
            "Test2",
            "blue2",
        };

        private object _SelectedItem;
        public object SelectedItem
        {
            get
            {
                Debug.WriteLine("Selecteditem get");

                return _SelectedItem;
            }
            set
            {
                Debug.WriteLine($"set: {value}");
                _SelectedItem = value;
            }
        }

        public object ChoCho
        {
            get
            {
                Debug.WriteLine("Chocho Get");
                return null;
            }
            set
            {
                Debug.WriteLine("Chocho set");
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
            FirstList.Add(one);
            FirstList.Add(two);
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
            MawEvo maw = new MawEvo();
            var db2 = new DxAsCollecProgress(DxTBLang.File)
            {
                Model = maw,
                Launcher = BasicLauncher<MawEvo>.Create(maw, ()=> Foo(maw)),

            };
            /*           TaskToRun = ,
                           TaskToRun = new TestProgressCollec(),*/

            //db2.Execute_Code();
            //db2.Show();
            db2.ShowDialog();
            //    db2.Model.TaskRunning.Wait();

        }

        private void Simule_SimpleProgress(object sender, RoutedEventArgs e)
        {
            TestProgressSimple tps = new TestProgressSimple();

            Maw maw = new Maw();
            maw.RerouteSignal(tps);
        }

        private void Simule_SimpleProgressMaou(object sender, RoutedEventArgs e)
        {
            MawEvo maou = new MawEvo();

            DxAsCollecProgress db2 = new DxAsCollecProgress(DxTBLang.File)
            {
                Model = maou,
                Launcher = BasicLauncher<Maw>.Create(maou, () => Foo(maou, "Il est passé par ici")),
                /*
                new Maou<string, object>()
                {
                    ToRun = Foo,
                    Param = "Il est passé par ici"
                },*/
            };
            db2.ShowDialog();
        }


        private object Foo(I_ASBase arg)
        {
            return Foo(arg , null);
        }

        /// <summary>
        /// Une méthode juste pour montrer
        /// </summary>
        /// <param name="tP">Utilisée par le model implémenté</param>
        /// <returns>
        /// Ce que vous voulez, Maou est generique, si vous devez retourner 'void' laissez sur 'object'
        /// </returns
        private object Foo(I_ASBase tP = null, string mee = null)
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
            _Value = "quelque chose";
            //tBExt.Text = Value;
            //tBExt.SetText(ref _Value);
            //   PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));

            _Value = "rien";
            //PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Value)));
        }

        private void PasswordDec_AskDectryption(object sender, RoutedEventArgs e)
        {
            PasswordDec pass = (PasswordDec)sender;
            pass.ClearPassword = pass.EncryptedPass;
        }


        private void tbAC2_AskToAdd(object sender, RoutedEventArgs e)
        {
            FirstCollection.Add("Green");
        }


    }
}
