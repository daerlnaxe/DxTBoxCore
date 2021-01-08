﻿using System;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DxTBoxCore.Controls
{
    /// <summary>
    /// Logique d'interaction pour ItemChecked.xaml
    /// </summary>
    public partial class ItemChecked : UserControl
    {
        public string Texte { get; set; }
        public bool Checked { get; set; }

        public ItemChecked()
        {
            InitializeComponent();
        }
    }
}
