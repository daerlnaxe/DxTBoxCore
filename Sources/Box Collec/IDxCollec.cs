using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Collec
{
    interface IDxCollec
    {
        ObservableCollection<dynamic> Elements { get; set; }

        void SetCollection<T>(List<T> elements);
        void SetCollection<T>(T[] elements);

        /// <summary>
        /// Element of the collection to show
        /// </summary>
        /// <param name="property"></param>
        void SetDisplay(string property); 
    }
}
