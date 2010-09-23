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

namespace WP7
{
    public partial class Game : PhoneApplicationPage
    {
        public Game()
        {
            InitializeComponent();
        }

        private void ComputerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void DoorButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void FilesButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }
		
		private void MapButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/City.xaml", UriKind.RelativeOrAbsolute));
        }

		private void NewspaperButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/Famous2.xaml", UriKind.RelativeOrAbsolute));
		}

		private void phoneButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/Famous3.xaml", UriKind.RelativeOrAbsolute));
		}
	}
}