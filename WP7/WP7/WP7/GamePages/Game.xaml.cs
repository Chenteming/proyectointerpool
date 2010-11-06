namespace WP7
{
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
	using Microsoft.Phone.Shell;
    using WP7.ServiceReference;

    public partial class Game : PhoneApplicationPage
    {
        private GameManager gm = GameManager.getInstance();

        public Game()
        {
            InitializeComponent();
            if (gm.ShowAnimation == true)
                ToastyStoryboard.Begin();
            gm.ShowAnimation = false;
        }

		private void Door_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Earth_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/City.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Files_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!gm.EmitOrder)
			    NavigationService.Navigate(new Uri("/GamePages/Filter.xaml", UriKind.RelativeOrAbsolute));
            else
                NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Laptop_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(2);
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Newspaper_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(1);
			NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Phone_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(0);
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void help_Click(object sender, System.EventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/Help.xaml", UriKind.RelativeOrAbsolute));
		}

		private void options_Click(object sender, System.EventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/Options.xaml", UriKind.RelativeOrAbsolute));
		}

	}
}