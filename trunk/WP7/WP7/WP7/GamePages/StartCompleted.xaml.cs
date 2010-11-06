﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;

namespace WP7.GamePages
{
    public partial class StartCompleted : PhoneApplicationPage
    {
        public StartCompleted()
        {
            InitializeComponent();
            InitializeComponent();
            //// this.GoButton.IsEnabled = false;
            Detective2Storyboard.Begin();
            Detective2Storyboard.Completed += new EventHandler(Detective2Storyboard_Completed);
        }

        void Detective2Storyboard_Completed(object sender, EventArgs e)
        {
            detectiveText.Visibility = Visibility.Visible;
            GoButton.Visibility = Visibility.Visible;
        }

        private void GoButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.Relative));
        }

    }
}