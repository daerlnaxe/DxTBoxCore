﻿#pragma checksum "..\..\..\..\Box Collec\DxMCollec.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "119764F4398ABBBE03B701450663385506711F7F"
//------------------------------------------------------------------------------
// <auto-generated>
//     Ce code a été généré par un outil.
//     Version du runtime :4.0.30319.42000
//
//     Les modifications apportées à ce fichier peuvent provoquer un comportement incorrect et seront perdues si
//     le code est régénéré.
// </auto-generated>
//------------------------------------------------------------------------------

using DxTBoxCore.Collec;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace DxTBoxCore.Collec {
    
    
    /// <summary>
    /// DxMCollec
    /// </summary>
    public partial class DxMCollec : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\Box Collec\DxMCollec.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btValid;
        
        #line default
        #line hidden
        
        
        #line 13 "..\..\..\..\Box Collec\DxMCollec.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btAnnul;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\..\Box Collec\DxMCollec.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessagePart;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Box Collec\DxMCollec.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lboxElements;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\..\Box Collec\DxMCollec.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.MenuItem miRemove;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/DxTBoxCore;V2.0.0.1;component/box%20collec/dxmcollec.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Box Collec\DxMCollec.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.1.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.btValid = ((System.Windows.Controls.Button)(target));
            
            #line 12 "..\..\..\..\Box Collec\DxMCollec.xaml"
            this.btValid.Click += new System.Windows.RoutedEventHandler(this.btValid_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btAnnul = ((System.Windows.Controls.Button)(target));
            
            #line 13 "..\..\..\..\Box Collec\DxMCollec.xaml"
            this.btAnnul.Click += new System.Windows.RoutedEventHandler(this.btAnnul_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.MessagePart = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 4:
            this.lboxElements = ((System.Windows.Controls.ListBox)(target));
            return;
            case 5:
            this.miRemove = ((System.Windows.Controls.MenuItem)(target));
            
            #line 19 "..\..\..\..\Box Collec\DxMCollec.xaml"
            this.miRemove.Click += new System.Windows.RoutedEventHandler(this.miRemove_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

