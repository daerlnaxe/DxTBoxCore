using System;
using System.Collections.Generic;
using System.Text;
using System;
using System.Runtime.InteropServices;

namespace DxTBoxCore.BoxChoose
{

    /// <summary>
    /// Class containing methods to retrieve specific file system paths.
    /// </summary>
    public static class SpecialFolders
    {
        /// <summary>
        /// Id known for special paths
        /// </summary>
        private static Dictionary<KnownFolder, string> _knownFolderGuids = new Dictionary<KnownFolder, string>()
        {
            {KnownFolder.Contacts,"{56784854-C6CB-462B-8169-88E350ACB882}"},
            {KnownFolder.Desktop, "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}"},
            {KnownFolder.Documents, "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}"},
            {KnownFolder.Downloads, "{374DE290-123F-4565-9164-39C4925E467B}"},
            {KnownFolder.Favorites, "{1777F761-68AD-4D8A-87BD-30B759FA33DD}" },
            {KnownFolder.Links, "{BFB9D5E0-C6A9-404C-B2B2-AE6DB6AF4968}"},
            {KnownFolder.Music, "{4BD8D571-6D19-48D3-BE97-422220080E43}"},
            {KnownFolder.Pictures, "{33E28130-4E1E-4676-835A-98395C3BC3BB}"},
            {KnownFolder.SavedGames, "{4C5C32FF-BB9D-43B0-B5B4-2D72E54EAAA4}"},
            {KnownFolder.SavedSearches, "{7D1D3A04-DEBB-4115-95CF-2F29DA2920DA}"},
            {KnownFolder.Videos, "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}"}
        };

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rfid"></param>
        /// <param name="dwFlags"></param>
        /// <param name="hToken"></param>
        /// <param name="ppszPath"></param>
        /// <returns></returns>
        [DllImport("Shell32.dll")]
        private static extern int SHGetKnownFolderPath(
            [MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken,
            out IntPtr ppszPath);

        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolderGUID">GUID Known of the special path</param>
        /// <param name="defaultUser">Specifies if the paths of the default user (user profile
        ///     template) will be used. This requires administrative rights.</param>
        /// <returns>The default path of the known folder.</returns>
        /// <exception cref="System.Runtime.InteropServices.ExternalException">Thrown if the path
        ///     could not be retrieved.</exception>
        public static string GetPath(KnownFolder knownFolder, bool defaultUser = false)
        {
            return GetPath(knownFolder, KnownFolderFlags.DontVerify, defaultUser);
        }


        /// <summary>
        /// Gets the current path to the specified known folder as currently configured. This does
        /// not require the folder to be existent.
        /// </summary>
        /// <param name="knownFolder">Enum of the knownFolder</param>
        /// <param name="flags"></param>
        /// <param name="defaultUser">
        /// Specifies if the paths of the default user (user profile template) will be used.
        /// This requires administrative rights.
        /// </param>
        /// <returns></returns>
        private static string GetPath(KnownFolder knownFolder,
                                      KnownFolderFlags flags = KnownFolderFlags.DontVerify,
                                      bool defaultUser = false)
        {
            // Création du GUID
            var guidKnown = new Guid(_knownFolderGuids[knownFolder]);

            // Résultat
            int result = SHGetKnownFolderPath(guidKnown, (uint)flags,
                                                new IntPtr(defaultUser ? -1 : 0), out var outPath);

            if (result >= 0)
            {
                string path = Marshal.PtrToStringUni(outPath);
                Marshal.FreeCoTaskMem(outPath);
                return path;
            }

            throw new ExternalException("Unable to retrieve the known folder path. It may not "
                                            + "be available on this system.", result);
        }

        [Flags]
        private enum KnownFolderFlags : uint
        {
            SimpleIDList = 0x00000100,
            NotParentRelative = 0x00000200,
            DefaultPath = 0x00000400,
            Init = 0x00000800,
            NoAlias = 0x00001000,
            DontUnexpand = 0x00002000,
            DontVerify = 0x00004000,
            Create = 0x00008000,
            NoAppcontainerRedirection = 0x00010000,
            AliasOnly = 0x80000000
        }
    }


    /// <summary>
    /// Standard folders registered with the system. These folders are installed with Windows Vista
    /// and later operating systems, and a computer will have only folders appropriate to it
    /// installed.
    /// </summary>
    public enum KnownFolder
    {
        Contacts,
        Desktop,
        Documents,
        Downloads,
        Favorites,
        Links,
        Music,
        Pictures,
        SavedGames,
        SavedSearches,
        Videos
    }

}
