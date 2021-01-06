using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public class ContFChoose
    {
        public E_IconFType Type { get; }

        public string Name { get; set; }
        public string Path { get; set; }

     //   public bool AccessGranted { get; set; } = true;

        public ObservableCollection<ContFChoose> Children { get; set; }

        public ContFChoose(E_IconFType type)
        {
            Type = type;
           // Children = new List<ContFChoose>();
        }
    }
}
