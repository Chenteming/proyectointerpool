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
    public partial class MainPage : PhoneApplicationPage
    {


        public MainPage()
        {
            InitializeComponent();
			intro_wma.Play();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
			GameManager gm = GameManager.getInstance();
            
            //Set initial current citie
            ServiceWP7Client client = new ServiceWP7Client();
 
            client.StartGameAsync();
           
            client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            client.GetCurrentCityAsync();
                               
            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCallback);
            client.GetPossibleCitiesAsync();           

            client.CloseAsync();           

            NavigationService.Navigate(new Uri("/Juego.xaml", UriKind.RelativeOrAbsolute));
        }

        // Asynchronous callbacks for displaying results.
        static void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            String initialCity = e.Result;
            GameManager gm = GameManager.getInstance();
            gm.SetActualCity(initialCity); 
        }

        static void GetPossibleCitiesCallback(object sender, GetPossibleCitiesCompletedEventArgs e)
        {
            List<String> cities = e.Result.ToList();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCities(cities);

            ServiceWP7Client client = new ServiceWP7Client();
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(GetCurrentFamousCallback);
            String ac = gm.GetActualCity();
            client.GetCurrentFamousAsync(ac);
            client.CloseAsync();
        }

        static void GetCurrentFamousCallback(object sender, GetCurrentFamousCompletedEventArgs e)
        {
            List<String> famous = e.Result.ToList();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentFamous(famous); 
        }

    }
}
