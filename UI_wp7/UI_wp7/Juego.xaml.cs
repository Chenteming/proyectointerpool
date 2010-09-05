using System;
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

namespace UI_wp7
{
    public partial class Juego : PhoneApplicationPage
    {
        public Juego()
        {
            InitializeComponent();
        }

        private void Puerta_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Personaje.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void DoorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void FilesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Sospechoso.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Ciudad.xaml", UriKind.RelativeOrAbsolute));
        }
		/*
		private void NewsPaperButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Personaje.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void CelularButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Personaje.xaml", UriKind.RelativeOrAbsolute));
        }*/
	}
}