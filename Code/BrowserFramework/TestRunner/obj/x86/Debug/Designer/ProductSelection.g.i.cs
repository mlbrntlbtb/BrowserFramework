﻿#pragma checksum "..\..\..\..\Designer\ProductSelection.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "D890E19116D5FA2844E8DE358985383F1CC6755844AD72B0229C56800ED5E011"
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


namespace TestRunner.Designer {
    
    
    /// <summary>
    /// ProductSelection
    /// </summary>
    public partial class ProductSelection : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 13 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel panelmMainRightTop;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblTestRunner;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSelect;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cboTargetApplication;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel panelmMainRightBottom;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label lblSelectDirectory;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox txtSuiteDirectory;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnBrowse;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnContinue;
        
        #line default
        #line hidden
        
        
        #line 36 "..\..\..\..\Designer\ProductSelection.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button btnQuit;
        
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
            System.Uri resourceLocater = new System.Uri("/TestRunner;component/designer/productselection.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\Designer\ProductSelection.xaml"
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
            
            #line 5 "..\..\..\..\Designer\ProductSelection.xaml"
            ((TestRunner.Designer.ProductSelection)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.panelmMainRightTop = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.lblTestRunner = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.lblSelect = ((System.Windows.Controls.Label)(target));
            return;
            case 5:
            this.cboTargetApplication = ((System.Windows.Controls.ComboBox)(target));
            
            #line 21 "..\..\..\..\Designer\ProductSelection.xaml"
            this.cboTargetApplication.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.cboTargetApplication_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.panelmMainRightBottom = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.lblSelectDirectory = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.txtSuiteDirectory = ((System.Windows.Controls.TextBox)(target));
            return;
            case 9:
            this.btnBrowse = ((System.Windows.Controls.Button)(target));
            
            #line 31 "..\..\..\..\Designer\ProductSelection.xaml"
            this.btnBrowse.Click += new System.Windows.RoutedEventHandler(this.btnbrowse_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.btnContinue = ((System.Windows.Controls.Button)(target));
            
            #line 35 "..\..\..\..\Designer\ProductSelection.xaml"
            this.btnContinue.Click += new System.Windows.RoutedEventHandler(this.btnContinue_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.btnQuit = ((System.Windows.Controls.Button)(target));
            
            #line 36 "..\..\..\..\Designer\ProductSelection.xaml"
            this.btnQuit.Click += new System.Windows.RoutedEventHandler(this.btnQuit_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
