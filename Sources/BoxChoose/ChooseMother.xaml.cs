using System;
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
    public partial class ChooseMother : Window
    {
        private object dummyNode = null;
        public List<string> Extensions { get; set; } = new List<string>();

        public string LinkResult { get; private set; }

        /// <summary>
        /// Changer le nom du bouton de droite (sauvegarde)
        /// </summary>
        /// <param name="nom"></param>
        public void SetSaveButton(string nom)
        {
            btOk.Content = nom;
        }

        /// <summary>
        /// Changer le nom du bouton de gauche (annuler)
        /// </summary>
        public void SetCancelButton(string nom)
        {
            btAnnul.Content = nom;
        }


        public ChooseMother()
        {
            InitializeComponent();

        }

        private void FileTree_Loaded(object sender, RoutedEventArgs e)
        {
            Fill_TreeView();
        }

        private void Fill_TreeView()
        {
            // Cas des répertoires utilisateurs
            string bureau;
            string mesDocs;
            string mesImgs;
            string maMus;
            string mesVids;

            Settings.Default.Bureau = bureau = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            Settings.Default.DocumentsPerso = mesDocs = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            Settings.Default.ImagesPerso = mesImgs = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
            Settings.Default.MusiquePerso = maMus = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            Settings.Default.VideosPerso = mesVids = Environment.GetFolderPath(Environment.SpecialFolder.MyVideos);

            
            #region Concerne les répértoires spéciaux
            /*
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
            */
            #endregion
            
            //profile.Items.Add( dummyNode);
            Make_Folder("Bureau", bureau);
            //FileTree.Items.Add(itmBur);
            Make_Folder("Mes Documents", mesDocs);
            //FileTree.Items.Add(itmDocs);
            Make_Folder("Mes Images", mesImgs);
            //FileTree.Items.Add(itmImgs);
            Make_Folder("Ma Musique", maMus);
            //FileTree.Items.Add(itmMus);
            Make_Folder("Mes Vidéos", mesVids);
            //FileTree.Items.Add(itmVid);
            
            Populate_Drives();
        }

        private void Make_Folder(string headVal, string tagVal)
        {
            TreeViewItem itm = new TreeViewItem() { Header = headVal, Tag = tagVal };
            if (Has_Content(tagVal))
            {
                itm.Items.Add(dummyNode);
                itm.Expanded += Developpement_Dossier;                
            }
            itm.Selected += Select_Item;

            FileTree.Items.Add(itm);
        }


        // Concerne les supports physiques
        private void Populate_Drives()
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


        /// <summary>
        /// Fonctions à éxécuter quand on développe un dossier
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Developpement_Dossier(object sender, RoutedEventArgs e)
        {
            TreeViewItem item = (TreeViewItem)sender;
            FilePath.Text = item.Tag.ToString();
            
            string chemchem = item.Tag.ToString();
            Console.WriteLine($"Développement du dossier {item.Tag}");

            try
            {
                if ((item.Items.Count == 1) && (item.Items[0] == dummyNode))    // Obligatoire pour les nodes enfants
                {
                    item.Items.Clear(); //Supression du pseudo Item

                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {
                        Make_Folder(s.Substring(s.LastIndexOf("\\") + 1), s);
                        /*TreeViewItem tvItem = new TreeViewItem();
                        tvItem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        tvItem.Tag = s;

                        // Vérification si le dossier est vide
                        if (Has_Content(s))
                        {
                            tvItem.Items.Add(dummyNode);
                            tvItem.Expanded += new RoutedEventHandler(Developpement_Dossier);
                        }
                        tvItem.Selected += Select_Item;
                        item.Items.Add(tvItem);*/
                    }

                    // Récupération des fichiers
                    IEnumerable<string> fichiers;// = new List<string>();

                    if (Extensions.Count == 0)
                    {
                        fichiers = Directory.EnumerateFiles(chemchem);
                    }
                    else
                    {
                        fichiers = Directory.EnumerateFiles(chemchem).Where(x => Extensions.Contains(Path.GetExtension(x)));
                    }

                    foreach (string fichier in fichiers)
                    {
                        //Debug.WriteLine("File:" + fichier);
                        TreeViewItem tvItem = new TreeViewItem();

                        tvItem.Selected += new RoutedEventHandler( Select_Item);

                        //tvItem.MouseDown += Select_File;
                        tvItem.Header =  fichier.Substring(fichier.LastIndexOf('\\')+1);
                        tvItem.Tag = fichier;
                        item.Items.Add(tvItem);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }

        private void Select_Item(object sender, RoutedEventArgs e)
        {
            TreeViewItem tvi = (TreeViewItem)sender;
            Debug.WriteLine($"Selection du fichier: {tvi.Tag}");
            FilePath.Text =  tvi.Tag.ToString();
        }



        // Traitement du développement des dossiers
        private void oldDeveloppement_Dossier(object sender, RoutedEventArgs e)
        {
            //Todo attention il y a des erreurs dans les accès, documents and settings par ex
            TreeViewItem item = (TreeViewItem)sender;
            Console.WriteLine($"Développement du dossier {item.Tag}");
            try
            {

                if ((item.Items.Count == 1) && (item.Items[0] == dummyNode))    // Obligatoire pour les nodes enfants
                {
                    item.Items.Clear(); //Supression du pseudo Item

                    foreach (string s in Directory.GetDirectories(item.Tag.ToString()))
                    {

                        TreeViewItem tvItem = new TreeViewItem();
                        tvItem.Header = s.Substring(s.LastIndexOf("\\") + 1);
                        tvItem.Tag = s;
                        tvItem.Items.Add(dummyNode);
                        // tvItem.MouseLeftButtonDown += new MouseButtonEventHandler(Developpement_Dossier);
                        tvItem.Expanded += new RoutedEventHandler(Developpement_Dossier);
                        item.Items.Add(tvItem);
                    }

                    foreach (string s in Directory.GetFiles(item.Tag.ToString(), "*.*"))
                    {
                        Debug.WriteLine("File:" + s);
                        TreeViewItem tvItem = new TreeViewItem();
                        //tvItem.Header = s;
                        item.Items.Add(s);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }


        /// <summary>
        /// Fonction qui vérifie si l'on peut lister le contenu d'un dossier
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private bool Has_Content(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            Debug.WriteLine($"{folder} = {di.Attributes}");

            // Exclusion des dossiers système
            if (di.Attributes.HasFlag(FileAttributes.System))
            {
                Debug.WriteLine($"Exclusion de {folder}: dossier système");
                return false;
            }

            // Dossier vide
            if (Directory.EnumerateFileSystemEntries(folder).Count() == 0) return false;

            return true;

        }

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
        
        /// <summary>
        /// Action à éxécuter quand la texbox supérieure est modifiée, cherche dans les items une correspondance.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            // Sécurité pour éviter les erreurs
            if (FileTree == null) return;


            TextBox tx = (TextBox)sender;
    
            string[] lectArr = tx.Text.Split('\\');
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

        /// <summary>
        /// Recherche une séquence dans les items
        /// </summary>
        /// <param name="refArray">Tableau du chemin à chercher</param>
        /// <param name="nodeTV"></param>
        /// <param name="lvl">Profondeur de la recherche, commencer à 0</param>
        /// <returns></returns>
        private bool? MatchItem(string[] refArray, ItemCollection nodeTV, ushort lvl = 0)
        {
            bool result=false;
            string toSearch = refArray[lvl];
            Debug.WriteLine($"Recherche de '{toSearch}'");

            foreach (TreeViewItem itm in nodeTV)
            {

                string strCompare = itm.Header.ToString();
                if (lvl == 0) strCompare = itm.Tag.ToString().TrimEnd('\\');

                Debug.WriteLine("Test pour: "+ strCompare);

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
    }
}

