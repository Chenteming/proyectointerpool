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
        public Ciudad()
        {
            InitializeComponent();
            GameManager gm = GameManager.getInstance();
            List<String> cities = gm.GetCities();
            //Show in the textBoxes the name of the cities
            textBox1.Text = cities.ElementAt(0);
            textBox2.Text = cities.ElementAt(1);
            textBox3.Text = cities.ElementAt(2);
        }
		
		private void SetAndStartButon(object sender, EventArgs e)
        {
            //Start the storyboard
            Storyboard1.Begin();
        }

        private void Travel1_Click(object sender, RoutedEventArgs e)
        {
            Iweb2Client client = new Iweb2Client();
            client.TravelAsync(textBox1.Text);

            //Show the information about the new current city
            //textBox4 = getInfoCity(textBox1.Text);

            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCallback);
            client.GetPossibleCitiesAsync();

            GameManager gm = GameManager.getInstance();
            //Set the new current city
            gm.SetActualCity(textBox1.Text);

            //Set the new current famous
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(GetCurrentFamousCallback);
            client.GetCurrentFamousAsync(textBox1.Text);
            client.CloseAsync();
            //Show the animation
            NavigationService.Navigate(new Uri("/Viaje.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Travel2_Click(object sender, RoutedEventArgs e)
        {
            Iweb2Client client = new Iweb2Client();
            client.TravelAsync(textBox2.Text);

            //Show the information about the new current city
            //textBox5 = getInfoCity(textBox2.Text);

            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCallback);
            client.GetPossibleCitiesAsync();

            GameManager gm = GameManager.getInstance();
            //Set the new current city
            gm.SetActualCity(textBox2.Text);

            //Set the new current famous
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(GetCurrentFamousCallback);
            client.GetCurrentFamousAsync(textBox2.Text);
            client.CloseAsync();
            //Show the animation
            NavigationService.Navigate(new Uri("/Viaje.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Travel3_Click(object sender, RoutedEventArgs e)
        {
            Iweb2Client client = new Iweb2Client();
            client.TravelAsync(textBox3.Text);

            //Show the information about the new current city
            //textBox6 = getInfoCity(textBox3.Text);

            client.GetPossibleCitiesCompleted += new EventHandler<GetPossibleCitiesCompletedEventArgs>(GetPossibleCitiesCallback);
            client.GetPossibleCitiesAsync();

            GameManager gm = GameManager.getInstance();
            //Set the new current city
            gm.SetActualCity(textBox3.Text);

            //Set the new current famous
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(GetCurrentFamousCallback);
            client.GetCurrentFamousAsync(textBox3.Text);
            client.CloseAsync();
            //Show the animation
            NavigationService.Navigate(new Uri("/Viaje.xaml", UriKind.RelativeOrAbsolute));
        }

        // Asynchronous callbacks for displaying results.

        static void GetPossibleCitiesCallback(object sender, GetPossibleCitiesCompletedEventArgs e)
        {
            List<String> cities = e.Result.ToList();
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCities(cities);

            //Set the new current famous
            Iweb2Client client = new Iweb2Client();
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