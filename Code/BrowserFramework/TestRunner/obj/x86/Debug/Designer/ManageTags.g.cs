﻿#pragma checksum "..\..\..\..\Designer\ManageTags.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "310DDCDFAE663881762CD6B8E911F5B8EC9677C470A8A94D3EB6994C5AC86FD4"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using TestRunner.Designer;


namespace TestRunner.Designer {
    
    
    /// <summary>
    /// ManageTags
    /// </summary>
    public partial class ManageTags : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 19 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOk;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
        #line default
        #line hidden
        
        
        #line 61 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddNewTag;
        
        #line default
        #line hidden
        
        
        #line 71 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbxAvailableTags;
        
        #line default
        #line hidden
        
        
        #line 96 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddTag;
        
        #line default
        #line hidden
        
        
        #line 100 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnRemoveTag;
        
        #line default
        #line hidden
        
        
        #line 124 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblCurrentTags;
        
        #line default
        #line hidden
        
        
        #line 128 "..\..\..\..\Designer\ManageTags.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox lbxCurrentTags;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/TestRunner;component/designer/managetags.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Designer\ManageTags.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 13 "..\..\..\..\Designer\ManageTags.xaml"
            ((System.Windows.Controls.MenuItem)(target)).Click += new System.Windows.RoutedEventHandler(this.mnuEdit_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnOk = ((System.Windows.Controls.Button)(target));
            
            #line 19 "..\..\..\..\Designer\ManageTags.xaml"
            this.btnOk.Click += new System.Windows.RoutedEventHandler(this.btnOk_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 20 "..\..\..\..\Designer\ManageTags.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnAddNewTag = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\..\..\Designer\ManageTags.xaml"
            this.btnAddNewTag.Click += new System.Windows.RoutedEventHandler(this.btnAddNewTag_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.lbxAvailableTags = ((System.Windows.Controls.ListBox)(target));
            
            #line 71 "..\..\..\..\Designer\ManageTags.xaml"
            this.lbxAvailableTags.GotFocus += new System.Windows.RoutedEventHandler(this.lbxAvailableTags_GotFocus);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnAddTag = ((System.Windows.Controls.Button)(target));
            
            #line 96 "..\..\..\..\Designer\ManageTags.xaml"
            this.btnAddTag.Click += new System.Windows.RoutedEventHandler(this.btnAddTag_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.btnRemoveTag = ((System.Windows.Controls.Button)(target));
            
            #line 100 "..\..\..\..\Designer\ManageTags.xaml"
            this.btnRemoveTag.Click += new System.Windows.RoutedEventHandler(this.btnRemoveTag_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.lblCurrentTags = ((System.Windows.Controls.Label)(target));
            return;
            case 9:
            this.lbxCurrentTags = ((System.Windows.Controls.ListBox)(target));
            
            #line 128 "..\..\..\..\Designer\ManageTags.xaml"
            this.lbxCurrentTags.GotFocus += new System.Windows.RoutedEventHandler(this.lbxCurrentTags_GotFocus);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
