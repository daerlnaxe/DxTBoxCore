﻿using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DxTBoxCore.BoxChoose
{
    /*
     * Don't check folder system
     */
    public abstract class A_ModelChoose : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;


        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        #region Sans notif


        /// <summary>
        /// Block populate (used by Search)
        /// </summary>
        private bool _UnactivePopulate;

        /// <summary>
        /// Set path compareason for research
        /// </summary>
        public StringComparison PathCompareason { get; set; } = StringComparison.CurrentCultureIgnoreCase;

        /// <summary>
        /// Show files when developping folders 
        /// </summary>
        public bool ShowFiles { get; set; } = true;

        /// <summary>
        /// Show only files with this extension
        /// </summary>
        /// <remarks>
        /// No dot needed
        /// </remarks>
        public string[] FilesExtension { get; set; }



        /// <summary>
        /// Define the mode to block selection of folder/file or none
        /// </summary>
        public virtual ChooseMode Mode { get; set; }

        #endregion

        public List<I_ContChoose> Root { get; set; } = new List<I_ContChoose>();

        /// <summary>
        /// Show info on the top of the window
        /// </summary>
        /// <example>
        /// Select your database document
        /// </example>
        public string Info { get; set; }

        protected string _LinkResult;
        /// <summary>
        /// Résultat
        /// </summary>
        public virtual string LinkResult
        {
            get => _LinkResult;
            set
            {
                _LinkResult = value;
                OnPropertyChanged();
            }
        }


        private string _startingFolder;
        /// <summary>
        /// Start from this directory
        /// </summary>
        /// <remarks>
        /// Notification property is useless here
        /// </remarks>
        public string StartingFolder
        {
            get => _startingFolder;
            set
            {
                if (string.IsNullOrEmpty(value))
                {
                    _startingFolder = null;

                }
                else
                {

                    _startingFolder = value;
                    // if (!StartingFolder.EndsWith('\\'))
                    //   Recherche();
                }


                OnPropertyChanged();
            } //=> _startingFolder = string.IsNullOrEmpty(value) ? null : value;
        }


        /// <summary>
        /// Listof paths to hide
        /// </summary>
        protected List<string> PathsToAvoid { get; set; } = new List<string>();

        /// <summary>
        /// Prevent doublon
        /// </summary>
        /// <param name="path"></param>
        protected bool AddPathToAvoid(string path)
        {
            if (PathsToAvoid.Contains(path))
                return false;

            PathsToAvoid.Add(path);
            return true;
        }

        public A_ModelChoose()
        {
            Build_Root();


        }

        #region construction de la racine

        /// <summary>
        /// Construit la racine de l'arborescence
        /// </summary>
        protected virtual void Build_Root()
        {
            PlatformID OSversion = Environment.OSVersion.Platform;
            string test2 = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
            string test3 = Environment.GetFolderPath(Environment.SpecialFolder.Windows);

            // Desktop (normalement cross) 
            Root.Add(
                Build_SpElement(
                    type: E_IconFType.Desktop,
                    name: DxTBLang.Desktop,
                    path: Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                )
            );

            // Documents (normalement cross)
            Root.Add(
                Build_SpElement(
                    type: E_IconFType.SpecialDoc,
                    name: DxTBLang.Documents,
                    path: Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory)
                    )
                );

            // Images (cross)
            Root.Add(
                Build_SpElement(
                    type: E_IconFType.SpecialImage,
                    name: DxTBLang.Images,
                    path: Environment.GetFolderPath(Environment.SpecialFolder.MyPictures)
                    )
                );

            // Music (cross)
            Root.Add(
                Build_SpElement(
                    type: E_IconFType.SpecialMusic,
                    name: DxTBLang.Music,
                    path: Environment.GetFolderPath(Environment.SpecialFolder.MyMusic))
                    );

            // Pas traité objets 3D
            var vévé = Environment.SpecialFolder.Favorites;

            // Downloads (c++ dll)
            if (OSversion == PlatformID.Win32NT)
                Root.Add(
                    Build_SpElement(
                        type: E_IconFType.Downloads,
                        name: DxTBLang.Downloads,
                        path: SpecialFolders.GetPath(KnownFolder.Downloads))
                    );

            // Videos (Cross)
            Root.Add(
                Build_SpElement(
                     type: E_IconFType.SpecialVideo,
                     name: DxTBLang.Videos,
                     path: SpecialFolders.GetPath(KnownFolder.Videos))
                );

            // Disques Durs
            foreach (DriveInfo driv in DriveInfo.GetDrives())
            {
                switch (driv.DriveType)
                {
                    case DriveType.CDRom:
                        Root.Add(Build_CDrom(driv));
                        break;

                    case DriveType.Fixed:
                        Root.Add(Build_Drive(driv, E_IconFType.HardDriveF));
                        break;

                    case DriveType.Removable:
                        Root.Add(Build_Drive(driv, E_IconFType.HardDriveR));
                        break;

                    case DriveType.Network:
                        Root.Add(Build_Drive(driv, E_IconFType.Network));
                        break;

                    default:
                        Debug.WriteLine($"Not managed {driv.Name} | {driv.VolumeLabel}");
                        break;
                }
            }
        }

        /// <summary>
        /// Construit un élément
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        private FolderElem Build_SpElement(E_IconFType type, string name, string path)
        {
            /*
            // Dossier vide (détecte s'il y a un dossier vide)
            if (Directory.EnumerateFileSystemEntries(folder).Count() == 0)
                return false;*/

            return new FolderElem(type)
            {
                Name = name,
                Path = path,
                Children = Check_IfChildren(path) ?
                                new ObservableCollection<I_ContChoose>()
                                {
                                    new FolderElem(E_IconFType.Dummy)
                                }
                                : null,
                //FolderSystem = new DirectoryInfo(path).;
            };
        }

        /// <summary>
        /// Construit un élément type CDRom
        /// </summary>
        /// <param name="driv"></param>
        /// <returns></returns>
        private FolderElem Build_CDrom(DriveInfo driv)
        {
            string lDrive = driv.Name.TrimEnd('\\');

            return new FolderElem(E_IconFType.CDRom)
            {
                Name = driv.IsReady ? $"({lDrive}) {driv.VolumeLabel}" : $"({lDrive}) {DxTBLang.Optical_Drive}",
                Path = driv.Name,
                Children = driv.IsReady && Check_IfChildren(driv.Name) ?
                                 new ObservableCollection<I_ContChoose>()
                                 {
                                     new FolderElem(E_IconFType.Dummy)
                                 }
                                 : null,
            };
        }

        /// <summary>
        /// Construit un élément d'un modèle commun de lecteur
        /// </summary>
        /// <param name="driv"></param>
        /// <returns></returns>
        private FolderElem Build_Drive(DriveInfo driv, E_IconFType type)
        {

            string lDrive = driv.Name.TrimEnd('\\');

            if (driv.IsReady)
                return new FolderElem(type)
                {
                    Name = $"({lDrive}) {driv.VolumeLabel}",
                    Path = driv.Name,
                    Children = Check_IfChildren(driv.Name) ?
                                    new ObservableCollection<I_ContChoose>()
                                    {
                                    new FolderElem(E_IconFType.Dummy)
                                    }
                                    : null,
                };
            else
                return new FolderElem(type)
                {
                    Name = $"({lDrive})",
                    Path = driv.Name
                };
        }



        #endregion


        #region CheckIfChildren
        /// <summary>
        /// Check that a folder have children and permissions
        /// </summary>
        /// <param name="folder"></param>
        protected virtual bool Check_ChildrenNAccess(string folder)
        {

            try
            {
                // di 1 ms
                DirectoryInfo di = new DirectoryInfo(folder);
                DirectorySecurity test = FileSystemAclExtensions.GetAccessControl(di);

                Type oppop = test.AccessRightType;

                if (folder.Contains("Infused"))
                {

                }

                // Exclusion des dossiers système ? ça marche ?
                if (di.Attributes.HasFlag(FileAttributes.System))
                {
                    Debug.WriteLine($"Exclusion de {folder}: dossier système");
                    return false;
                }


                // Dossier vide (détecte s'il y a un dossier vide)
                if (Directory.EnumerateFileSystemEntries(folder).Count() == 0)
                    return false;
            }
            catch (UnauthorizedAccessException exc)
            {
                Debug.WriteLine($"Accès interdit: {folder}");
                return false;
            }

            return true;
        }

        /// <summary>
        /// Check if files or Subfolders
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        protected virtual bool Check_IfChildren(string folder)
        {
            // Dossier vide (détecte s'il y a un dossier vide)
            if (Directory.EnumerateFileSystemEntries(folder).Count() == 0)
                return false;

            return true;
        }

        #endregion


        #region Populate
        /// <summary>
        /// Liste le contenu d'un dossier
        /// </summary>
        /// <remarks>
        /// Le modèle de base ne liste que les sous-dossiers
        /// On a levé le clear car il y a apparemment des events non gérés qui peuvent survenir, 
        /// tant que le clear est actif ça change toute la population et annule l'expand à chaque fois que ces events arrivent
        /// </remarks>        
        public virtual void Populate_Folder(I_ContChoose parent)
        {
            if (_UnactivePopulate)
                return;

            Debug.WriteLine($"- Populate Folder: '{parent.Name}'");
            /*


            // 21 ms
            DirectorySecurity test = FileSystemAclExtensions.GetAccessControl
                                            (
                                                new DirectoryInfo(parent.Path)

            DirectoryInfo di = new DirectoryInfo(parent.Path);
            di.GetAccessControl();

            if (parent.Path == @"C:\Windows")
            {
                Debug.WriteLine($"{parent.Path} - {test.AreAccessRulesProtected}");
            }
            //
            if (test.AccessRuleType.Attributes.HasFlag(System.Reflection.TypeAttributes.Sealed))
            Debug.WriteLine($"Sealed folder {parent.Path} {}");


            else
            {
                Debug.WriteLine($"{parent.Path} - {test.AreAccessRulesProtected}");

            }
                                            );*/
            // Bloque le développement s'il n'y a pas d'enfants
            if (parent.Children == null)
                return;

            // On enlève le dummy file s'il est présent
            /*if (parent.Children[0].Type == E_IconFType.Dummy)
                parent.Children.RemoveAt(0);            */

            /*
            //17/03/2021
            string[] files;
            // Filtre les fichiers
            if (FilesExtension != null)
            {

                foreach (string ext)

            }
            else
            {
                files = Directory.EnumerateFiles(parent.Path, "*.*", SearchOption.TopDirectoryOnly);
            }*/
            //17/03/2021


            // On revérifie en cas de modifications
            //if (directories.Length == 0 && files.Length == 0)
            //parent.Children.Clear();

            Populate_ByFolders(parent);


            // Ajoute les fichiers si le switch est activé
            Debug.WriteLine($"-\t Populate with Files");
            if (ShowFiles)
                foreach (string f in Directory.EnumerateFiles(parent.Path, "*.*", SearchOption.TopDirectoryOnly))
                {
                    //17/03/2021 semble fonctionner
                    if (FilesExtension != null &&
                        !FilesExtension.Contains(Path.GetExtension(f).TrimStart('.').ToLowerInvariant()))
                        continue;

                    //17/03/2021
                    parent.Children.Add(Build_File(f));
                }

            Debug.WriteLine("- Fin populate");
        }

        /// <summary>
        /// Manages folders (add & remove)
        /// </summary>
        /// <param name="parent"></param>
        /// <remarks>
        /// No doublon
        /// </remarks>
        private void Populate_ByFolders(I_ContChoose parent)
        {
            string[] directories = Directory.GetDirectories(parent.Path, "*.*", SearchOption.TopDirectoryOnly);

            // On retire ce qui a disparu
            for (int i = 0; i < parent.Children.Count; i++)
            {
                var dir = parent.Children[i];
                if (directories.Contains(dir.Path))
                    continue;

                // On enlève
                parent.Children.RemoveAt(i);
                i--;
            }


            // Population 
            Debug.WriteLine($"-\t Populate with SubFolders");
            foreach (string d in directories)
            {
                // on vérifie juste que ça n'existe pas 
                //var ex = parent.Children.FirstOrDefault();

                // Introduction du système pour masker certains répertoires
                if (PathsToAvoid.FirstOrDefault(
                    (x) => x.Equals(d, StringComparison.OrdinalIgnoreCase)
                    ) != null)
                    continue;

                // Pas de doublon
                if (parent.Children.FirstOrDefault((x) => x.Path.Equals(d)) != null)
                    continue;



                // test 
                parent.Children.Add(
                    Build_SubFolder(
                        //type: E_IconFType.Folder,
                        //name: Path.GetFileName(d),
                        path: d)


                        /*
                        new ContFChoose(E_IconFType.Folder)
                        {
                            Name = Path.GetFileName(d),
                            Path = d,
                            Children = new List<ContFChoose>()
                            {
                                new ContFChoose( E_IconFType.Dummy )
                            }
                        }
                        */
                        );
            }
        }



        /// <summary>
        /// Construit un élément
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        /// <remarks>
        /// On teste aussi l'accès
        /// </remarks>
        protected virtual FolderElem Build_SubFolder(/*string name,*/ string path)
        {
            // Debug.WriteLine("Build_Subfolder");
            // bool accessGranted = Check_ChildrenNAccess(path);
            return new FolderElem(E_IconFType.Folder)
            {
                Name = Path.GetFileName(path),
                Path = path,
                Children = Check_ChildrenNAccess(path) ?
                                new ObservableCollection<I_ContChoose>()
                                {
                                    new FolderElem(E_IconFType.Dummy)
                                }
                                : null,
                //IsFocusable = Mode == ChooseMode.All | Mode == ChooseMode.Folder ? true : false,
                //     AccessGranted = accessGranted
            };

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        private FileElem Build_File(string file)
        {
            // bool accessGranted = Check_ChildrenNAccess(path);
            return new FileElem()
            {
                Name = Path.GetFileName(file),
                Path = file,
                IsFocusable = Mode == ChooseMode.All | Mode == ChooseMode.File ? true : false,

                //     AccessGranted = accessGranted
            };
        }
        #endregion


        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// La fin ne doit jamais avoir de '\' or les lecteurs finissent par '\'
        /// </remarks>
        internal void Recherche()
        {
            if (string.IsNullOrEmpty(StartingFolder))
                return;

            Debug.WriteLine($"Recherche de {StartingFolder}");
            // return;
            if (Match(StartingFolder, Root))
            {
                Debug.WriteLine("Success");

            }
            else
            {
                Debug.WriteLine("Fail");
            }
        }


        /*
            Example: C:\Win est contenu dans C:\Windows,
                    la seule manière est de comparer c:\Win\ avec c:\Windows\
            Le split est LA Seule manière, en effet en cas de start with, la fin peut ne pas correspondre que ça
            renvoie que c'est ok quand même. Si on rajoute un split 
         */
        internal virtual bool Match(string toSearch, IEnumerable<I_ContChoose> collec)
        {
            if (collec == null)
                return false;

            foreach (I_ContChoose data in collec)
            {
                if (data.Path == null)
                    continue;

                // Particular case for drives
                string toAnalyse = data.Path.TrimEnd('\\');
                  
                // Rempile ce qui est avant
                if (data.IsExpanded)
                    data.IsExpanded = false;

                // Each element have its path contained, test if data.path is contained into the string to search
                if (ComparePaths(toSearch, data.Path))
                {
                    // On peuple
                    // Pas la peine car c'est peuplé directement quand c'est expandé 
                    Populate_Folder(data);


                    // Bloque populate
                    //_UnactivePopulate = true;
                    data.IsSelected = true;
                    data.IsExpanded = true;

                    // Débloque populate
                    //_UnactivePopulate = false;

                    //bool isExp =;
                    // On cherche dans les enfants si ce n'est pas terminé
                    if (toSearch.Length > toAnalyse.Length)
                    {
                        // On rempile les enfants au cas où
                        /*foreach (var d in data.Children)
                         if (d.IsSelected)
                          d.IsSelected = false;*/

                        /*      foreach (var d in from d in data.Children where d.IsSelected select d)
                              {
                                  d.IsSelected = false;
                              }*/

                        return Match(StartingFolder, data.Children);
                    }

                    return true;
                }



            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="testedF">Always the biggest, search into</param>
        /// <param name="toSearch"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// </remarks>
        internal virtual bool ComparePaths(string toSearch, string testedF)
        {

            string[] partsToTest = testedF.TrimEnd('\\').Split('\\');
            string[] partsToSearch = toSearch.TrimEnd('\\').Split('\\');

            // Si la partie à tester est plus grande que la partie à chercher c'est faux
            if (partsToTest.Length > partsToSearch.Length)
                return false;

            /*for (int i = 0; i < refER.Length; i++)*/
            int i = 0;
            while (true)
            {
                // Si on a parcouru toute la partie à tester c'est que c'est ok
                if (i >= partsToTest.Length)
                    return true;

                // Si une partie est fausse, alors tout est faux
                if (!partsToTest[i].Equals(partsToSearch[i], PathCompareason))
                    return false;

                i++;
            };

            //return true;
        }
    }
}
