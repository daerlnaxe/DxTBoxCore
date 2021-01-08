using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace DxTBoxCore.BoxChoose
{
    public abstract class A_ModelChoose : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        }



        /// <summary>
        /// Utilisé dans le cas où un dossier n'est pas vide
        /// </summary>
        protected object dummyNode { get; set; } = null;

        /// <summary>
        /// Génère des items pour les supports physiques
        /// </summary>
        protected void Populate_Drives()
        {
            foreach (DriveInfo driv in DriveInfo.GetDrives())
            {
                // Pour chaque lecteur on fait un treeviewitem
                TreeViewItem tvItem = new TreeViewItem();
                string letDrive = driv.Name.TrimEnd('\\');

                switch (driv.DriveType)
                {
                    case DriveType.CDRom:
                        if (driv.IsReady)
                        {
                            tvItem.Header = $"({letDrive}) {driv.VolumeLabel}";
                            tvItem.Items.Add(dummyNode);
                            tvItem.Expanded += new RoutedEventHandler(Developpement_Dossier);
                            tvItem.Selected += Select_Item;
                        }
                        else
                        {
                            tvItem.Header = $"({letDrive}) Lecteur Optique";
                            FilePath.Text = "Lecteur non prêt";
                        }
                        break;
                    default:
                        tvItem.Header = $"({letDrive}) {driv.VolumeLabel}";
                        tvItem.Items.Add(dummyNode);
                        tvItem.Expanded += new RoutedEventHandler(Developpement_Dossier);
                        tvItem.Selected += Select_Item;

                        break;
                }


                tvItem.Tag = driv.Name;
                tvItem.FontWeight = FontWeights.Normal;

                FileTree.Items.Add(tvItem);
                //Console.WriteLine(driv);
            }
        }
    }

}

