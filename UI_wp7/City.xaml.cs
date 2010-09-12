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
using UI_wp7.ServiceReference;


namespace UIPrototype
{
    public partial class City : PhoneApplicationPage
    {
        private ServiceWP7Client client;

        public City()
        {
            InitializeComponent();
            client = new ServiceWP7Client();
            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCompleted);
            client.GetPossibleCitiesAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            Travel1.Visibility = System.Windows.Visibility.Collapsed;
            Travel2.Visibility = System.Windows.Visibility.Collapsed;
            Travel3.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Travel1_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(Travel1.Content.ToString());
        }

        private void Travel2_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel2.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(Travel2.Content.ToString());
        }

        private void Travel3_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel3.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(Travel3.Content.ToString());

        }

        // Asynchronous callbacks for displaying results.

        public void GetPossibleCitiesCompleted(object sender, GetPossibleCitiesCompletedEventArgs e)
        {
            List<String> cities = e.Result.ToList();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCities(cities);

            Travel1.Visibility = System.Windows.Visibility.Visible;
            Travel2.Visibility = System.Windows.Visibility.Visible;
            Travel3.Visibility = System.Windows.Visibility.Visible;

            Travel1.Content = cities.ElementAt(0);
            Travel2.Content = cities.ElementAt(1);
            Travel3.Content = cities.ElementAt(2);
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //TODO
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        void client_TravelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }
    }
}