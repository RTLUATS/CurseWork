﻿#pragma checksum "..\..\ChefViewFood.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "223B05D7113B8BDA7A8962A26F69DDF74F41A408E417E9EBB56C12273889B2E9"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using CurseWork;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace CurseWork {
    
    
    /// <summary>
    /// ChefViewFood
    /// </summary>
    public partial class ChefViewFood : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 21 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Image;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Table;
        
        #line default
        #line hidden
        
        
        #line 37 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Name;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Category;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ChangeImage;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button AddFood;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\ChefViewFood.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveChange;
        
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
            System.Uri resourceLocater = new System.Uri("/CurseWork;component/chefviewfood.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\ChefViewFood.xaml"
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
            this.Image = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.Table = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 3:
            this.Name = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.Category = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.ChangeImage = ((System.Windows.Controls.Button)(target));
            
            #line 39 "..\..\ChefViewFood.xaml"
            this.ChangeImage.Click += new System.Windows.RoutedEventHandler(this.ChangeImage_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.AddFood = ((System.Windows.Controls.Button)(target));
            
            #line 40 "..\..\ChefViewFood.xaml"
            this.AddFood.Click += new System.Windows.RoutedEventHandler(this.AddFood_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.SaveChange = ((System.Windows.Controls.Button)(target));
            
            #line 41 "..\..\ChefViewFood.xaml"
            this.SaveChange.Click += new System.Windows.RoutedEventHandler(this.SaveChange_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

