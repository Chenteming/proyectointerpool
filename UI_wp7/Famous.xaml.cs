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
    public partial class Famous : PhoneApplicationPage
    {
        private ServiceWP7Client client;

        public Famous()
        {
            InitializeComponent();
            GameManager gm = GameManager.getInstance();    
        
            client = new ServiceWP7Client();
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(client_GetCurrentFamousCompleted);
            client.GetCurrentFamousAsync(gm.GetCurrentCity());

            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            // Set the textboxes with the name of the famous
            Famous1.Visibility = System.Windows.Visibility.Collapsed;
            Famous2.Visibility = System.Windows.Visibility.Collapsed;
            Famous3.Visibility = System.Windows.Visibility.Collapsed;            
        }       
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void Famous1_Click(object sender, RoutedEventArgs e)
        {
            client = new ServiceWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);          
            client.GetClueByFamousAsync(Famous1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void Famous2_Click(object sender, RoutedEventArgs e)
        {
            client = new ServiceWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            client.GetClueByFamousAsync(Famous2.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void Famous3_Click(object sender, RoutedEventArgs e)
        {            
            client = new ServiceWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            client.GetClueByFamousAsync(Famous3.Content.ToString());            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void GetClueByFamousCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            Clue.Text = clue;           
        }
        void client_GetCurrentFamousCompleted(object sender, GetCurrentFamousCompletedEventArgs e)
        {
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentFamous(e.Result.ToList());            
            List<String> famous = gm.GetFamous();

            Famous1.Visibility = System.Windows.Visibility.Visible;
            Famous2.Visibility = System.Windows.Visibility.Visible;
            Famous3.Visibility = System.Windows.Visibility.Visible;

            //Show in the textBoxes the name of the famous
            Famous1.Content = famous.ElementAt(0);
            Famous2.Content = famous.ElementAt(1);
            Famous3.Content = famous.ElementAt(2);

            
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //TODO
        }

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Game.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}