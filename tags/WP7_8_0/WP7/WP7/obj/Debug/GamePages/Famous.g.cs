﻿#pragma checksum "C:\Users\Vicente\Documents\Facultad\4to\Proyecto Ingeniería de Software\Repositorio\trunk\WP7\WP7\WP7\GamePages\Famous.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "A665A2C9C1108F2E5BD57C48C5C5FD96"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Automation.Peers;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Resources;
using System.Windows.Shapes;
using System.Windows.Threading;


namespace WP7 {
    
    
    public partial class Famous : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentGrid;
        
        internal System.Windows.Controls.TextBox textToQuestion;
        
        internal System.Windows.Controls.TextBlock dialogText;
        
        internal System.Windows.Controls.Image famousImage;
        
        internal System.Windows.Controls.Button famousNameButton;
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP7;component/GamePages/Famous.xaml", System.UriKind.Relative));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentGrid = ((System.Windows.Controls.Grid)(this.FindName("ContentGrid")));
            this.textToQuestion = ((System.Windows.Controls.TextBox)(this.FindName("textToQuestion")));
            this.dialogText = ((System.Windows.Controls.TextBlock)(this.FindName("dialogText")));
            this.famousImage = ((System.Windows.Controls.Image)(this.FindName("famousImage")));
            this.famousNameButton = ((System.Windows.Controls.Button)(this.FindName("famousNameButton")));
        }
    }
}

