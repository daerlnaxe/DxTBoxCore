using DxTBoxCore.Languages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.AccessControl;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    /*
     * Don't check folder system
     */
    public class A_ModelChoose
    {
        public List<ContFChoose> Root { get; set; } = new List<ContFChoose>();



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
            set => _startingFolder = string.IsNullOrEmpty(value) ? null : value;
            //  OnPropertyChanged();
        }

        public A_ModelChoose()
        {
            Build_Root();

        }

        protected virtual void Build_Root()
        {
            string p;

            PlatformID OSversion = Environment.OSVersion.Platform;
            Environment.SpecialFolder test2 = Environment.SpecialFolder.Recent;
            Environment.SpecialFolder test3 = Environment.SpecialFolder.Windows;

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
        private ContFChoose Build_SpElement(E_IconFType type, string name, string path)
        {
            /*
            // Dossier vide (détecte s'il y a un dossier vide)
            if (Directory.EnumerateFileSystemEntries(folder).Count() == 0)
                return false;*/

            return new ContFChoose(type)
            {
                Name = name,
                Path = path,
                Children = Check_IfChildren(path) ?
                                new ObservableCollection<ContFChoose>()
                                {
                                    new ContFChoose(E_IconFType.Dummy) 
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
        private ContFChoose Build_CDrom(DriveInfo driv)
        {
            string lDrive = driv.Name.TrimEnd('\\');

            return new ContFChoose(E_IconFType.CDRom)
            {
                Name = driv.IsReady ? $"({lDrive}) {driv.VolumeLabel}" : $"({lDrive}) {DxTBLang.Optical_Drive}",
                Path = driv.Name,
                Children = driv.IsReady && Check_IfChildren(driv.Name) ?
                                 new ObservableCollection<ContFChoose>()
                                 {
                                     new ContFChoose(E_IconFType.Dummy)
                                 }
                                 : null,
            };
        }

        /// <summary>
        /// Construit un élément d'un modèle commun de lecteur
        /// </summary>
        /// <param name="driv"></param>
        /// <returns></returns>
        private ContFChoose Build_Drive(DriveInfo driv, E_IconFType type)
        {
            string lDrive = driv.Name.TrimEnd('\\');

            return new ContFChoose(type)
            {
                Name = $"({lDrive}) {driv.VolumeLabel}",
                Path = driv.Name,
                Children = Check_IfChildren(driv.Name) ?
                                new ObservableCollection<ContFChoose>()
                                {
                                    new ContFChoose(E_IconFType.Dummy)
                                }
                                : null,
            };
        }

        /// <summary>
        /// Construit un élément
        /// </summary>
        /// <param name="p"></param>
        /// <param name="type"></param>
        /// <remarks>
        /// On teste aussi l'accès
        /// </remarks>
        protected virtual ContFChoose Build_Element(E_IconFType type, string name, string path)
        {
            // bool accessGranted = Check_ChildrenNAccess(path);
            return new ContFChoose(type)
            {
                Name = name,
                Path = path,
                Children = Check_ChildrenNAccess(path) ?
                                new ObservableCollection<ContFChoose>() 
                                {
                                    new ContFChoose(E_IconFType.Dummy) 
                                }
                                : null,
                //     AccessGranted = accessGranted
            };

        }


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

                Debug.WriteLine($" - {folder}");
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
        /// <summary>
        /// Liste le contenu d'un dossier
        /// </summary>
        /// <remarks>
        /// Le modèle de base ne liste que les sous-dossiers
        /// </remarks>        
        public virtual void Populate_Folder(ContFChoose parent)
        {
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
            if (parent.Children[0].Type == E_IconFType.Dummy)
                parent.Children.RemoveAt(0);


            foreach (string d in Directory.GetDirectories(parent.Path, "*.*", SearchOption.TopDirectoryOnly))
            {
                // test 

                parent.Children.Add(
                    Build_Element(
                        type: E_IconFType.Folder,
                        name: Path.GetFileName(d),
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
    }
}
