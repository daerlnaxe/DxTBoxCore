using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DxTBoxCore.Cont
{
    public class DxImage
    {
        public string Title { get; set; }
        public string FullPath { get; set; }

        public DxImage() { }

        public DxImage(string title, string fpath) { Title = title; FullPath = fpath; }
    }
}
