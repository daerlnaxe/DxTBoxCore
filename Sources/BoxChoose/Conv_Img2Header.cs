using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using DxTBoxCore.Properties;

/*
    Thanks to Toma4025 for icons

    Note:
        - Images parameters to resource generation, and mode 'not copy'
*/

namespace DxTBoxCore.BoxChoose
{
    public class Conv_Img2Header : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string tag = (string)value;
            //Debug.WriteLine($"Header: {tag}");

            // Cas du profil
            //if (tag == Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) return null;

            // Bureau
            if (tag.Equals(UserFolders.Bureau))
                return "../Resources/Bureaux40.png";
            if (tag.Equals(UserFolders.Documents))
                return "../Resources/Documentsx40.png";
            if (tag.Equals(UserFolders.Images))
                return "../Resources/MesImagesx40.png";
            if (tag.Equals(UserFolders.Musiques))
                return "../Resources/Mediax40.png";
            if (tag.Equals(UserFolders.Videos))
                return "../Resources/MesVideosx40.png";

            // Dossiers System
            

            // Cas des disques durs
            if (tag.EndsWith(":\\"))
            {
                //Debug.WriteLine("Cas d'un disque dur");
                foreach (DriveInfo driv in DriveInfo.GetDrives())
                {
                    if (driv.Name.Equals(tag))
                    {
                        switch (driv.DriveType)
                        {
                            case DriveType.Fixed:
                                return "../Resources/W10_HDDx40.png";
                            case DriveType.CDRom:
                                return "../Resources/W10_Opticx40.png";
                            case DriveType.Network:
                                return "../Resources/W10_NetDrivex40.png";
                            case DriveType.Removable:
                                return "../Resources/Drive USB.png";
                        }
                        break;
                    }
                }

            }

            // Cas des dossiers
            try
            {
                if (Directory.Exists(tag))
                {
                    DirectoryInfo di = new DirectoryInfo(tag);
                    //if (di.Attributes.HasFlag(FileAttributes.System)) return null;

                    //Debug.WriteLine("Cas d'un dossier");

                    return "../Resources/Folderx40.png";
                }

                if (File.Exists(tag))
                {
                    //Debug.WriteLine("Cas d'un fichier");
                    return "../Resources/document_blank.png";
                }
            }
            catch (Exception exc)
            {
                Debug.WriteLine(exc);
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
