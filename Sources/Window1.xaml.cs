﻿using AsyncProgress;
using AsyncProgress.Tests;
using AsyncProgress.Tools;
using DxTBoxCore.Async_Box_Progress;
using DxTBoxCore.Box_Password;
using DxTBoxCore.Box_Progress;
using DxTBoxCore.Box_Status;
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

        private void Test_GameStatus(object sender, RoutedEventArgs e)
        {
            ColumnStatus win = new ColumnStatus();
            win.States = new Dictionary<string, bool?>()
            {
                { "game", false },
                { "musique", true } ,
                { "Pays", null }
            };
            win.TrueColor = System.Windows.Media.Colors.GreenYellow;
            win.FalseColor = System.Windows.Media.Colors.Red;
            win.NullColor = System.Windows.Media.Colors.DarkGray;
            win.Buttons = Common.E_DxButtons.Ok | Common.E_DxButtons.Cancel;
            //fonctionne
            /*var uri = new Uri("/DxTBoxCore;component/Themes/Basic.xaml", UriKind.Relative) ;
            win.Resources.MergedDictionaries.Add(Application.LoadComponent(uri) as ResourceDictionary);*/
            //var uri = new Uri("/DxTBoxCore;component/Themes/DarkStyle.xaml", UriKind.Relative) ;
            //var dic = Application.LoadComponent(uri) as ResourceDictionary;
            //win.Resources.MergedDictionaries.Clear();
            //win.Resources.MergedDictionaries.Add(dic);
            win.ShowDialog();
        }


        private void Open_ChooseFile(object sender, RoutedEventArgs e)
        {
            TreeChoose cf = new TreeChoose()
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

        private void Simule_Another(object sender, RoutedEventArgs e)
        {
            TestProgressSimple tps = new TestProgressSimple();
            TaskLauncher al = new TaskLauncher()
            {
                ProgressIHM = new DxProgressB1()
                {
                    Model = new EphemProgress(tps)
                },
                //Objet = tps,
                MethodToRun = () => tps.Run()
            };
            al.Launch(tps);

        }

        private void Simule_Another_Double(object sender, RoutedEventArgs e)
        {
            TestProgressCollec tpc = new TestProgressCollec();
            TaskLauncher al = new TaskLauncher()
            {
                ProgressIHM = new DxStateProgress()
                {
                    Model = new EphemProgressD(tpc)
                },
                //Objet = tpc,
                MethodToRun = () => tpc.Run(50),
            };
            al.Launch(tpc);
        }

        private void Simule_AnotherMaou(object sender, RoutedEventArgs e)
        {
            Maw<EphemProgress> maw = new Maw<EphemProgress>();

            TaskLauncher al = new TaskLauncher()
            {
                ProgressIHM = new DxProgressB1()
                {
                },
                //Objet = maw,
                //    MethodToRun = () => Foo(maw),
            };

            al.Launch(maw);

        }

        private void Simule_SimpleProgress(object sender, RoutedEventArgs e)
        {
            TestProgressSimple tps = new TestProgressSimple();

            Maw<EphemProgress> maw = new Maw<EphemProgress>();
            maw.RerouteSignal(tps);

            DxAsProgress window = new DxAsProgress()
            {
                Model = maw.Parler,

                Launcher = BasicLauncher<Maw<EphemProgress>>.Create(maw, () => tps.Run()),
            };
            window.ShowDialog();
        }

        private void Simule_DoubleProgress(object sender, RoutedEventArgs e)
        {
            TestProgressCollec tps = new TestProgressCollec();

            MawEvo<EphemProgressD> maw = new MawEvo<EphemProgressD>(tps);

            DxAsDoubleProgress window = new DxAsDoubleProgress()
            {
                Model = maw.Parler,
                Launcher = BasicLauncher<MawEvo<EphemProgressD>>.Create(maw, () => tps.Run(10)),
            };
            window.ShowDialog();
        }

        private void Simule_SimpleProgressMaou(object sender, RoutedEventArgs e)
        {
            var maou = new MawEvo<EphemProgressD>();

            DxAsCollecProgress db2 = new DxAsCollecProgress(DxTBLang.File)
            {
                Model = maou.Parler,
                Launcher = BasicLauncher<MawEvo<EphemProgressD>>.Create(maou, () => Foo(maou, "Il est passé par ici")),
                /*
                new Maou<string, object>()
                {
                    ToRun = Foo,
                    Param = "Il est passé par ici"
                },*/
            };
            db2.ShowDialog();
        }

        private void Simule_DoubleProgressMaou(object sender, System.Windows.RoutedEventArgs e)
        {
            var maw = new MawEvo<EphemProgressD>();
            var db2 = new DxAsCollecProgress(DxTBLang.File)
            {
                Model = maw.Parler,
                Launcher = BasicLauncher<MawEvo<EphemProgressD>>.Create(maw, () => Foo(maw)),

            };
            /*           TaskToRun = ,
                           TaskToRun = new TestProgressCollec(),*/

            //db2.Execute_Code();
            //db2.Show();
            db2.ShowDialog();
            //    db2.Model.TaskRunning.Wait();

        }

        private object Foo(I_ASBase arg)
        {
            return Foo(arg, null);
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
