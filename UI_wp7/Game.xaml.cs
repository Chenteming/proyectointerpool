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

namespace UIPrototype
{
    public partial class Game : PhoneApplicationPage
    {
        public Game()
        {
            InitializeComponent();
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Famous.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void DoorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void FilesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/City.xaml", UriKind.RelativeOrAbsolute));
        }
	}
}