using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.BoxChoose
{
    public class M_SaveFile : A_ModelChoose
    {
        public new ChooseMode Mode => ChooseMode.Folder;

        public string FileValue 
        {
            get;
            set;
        } = Languages.DxTBLang.New_Doc;
    }
}
