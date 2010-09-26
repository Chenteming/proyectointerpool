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
using WP7.ServiceReference;


namespace WP7
{
    public partial class City : PhoneApplicationPage
    {
        private ServiceWP7Client client;
        private LanguageManager language;

        public City()
        {
            InitializeComponent();
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
            client = new ServiceWP7Client();
            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCompleted);
            client.GetPossibleCitiesAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            button1.Visibility = System.Windows.Visibility.Collapsed;
            button2.Visibility = System.Windows.Visibility.Collapsed;
            button3.Visibility = System.Windows.Visibility.Collapsed;
        }



        // Asynchronous callbacks for displaying results.

        public void GetPossibleCitiesCompleted(object sender, GetPossibleCitiesCompletedEventArgs e)
        {
            List<String> cities = e.Result.ToList();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCities(cities);

            button1.Visibility = System.Windows.Visibility.Visible;
            button2.Visibility = System.Windows.Visibility.Visible;
            button3.Visibility = System.Windows.Visibility.Visible;

            button1.Content = cities.ElementAt(0);
            button2.Content = cities.ElementAt(1);
            button3.Content = cities.ElementAt(2);
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        void client_TravelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(button1.Content.ToString());
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(button1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(button2.Content.ToString());
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(button2.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(button3.Content.ToString());
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(button3.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }
    }
}