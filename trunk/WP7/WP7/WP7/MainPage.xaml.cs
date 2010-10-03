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
using System.Xml.Linq;
using Microsoft.Phone.Tasks;

namespace WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.getInstance();

        public MainPage()
        {
            InitializeComponent();
            
            client.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(3000);
            //client.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(300);
            intro_wma.Play();
			intro_animation.Begin();			
            if (language.GetXDoc() != null)
			{
                language.TranslatePage(this);     
			}
			else
			{
				language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));	
				language.TranslatePage(this);
			}                       
			
            
            
        }

        void client_GetUserIdFacebookCompleted(object sender, GetUserIdFacebookCompletedEventArgs e)
        {
            gm.userId = e.Result;

            client.StartGameCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_StartGameCompleted);
            client.StartGameAsync(gm.userId);
            //client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            //client.GetCurrentCityAsync(gm.userId);
            //client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            //client.CloseAsync();

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
          
        }

		// Asynchronous callbacks for displaying results.
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        void client_StartGameCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            client.GetCurrentCityAsync(gm.userId);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }
        
        void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            DataCity dc = (DataCity)e.Result;
            gm.SetCurrentCity(dc.name_city);
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OptionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	string current = language.GetCurrentLanguage();
            if (current.Equals("English"))
			{
                language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
				language.SetCurrentLanguage("Spanish");
			}
            else
			{
                language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
				language.SetCurrentLanguage("English");	
			}
            language.TranslatePage(this);
        }

        private void PlayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {		
            client.GetUserIdFacebookAsync("");
            client.GetUserIdFacebookCompleted += new EventHandler<GetUserIdFacebookCompletedEventArgs>(client_GetUserIdFacebookCompleted);
           
            // NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            //task.URL = "http://servicewp7.cloudapp.net";
            task.URL = "http://127.0.0.1:81/";
            task.Show();
        }
   	 	private void ExitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
