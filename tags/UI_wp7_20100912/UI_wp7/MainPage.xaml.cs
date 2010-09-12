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
    public partial class MainPage : PhoneApplicationPage
    {
        private ServiceWP7Client client;

        public MainPage()
        {
            InitializeComponent();
            client = new ServiceWP7Client();
            client.StartGameCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_StartGameCompleted);
            client.StartGameAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            GameManager gm = GameManager.getInstance();
            client = new ServiceWP7Client();
            client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            client.GetCurrentCityAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            NavigationService.Navigate(new Uri("/Game.xaml", UriKind.RelativeOrAbsolute));
        }

		// Asynchronous callbacks for displaying results.
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        void client_StartGameCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }
        
        static void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            String initialCity = e.Result;
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(initialCity);
        }
    }
}
