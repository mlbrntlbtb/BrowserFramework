﻿#pragma checksum "..\..\..\ManageSuiteLinks.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "08B72BD815818DEC7F76FD872153DFC2A66DB149CEF8926198AC583598D276BD"
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


namespace TestRunner {
    
    
    /// <summary>
    /// ManageSuiteLinks
    /// </summary>
    public partial class ManageSuiteLinks : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 24 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnAddRow;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnEditRow;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveUp;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnMoveDown;
        
        #line default
        #line hidden
        
        
        #line 48 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnDeleteRow;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid dgLinks;
        
        #line default
        #line hidden
        
        
        #line 92 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnOK;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\..\ManageSuiteLinks.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnCancel;
        
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
            System.Uri resourceLocater = new System.Uri("/TestRunner;component/managesuitelinks.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ManageSuiteLinks.xaml"
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
            
            #line 4 "..\..\..\ManageSuiteLinks.xaml"
            ((TestRunner.ManageSuiteLinks)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.btnAddRow = ((System.Windows.Controls.Button)(target));
            
            #line 24 "..\..\..\ManageSuiteLinks.xaml"
            this.btnAddRow.Click += new System.Windows.RoutedEventHandler(this.btnAddRow_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.btnEditRow = ((System.Windows.Controls.Button)(target));
            
            #line 30 "..\..\..\ManageSuiteLinks.xaml"
            this.btnEditRow.Click += new System.Windows.RoutedEventHandler(this.btnEditRow_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.btnMoveUp = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\ManageSuiteLinks.xaml"
            this.btnMoveUp.Click += new System.Windows.RoutedEventHandler(this.btnMoveUp_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.btnMoveDown = ((System.Windows.Controls.Button)(target));
            
            #line 42 "..\..\..\ManageSuiteLinks.xaml"
            this.btnMoveDown.Click += new System.Windows.RoutedEventHandler(this.btnMoveDown_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.btnDeleteRow = ((System.Windows.Controls.Button)(target));
            
            #line 48 "..\..\..\ManageSuiteLinks.xaml"
            this.btnDeleteRow.Click += new System.Windows.RoutedEventHandler(this.btnDeleteRow_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.dgLinks = ((System.Windows.Controls.DataGrid)(target));
            
            #line 56 "..\..\..\ManageSuiteLinks.xaml"
            this.dgLinks.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.dgLinks_SelectionChanged);
            
            #line default
            #line hidden
            
            #line 57 "..\..\..\ManageSuiteLinks.xaml"
            this.dgLinks.Drop += new System.Windows.DragEventHandler(this.dgLinks_Drop);
            
            #line default
            #line hidden
            
            #line 57 "..\..\..\ManageSuiteLinks.xaml"
            this.dgLinks.DragEnter += new System.Windows.DragEventHandler(this.dgLinks_DragEnter);
            
            #line default
            #line hidden
            return;
            case 8:
            this.btnOK = ((System.Windows.Controls.Button)(target));
            
            #line 92 "..\..\..\ManageSuiteLinks.xaml"
            this.btnOK.Click += new System.Windows.RoutedEventHandler(this.btnOK_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            this.btnCancel = ((System.Windows.Controls.Button)(target));
            
            #line 93 "..\..\..\ManageSuiteLinks.xaml"
            this.btnCancel.Click += new System.Windows.RoutedEventHandler(this.btnCancel_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

