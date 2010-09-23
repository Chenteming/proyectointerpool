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
    public partial class Famous : PhoneApplicationPage
    {
        private ServiceWP7Client client;
        private LanguageManager language;

        public Famous()
        {
            InitializeComponent();
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
            GameManager gm = GameManager.getInstance();    
        
            client = new ServiceWP7Client();
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(client_GetCurrentFamousCompleted);
            client.GetCurrentFamousAsync(gm.GetCurrentCity());

            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            // Set the textboxes with the name of the famous
            button1.Visibility = System.Windows.Visibility.Collapsed;
                        
        }       
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void Famous1_Click(object sender, RoutedEventArgs e)
        {
            
        }



        private void GetClueByFamousCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            dialogText.Text = clue;           
        }
        void client_GetCurrentFamousCompleted(object sender, GetCurrentFamousCompletedEventArgs e)
        {
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentFamous(e.Result.ToList());            
            List<String> famous = gm.GetFamous();

            button1.Visibility = System.Windows.Visibility.Visible;


            //Show in the textBoxes the name of the famous
            button1.Content = famous.ElementAt(0);
          

            
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            //TODO
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	client = new ServiceWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            client.GetClueByFamousAsync(button1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync(); 
        }

    
    }
}