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
        private InterpoolWP7Client client;
        private LanguageManager language;
        private GameManager gm = GameManager.getInstance();

        public City()
        {
			// AnimationPage.Begin();
            InitializeComponent();
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
            client = new InterpoolWP7Client();
            client.GetCitiesCompleted += new EventHandler<GetCitiesCompletedEventArgs>(client_GetCitiesCompleted);
            client.GetCitiesAsync(gm.userId);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            button1.Visibility = System.Windows.Visibility.Collapsed;
            button2.Visibility = System.Windows.Visibility.Collapsed;
            button3.Visibility = System.Windows.Visibility.Collapsed;
        }

        void client_GetCitiesCompleted(object sender, GetCitiesCompletedEventArgs e)
        {
            List<DataCity> dataCities = e.Result.ToList();
            List<string> cities = new List<string>(); 
            foreach (DataCity dataCity in dataCities)
            {
                cities.Add(dataCity.name_city);
            }
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
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted += new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button1.Content.ToString());
            gm.SetCurrentCity(button1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        void client_TravelCompleted(object sender, TravelCompletedEventArgs e)
        {
            
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted +=new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button2.Content.ToString());
            gm.SetCurrentCity(button2.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted +=new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button3.Content.ToString());
            gm.SetCurrentCity(button3.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }
    }
}