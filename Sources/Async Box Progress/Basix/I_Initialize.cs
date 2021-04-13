using DxLocalTransf.Progress;
using System;
using System.Collections.Generic;
using System.Text;

namespace DxTBoxCore.Box_Progress.Basix
{
    public interface I_Initialize
    {
        I_AsyncSigD Objet { get;}

        void Initialize<T>(T objet, Func<object> method) where T : I_AsyncSigD;

    }
}
