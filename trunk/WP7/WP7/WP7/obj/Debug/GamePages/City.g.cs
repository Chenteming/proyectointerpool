﻿#pragma checksum "C:\Users\Juan Pablo\Desktop\PIS\trunk\WP7\WP7\WP7\GamePages\City.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "F954128EF458C03D183668683C72CBA1"
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
    
    
    public partial class City : Microsoft.Phone.Controls.PhoneApplicationPage {
        
        internal System.Windows.Media.Animation.Storyboard animacion2;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames scaleX;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames translateX;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trX1;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trX2;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trX3;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trX4;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames scaleY;
        
        internal System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames translateY;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trY1;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trY2;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trY3;
        
        internal System.Windows.Media.Animation.EasingDoubleKeyFrame trY4;
        
        internal System.Windows.Media.Animation.Storyboard AnimationPage;
        
        internal System.Windows.Controls.Grid LayoutRoot;
        
        internal System.Windows.Controls.StackPanel TitlePanel;
        
        internal System.Windows.Controls.Grid ContentGrid;
        
        internal System.Windows.Controls.Canvas canva1;
        
        internal System.Windows.Controls.Image avion;
        
        internal System.Windows.Controls.Image Whitespace;
        
        internal System.Windows.Controls.Image Background1;
        
        internal System.Windows.Controls.Button Back;
        
        internal System.Windows.Controls.TextBox textToTravel;
        
        internal System.Windows.Controls.Button button3;
        
        internal System.Windows.Controls.Button button2;
        
        internal System.Windows.Controls.Button button1;
        
        internal System.Windows.Controls.Image plane;
        
        internal System.Windows.Controls.Image InitPoint;
        
        internal System.Windows.Controls.Image YellowPoint;
        
        internal System.Windows.Controls.Image BluePoint;
        
        internal System.Windows.Controls.Image GreenPoint;
        
        internal System.Windows.Controls.MediaElement plane_sound;
        
        internal System.Windows.Controls.Image MessageImage;
        
        internal System.Windows.Controls.Button YesButton;
        
        internal System.Windows.Controls.Button NoButton;
        
        internal System.Windows.Controls.TextBlock messageText;
        
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
            System.Windows.Application.LoadComponent(this, new System.Uri("/WP7;component/GamePages/City.xaml", System.UriKind.Relative));
            this.animacion2 = ((System.Windows.Media.Animation.Storyboard)(this.FindName("animacion2")));
            this.scaleX = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("scaleX")));
            this.translateX = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("translateX")));
            this.trX1 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trX1")));
            this.trX2 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trX2")));
            this.trX3 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trX3")));
            this.trX4 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trX4")));
            this.scaleY = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("scaleY")));
            this.translateY = ((System.Windows.Media.Animation.DoubleAnimationUsingKeyFrames)(this.FindName("translateY")));
            this.trY1 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trY1")));
            this.trY2 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trY2")));
            this.trY3 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trY3")));
            this.trY4 = ((System.Windows.Media.Animation.EasingDoubleKeyFrame)(this.FindName("trY4")));
            this.AnimationPage = ((System.Windows.Media.Animation.Storyboard)(this.FindName("AnimationPage")));
            this.LayoutRoot = ((System.Windows.Controls.Grid)(this.FindName("LayoutRoot")));
            this.TitlePanel = ((System.Windows.Controls.StackPanel)(this.FindName("TitlePanel")));
            this.ContentGrid = ((System.Windows.Controls.Grid)(this.FindName("ContentGrid")));
            this.canva1 = ((System.Windows.Controls.Canvas)(this.FindName("canva1")));
            this.avion = ((System.Windows.Controls.Image)(this.FindName("avion")));
            this.Whitespace = ((System.Windows.Controls.Image)(this.FindName("Whitespace")));
            this.Background1 = ((System.Windows.Controls.Image)(this.FindName("Background1")));
            this.Back = ((System.Windows.Controls.Button)(this.FindName("Back")));
            this.textToTravel = ((System.Windows.Controls.TextBox)(this.FindName("textToTravel")));
            this.button3 = ((System.Windows.Controls.Button)(this.FindName("button3")));
            this.button2 = ((System.Windows.Controls.Button)(this.FindName("button2")));
            this.button1 = ((System.Windows.Controls.Button)(this.FindName("button1")));
            this.plane = ((System.Windows.Controls.Image)(this.FindName("plane")));
            this.InitPoint = ((System.Windows.Controls.Image)(this.FindName("InitPoint")));
            this.YellowPoint = ((System.Windows.Controls.Image)(this.FindName("YellowPoint")));
            this.BluePoint = ((System.Windows.Controls.Image)(this.FindName("BluePoint")));
            this.GreenPoint = ((System.Windows.Controls.Image)(this.FindName("GreenPoint")));
            this.plane_sound = ((System.Windows.Controls.MediaElement)(this.FindName("plane_sound")));
            this.MessageImage = ((System.Windows.Controls.Image)(this.FindName("MessageImage")));
            this.YesButton = ((System.Windows.Controls.Button)(this.FindName("YesButton")));
            this.NoButton = ((System.Windows.Controls.Button)(this.FindName("NoButton")));
            this.messageText = ((System.Windows.Controls.TextBlock)(this.FindName("messageText")));
        }
    }
}

