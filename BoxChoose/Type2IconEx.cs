using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace DxTBoxCore.BoxChoose
{
    /// <summary>
    /// Used to get right icon link according type
    /// </summary>
    class Type2IconEx : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            E_IconFType type = E_IconFType.Parse<E_IconFType>(value.ToString());

            switch (type)
            {
                case E_IconFType.Folder:
                    return "../Resources/Folderx40.png";

                case E_IconFType.Desktop:
                    return "../Resources/Bureaux40.png";

                case E_IconFType.SpecialDoc:
                    return "../Resources/Documentsx40.png";

                case E_IconFType.SpecialImage:
                    return "../Resources/MesImagesx40.png";

                case E_IconFType.SpecialMusic:
                    return "../Resources/Mediax40.png";
                    
                case E_IconFType.SpecialVideo:
                    return "../Resources/MesVideosx40.png";

                case E_IconFType.CDRom:
                    return "../Resources/W10_Opticx40.png";

                case E_IconFType.HardDriveF:
                    return "../Resources/W10_HDDx40.png";

                case E_IconFType.HardDriveR:
                    return "../Resources/Drive USB.png";

                case E_IconFType.Network:
                    return "../Resources/W10_NetDrivex40.png";

                case E_IconFType.File:
                    return @"E:\Programmation\#Graphismes\blend-png\Blend PNG\512x512 PNG\Location Generic.png";

                default:
                    break;
            }


            //image.Source = Imaging.CreateBitmapSourceFromHIcon(ico.Handle, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
