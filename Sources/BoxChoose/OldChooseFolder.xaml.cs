﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
using Path = System.IO.Path;
using DxTBoxCore.Properties;

namespace DxTBoxCore.BoxChoose
{
    /// <summary>
    /// Logique d'interaction pour ChooseRep.xaml
    /// </summary>
    [Obsolete("Use TreeChoose")]
    public partial class OldChooseFolder : Window
    {
        /// <summary>
        /// Datacontext model
        /// </summary>
        public A_ModelChoose Model { get; set; } = new M_ChooseFolder();

        #region Relais vers le Modèle
        /// <summary>
        /// Folder used to initialize
        /// </summary>
        public string StartingFolder 
        { 
            get => Model.StartingFolder;
            set => Model.StartingFolder = value;            
        }
        #endregion







        /// <summary>
        /// 
        /// </summary>
        public List<string> Extensions { get; set; } = new List<string>();



        /// <summary>
        ///
        /// </summary>
        public string LinkResult { get; private set; }

        /// <summary>
        /// Name of Save Button
        /// </summary>
        public string SaveButtonName { get; set; } = "Save";

        /// <summary>
        /// Name of Cancel Button
        /// </summary>
        public string CancelButtonName { get; set; } = "Cancel";

        public OldChooseFolder()
        {
            InitializeComponent();
            DataContext = Model;

        }

        /*
        private void FileTree_Loaded(object sender, RoutedEventArgs e)
        {
            Fill_TreeView();
            if (!string.IsNullOrEmpty(StartingFolder))
            {
                tbStartingFolder.Text = StartingFolder;
                Recherche(StartingFolder);
            }
        }*/

        /// <summary>
        /// Construit la racine de l'arbre
        /// </summary>
        private void Fill_TreeView()
        {
            // Cas des répertoires utilisateurs
            string bureau;
            string mesDocs;
            string mesImgs;
            string maMus;
            string mesVids;

            UserFolders.Bureau = bureau = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            UserFolders.Documents = mesDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            UserFolders.Images = mesImgs = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            UserFolders.Musiques = maMus = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            UserFolders.Videos = mesVids = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            /*
            #region Concerne les répértoires spéciaux
            // Bureau
            TreeViewItem itmBur = new TreeViewItem() { Header = "Bureau", Tag = bureau };
            if (Has_Content(bureau))
            {
                itmBur.Items.Add(dummyNode);
                itmBur.Expanded += Developpement_Dossier;
            }
            //profile.Items.Add(itmBur);

            // Mes documents
            TreeViewItem itmDocs = new TreeViewItem() { Header = "Mes Documents", Tag = mesDocs };
            if (Has_Content(mesDocs))
            {
                itmDocs.Items.Add(dummyNode);
                itmDocs.Expanded += Developpement_Dossier;
            }
            //profile.Items.Add(itmDocs);

            // Mes Images
            TreeViewItem itmImgs = new TreeViewItem() { Header = "Mes Images", Tag = mesImgs };
            if (Has_Content(mesImgs))
            {
                itmImgs.Items.Add(dummyNode);
                itmImgs.Expanded += Developpement_Dossier;
            }

            // Ma Musique
            TreeViewItem itmMus = new TreeViewItem() { Header = "Ma Musique", Tag = maMus };
            if (Has_Content(maMus))
            {
                itmMus.Items.Add(dummyNode);
                itmMus.Expanded += Developpement_Dossier;
            }
            //profile.Items.Add(itmMus);

            // Mes vidéos
            TreeViewItem itmVid = new TreeViewItem() { Header = "Mes Vidéos", Tag = mesVids };
            if (Has_Content(mesVids))
            {
                itmVid.Items.Add(dummyNode);
                itmVid.Expanded += Developpement_Dossier;
            }
            //profile.Items.Add(itmVid);
            #endregion
            //profile.Items.Add( dummyNode);
            */
            //var ok = FileTree.Items;
            Make_Folder(FileTree.Items, "Bureau", bureau);
            //FileTree.Items.Add(itmBur);
            Make_Folder(FileTree.Items, "Mes Documents", mesDocs);
            //FileTree.Items.Add(itmDocs);
            Make_Folder(FileTree.Items, "Mes Images", mesImgs);
            //FileTree.Items.Add(itmImgs);
            Make_Folder(FileTree.Items, "Ma Musique", maMus);
            //FileTree.Items.Add(itmMus);
            Make_Folder(FileTree.Items, "Mes Vidéos", mesVids);

           // Model.Populate_Drives();
        }

        /// <summary>
        /// Add items to the threeview for special folders
        /// </summary>
        /// <param name="itemCollec"></param>
        /// <param name="headVal"></param>
        /// <param name="tagVal"></param>
        private void Make_Folder(ItemCollection itemCollec, string headVal, string tagVal)
        {
            TreeViewItem itm = new TreeViewItem()
            {
                Header = headVal,
                Tag = tagVal
            };

            // If folder has content, add dummy node to give possibilité to expand it later
            if (CommonChoose.Has_Content(tagVal))
            {
            //    itm.Items.Add(dummyNode);
               // itm.Expanded += Developpement_Dossier;
            }

            itm.Selected += Select_Item;

            //FileTree.Items.Add(itm);
            itemCollec.Add(itm);
        }


      
        
        




        /*
        private void Load_Drive(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            string drive = item.Tag.ToString();
            Debug.WriteLine($"Chargement du lecteur {drive}");
            try
            {
                // Liste des dossiers
                item.Items.Clear();
                foreach (string s in Directory.GetDirectories(drive))
                {

                    TreeViewItem tvItem = new TreeViewItem();
                    tvItem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                    tvItem.Tag = s;

                    // Vérification si le dossier est vide
                    if (Has_Content(s))
                    {
                        tvItem.Items.Add(dummyNode);
                        tvItem.Expanded += new RoutedEventHandler(Developpement_Dossier);
                    }

                    item.Items.Add(tvItem);


                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
        }
        */

        /*

        private void Developpement_Dossier(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;

            string chemchem = item.Tag.ToString();
            Console.WriteLine($"Développement du dossier {item.Tag}");

            try
            {
                if ((item.Items.Count == 1) && (item.Items[0] == dummyNode))    // Obligatoire pour les nodes enfants
                {
                    item.Items.Clear(); //Supression du pseudo Item

                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        Make_Folder(item.Items, s.Substring(s.LastIndexOf("\\") + 1), s);
                    }


                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            e.Handled = true;
        }
        */

        #region vieux
        /*
                /// <summary>
                /// 
                /// </summary>
                /// <param name="link"></param>
                private void search(string link)
                {
                    string[] lectArr = link.Split('\\');

                    MatchItem(lectArr, FileTree.Items);

                    Console.WriteLine("Fin match item");
                }*/

        /*
                /// <summary>
                /// 
                /// </summary>
                /// <returns></returns>
                private bool oldMatchItem(string[] refArray, ItemCollection nodeTV)
                {
                    // Parcours des composants du lien
                    for (int i = 0; i < refArray.Count(); i++)
                    {
                        Debug.WriteLine("niveau de la chaine: " + refArray[i]);

                        foreach (TreeViewItem zeItem in nodeTV)
                        {
                            Debug.WriteLine("Niveau dans l'arbre: " + zeItem.Tag);
                            Debug.WriteLine("Niveau dans l'arbre: " + zeItem.Header);
                            string caca = zeItem.Header.ToString();
                            caca = caca.TrimEnd('\\');

                            //Sortie si on ne trouve pas
                            if (refArray[i].IndexOf(caca, StringComparison.OrdinalIgnoreCase) == -1) continue;
                            zeItem.IsExpanded = true;
                            zeItem.IsSelected = true;
                            if (i + 1 == refArray.Count())
                            {
                                //Sortie si on a fini
                                Console.WriteLine("Trouvé et fin");
                                return true;
                            }

                            Console.WriteLine("trouvé mais il en manque");
                            MatchItem(refArray, zeItem.Items);

                            break;


                            Console.WriteLine("matchitem");

                        }



                    }



                    /*
                        if (i + 1 == refArray.Count())
                        {
                            return true;
                        }
                        else
                        {
                            //ItemCollection cmlur = treeVWItem.Items;
                            Console.WriteLine("Kamoulox");
                        }
                        */

        /*


        foreach (TreeViewItem zeItem in treeVWItem.Items)
        {
            if (zeItem is null)
            {
                Console.WriteLine(treeVWItem.Header.ToString() + "est null");
                continue;
            }
            else
            {
                Console.WriteLine("go");
            }


            Console.WriteLine("test");
            string caca = zeItem.Header.ToString();
            caca = caca.TrimEnd('\\');

            Console.WriteLine($"-> {refArray[i]} : {caca} <-");
            if ( refArray[i].IndexOf(caca, StringComparison.OrdinalIgnoreCase) > -1  )
            {
                zeItem.IsExpanded = true;


                Console.WriteLine("Similitude");

                if (zeItem.HasItems)
                {
                    Console.WriteLine("chameau");
                    MatchItem(refArray, zeItem);
                }

                return true; 
                break;
            }

        }
        */
        /*


        return false;
    }
*/


        #endregion
        #region private
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

            TextBox tx = (TextBox)sender;
            Recherche(tx.Text);

        }

        private void Recherche(string chaine)
        {

            // Sécurité pour éviter les erreurs
            if (FileTree == null) return;

            string[] lectArr = chaine.Split('\\');
            if (MatchItem(lectArr, FileTree.Items) == false)//lectArr, FileTree.Items))
            {
                Debug.WriteLine("Recherche infructueuse");
            }
            else
            {
                Debug.WriteLine("Recherche fructueuse");
            }

            //search(tx.Text);
        }

        private bool? MatchItem(string[] refArray, ItemCollection nodeTV, ushort lvl = 0)
        {
            bool result = false;
            string toSearch = refArray[lvl];
            Debug.WriteLine($"Recherche de '{toSearch}'");

            foreach (TreeViewItem itm in nodeTV)
            {

                string strCompare = itm.Header.ToString();
                if (lvl == 0) strCompare = itm.Tag.ToString().TrimEnd('\\');

                Debug.WriteLine("Test pour: " + strCompare);

                result = toSearch.Equals(strCompare);

                if (result)
                {
                    Debug.WriteLine($"Concordance {itm.Header}");
                    itm.IsExpanded = true;
                    itm.IsSelected = true;
                    itm.BringIntoView();


                    Debug.WriteLine(lvl + "--" + refArray.Length);
                    if (lvl + 1 < refArray.Length)
                    {
                        lvl++;
                        MatchItem(refArray, itm.Items, lvl);


                    }
                    return true;
                }
                else
                {
                    itm.IsExpanded = false;
                }


            }



            if (!result && lvl == 0)
            {
                return false;
            }

            return null;
        }


        private void Select_Item(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;
            Debug.WriteLine($"Selection de: {tvi.Tag}");
            FilePath.Text = tvi.Tag.ToString();
            e.Handled = true;                           // Très important, interrompt le signal donc les parents ne seront pas sollicités
        }

        private void btOk_Click(object sender, RoutedEventArgs e)
        {
            LinkResult = FilePath.Text;
            DialogResult = true;
        }

        private void btAnnul_Click(object sender, RoutedEventArgs e)
        {
            LinkResult = null;
            DialogResult = false;
        }

        private void tbStartingFolder_GotFocus(object sender, RoutedEventArgs e)
        {
            tbStartingFolder.Text = "";
        }
        #endregion
    }
}

