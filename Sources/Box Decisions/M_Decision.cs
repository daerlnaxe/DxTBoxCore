using DxLocalTransf;
using DxTBoxCore.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace DxTBoxCore.Box_Decisions
{
    public class M_Decision//: INotifyPropertyChanged
    {

        public string Message { get; set; }


        /// <summary>
        /// Fichier source
        /// </summary>
        public string Source { get; set; }

        /// <summary>
        /// Fichier de destination
        /// </summary>
        public string Destination { get; set; }
        
        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// size etc...
        /// </remarks>
        public string SourceInfo { get; set; }
        public string DestInfo { get; set; }


        public E_Decision Decision { get; set; }

        public bool All { get; set; }


    }
}
