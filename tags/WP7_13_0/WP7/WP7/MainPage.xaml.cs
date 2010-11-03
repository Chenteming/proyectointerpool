namespace WP7
{
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

    public partial class MainPage : PhoneApplicationPage
    {
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.getInstance();

        public MainPage()
        {
            InitializeComponent();            
            this.client.Endpoint.Binding.SendTimeout = TimeSpan.FromSeconds(6000);
            client.Endpoint.Binding.OpenTimeout = TimeSpan.FromSeconds(6000);
            intro_wma.Play();
			intro_animation.Begin();
            if (this.language.GetXDoc() != null)
			{
                this.language.TranslatePage(this);     
			}
			else
			{
				this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));	
				this.language.TranslatePage(this);
			}
        }

        void client_GetUserIdFacebookCompleted(object sender, GetUserIdFacebookCompletedEventArgs e)
        {
            this.gm.UserId = e.Result;
            this.client.StartGameCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_StartGameCompleted);
            this.client.StartGameAsync(this.gm.UserId);
            ////client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            ////client.GetCurrentCityAsync(gm.userId);
            ////client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            ////client.CloseAsync();

        }

        private void Play_Click(object sender, RoutedEventArgs e)
        { 
        }

		//// Asynchronous callbacks for displaying results.
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        void client_StartGameCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            this.client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            this.client.GetCurrentCityAsync(this.gm.UserId);
            this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            this.client.CloseAsync();
        }
        
        void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            DataCity dc = (DataCity)e.Result;
            gm.DeadLineDateTime = dc.DeadLine;
            this.gm.SetCurrentCity(dc.NameCity);
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
        }

        private void OptionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {        	
            NavigationService.Navigate(new Uri("/GamePages/Options.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PlayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {		
			////Usando el mail de FACEBOOK
            gm.UserEmail = "taru_borio@hotmail.com";
			client.GetUserIdFacebookAsync(gm.UserEmail);
            this.client.GetUserIdFacebookCompleted += new EventHandler<GetUserIdFacebookCompletedEventArgs>(client_GetUserIdFacebookCompleted);
            ////NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            ////task.URL = "http://pis2010.cloudapp.net/Default.aspx?loginid=" + loginId;
            task.URL = "http://127.0.0.1:81/Default.aspx?email=vicente_cai@hotmail.com";
            task.Show();
        }

   	 	private void ExitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }

   	 	private void LoginButton_Click(object sender, System.Windows.RoutedEventArgs e)
   	 	{
			NavigationService.Navigate(new Uri("/GamePages/Login.xaml", UriKind.RelativeOrAbsolute));
   	 	}
    }
}
