using DxTBoxCore.Common;
using DxTBoxCore.Box_MBox;
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

namespace DxTBoxCore.Collec
{
    /// <summary>
    /// Logique d'interaction pour DxMCollec.xaml
    /// </summary>
    public partial class DxMCollec : Window, IDxCollec
    {
        public Type zeType;
        public ObservableCollection<dynamic> Elements { get; set; }
        public string Message { get; set; }

        public DxMCollec()
        {
            InitializeComponent();
            DataContext = this;            
        }

        public DxMCollec(List<string> elements)
        {
            InitializeComponent();
            DataContext = this;
        }

        public void SetCollection<T>(List<T> collection)
        {
            Elements = new ObservableCollection<dynamic>();
            collection.ForEach(x=> Elements.Add(x));          
        }

        public void SetCollection<T>(T[] collection)
        {
            Elements = new ObservableCollection<dynamic>();
            foreach (T elem in collection) Elements.Add(elem);            
        }

        public void SetDisplay(string property)
        {
            this.lboxElements.DisplayMemberPath = property;
        }


        /// <summary>
        /// Remove element
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void miRemove_Click(object sender, RoutedEventArgs e)
        {
            if (lboxElements.SelectedIndex == -1) return;

            Elements.RemoveAt(lboxElements.SelectedIndex);
            
                                                        

            ////////////////string elem = (string)lboxElements.SelectedItem;
            ////////////////MessageBox.Show(elem);
            ////////////////int index = lboxElements.SelectedIndex;
            ////////////////MessageBox.Show(index.ToString());
            ////////////////Elements.RemoveAt(index);

            ////////////////foreach (string elemts in Elements)
            ////////////////{
            ////////////////    Console.WriteLine(elemts);
            ////////////////}
            ////////////////Console.WriteLine();
            // if (PropertyChanged != null) PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Elements"));
        }

        private void btAnnul_Click(object sender, RoutedEventArgs e)
        {
            // check if modal or not
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                this.DialogResult = false;
            }
            else
            {
                DxMBox.ShowDial("It's not in modal mode", "!!!", E_DxButtons.Ok);
            }
            this.Close();
        }

        private void btValid_Click(object sender, RoutedEventArgs e)
        {
            // check if modal or not
            if (System.Windows.Interop.ComponentDispatcher.IsThreadModal)
            {
                this.DialogResult = true;

            }
            else
            {
                DxMBox.ShowDial("It's not in modal mode", "!!!", E_DxButtons.Ok);
            }
            this.Close();
        }

        /*
        public static DxMBox_Res ShowDialog(string[] elements)
        {
            DxMCollec window = new DxMCollec();
            window.Elements = elements;
            window.Show();
        }*/
    }
}
