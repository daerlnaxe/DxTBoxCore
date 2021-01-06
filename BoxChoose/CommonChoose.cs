using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.BoxChoose
{
    static class CommonChoose
    {
        /// <summary>
        /// Fonction qui vérifie si l'on peut lister le contenu d'un dossier
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        static internal bool Has_Content(string folder)
        {
            DirectoryInfo di = new DirectoryInfo(folder);
            //Debug.WriteLine($"{folder} = {di.Attributes}");

            // Exclusion des dossiers système
            if (di.Attributes.HasFlag(FileAttributes.System))
            {
                Debug.WriteLine($"Exclusion de {folder}: dossier système");
                return false;
            }

            // Dossier vide
            if (Directory.EnumerateFileSystemEntries(folder).Count() == 0)
                return false;

            return true;

        }




    }
}
