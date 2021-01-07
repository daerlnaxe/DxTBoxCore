using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    /// <summary>
    /// Utilisé pour connaître le type d'un élément dans une box de choix de fichier ou de dossier
    /// </summary>
    public enum E_IconFType
    {
        Dummy,
        File,
        Folder,
        FolderDenied,
        Desktop,
        SpecialDoc,
        SpecialImage,
        SpecialMusic,
        Special3DObj,   // Not implemented
        Downloads,      // Not Implemented
        SpecialVideo,
        SpecialGame,
        CDRom,
        HardDriveF,     // Disque dur fixe
        HardDriveR,     // Disque dur mobile
        Network,


    }
}
