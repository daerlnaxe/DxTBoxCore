using System;
using System.Collections.Generic;
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

namespace DxTBoxCore.Images
{
    /// <summary>
    /// Logique d'interaction pour BigPic.xaml
    /// </summary>
    public partial class BigPic : Window
    {
        public ImageSource ImageLink { get; set;}

        public BigPic()
        {
            InitializeComponent();
        }
    }
}
