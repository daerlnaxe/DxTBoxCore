using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public sealed class M_ChooseFolder : A_ModelChoose
    {
        /// <summary>
        /// Mask Windows folder
        /// </summary>
        public bool HideWindowsFolder
        {
            get => PathsToAvoid.Contains(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
            set
            {
                if (value == true)
                    base.AddPathToAvoid(Environment.GetFolderPath(Environment.SpecialFolder.Windows));
               /* else 
                    base.RemovePathToAvoid(Environment.GetFolderPath(Environment.SpecialFolder.Windows));*/
                    
            }
        }

        public override ChooseMode Mode => ChooseMode.Folder;

        public M_ChooseFolder() : base()
        {
            base.ShowFiles = true;
            HideWindowsFolder = true;
        }

        /*
        public override void Populate_Folder(ContFChoose parent)
        {
            // Bloque le développement s'il n'y a pas d'enfants
            if (parent.Children == null)
                return;

            // On enlève le dummy file s'il est présent
            /* if (parent.Children[0].Type == E_IconFType.Dummy)
                 parent.Children.RemoveAt(0);*/
        /*
            parent.Children.Clear();




               // string test = Environment.GetFolderPath(Environment.SpecialFolder.Windows);
            foreach (string d in Directory.GetDirectories(parent.Path, "*.*", SearchOption.TopDirectoryOnly))
            {
                if (d.Equals(Environment.GetFolderPath( Environment.SpecialFolder.Windows), StringComparison.OrdinalIgnoreCase))
                    continue;

                parent.Children.Add(
                    base.Build_SubFolder(
                        //type: E_IconFType.Folder,
                        //name: Path.GetFileName(d),
                        path: d)

                        );
            }
        }*/
    }

}
