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


namespace UI_wp7
{
    public partial class Ciudad : PhoneApplicationPage
    {
        private ServiceWP7Client client; 

        public Ciudad()
        {
            InitializeComponent();
            GameManager gm = GameManager.getInstance();            
            client = new ServiceWP7Client();

            client.GetPossibleCitiesCompleted+=new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCompleted);
            client.GetPossibleCitiesAsync();
            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            Travel1.Visibility = System.Windows.Visibility.Collapsed;
            Travel2.Visibility = System.Windows.Visibility.Collapsed;
            Travel3.Visibility = System.Windows.Visibility.Collapsed;
        }
		
		private void SetAndStartButon(object sender, EventArgs e)
        {
            //Start the storyboard
            Storyboard1.Begin();
        }

        private void Travel1_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel1.Content.ToString());            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetActualCity(Travel1.Content.ToString());
           // gm.incJuego();
            
        }

        void client_TravelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //TODO
        }

        private void Travel2_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel2.Content.ToString());            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetActualCity(Travel2.Content.ToString());
            
        }

        private void Travel3_Click(object sender, RoutedEventArgs e)
        {
            ServiceWP7Client client = new ServiceWP7Client();
            client.TravelCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(Travel3.Content.ToString());            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            GameManager gm = GameManager.getInstance();
            gm.SetActualCity(Travel3.Content.ToString());
            
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

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Juego.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}