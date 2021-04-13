using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public class M_SaveFile : A_ModelChoose
    {
        public override ChooseMode Mode => ChooseMode.File;

        /// <summary>
        /// Probablement différent du modèle originel
        /// </summary>
        public override string LinkResult
        {
            get => Path.Combine(FolderValue, FileValue);
            
            /*set
            {
                _LinkResult = value;
                FileValue = Path.GetFileName(value);
                OnPropertyChanged();
            }*/
        }

        private string _FileValue = Languages.DxTBLang.New_Doc;
        public string FileValue
        {
            get => _FileValue;
            set
            {
                _FileValue = value;
                OnPropertyChanged();
            }
        }

        internal string FolderValue { get; set; }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="type"></param>
        internal void AssignValues(string path, E_IconFType type)
        {
            if (Mode == ChooseMode.File)
            {
                Debug.WriteLine("All choisi");
                FileAttributes attr = File.GetAttributes(path);

                if((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    // En cas de dossier choisi
                    FolderValue = path;
                }
                else
                {
                    // En cas de fichier choisi
                    FolderValue = Path.GetDirectoryName(path);
                    FileValue = Path.GetFileName(path);                    
                }
            }

        }
    }
}
