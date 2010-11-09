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
    using WP7.Utilities;
    using System.ServiceModel;

    public partial class MainPage : PhoneApplicationPage
    {
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.GetInstance();

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

        void client_GetUserInfoCompleted(object sender, GetUserInfoCompletedEventArgs e)
        {
            this.gm.UserInfo = e.Result;
            this.gm.UserId = gm.UserInfo.UserIdFacebook;
            this.gm.GetUserInfoTries++;
            if (gm.UserInfo.UserState == UserState.NO_REGISTERED || gm.UserInfo.UserState == UserState.REGISTERED_NO_PLAYING_LOGIN_REQUIRED)
            {
                if (gm.GetUserInfoTries >= 3)
                {
                    //// TODO: Message telling the user that his email or the login to Facebook is wrong
                    gm.FromMainPage = true;
                    NavigationService.Navigate(new Uri("/GamePages/Login.xaml", UriKind.RelativeOrAbsolute));
                }
                else
                {
                    WebBrowserTask task = new WebBrowserTask();
                    task.URL = Constants.FACEBOOK_LOGIN_URL;
                    task.Show();
                    gm.BrowserOpened = true;
                }
            }
            else
            {
                try
                {
                    this.client.StartGameCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_StartGameCompleted);
                    if (gm.UserInfo.UserState == UserState.REGISTERED_NO_PLAYING)
                    {
                        NavigationService.Navigate(new Uri("/GamePages/Start.xaml", UriKind.RelativeOrAbsolute));
                    }

                    this.client.StartGameAsync(this.gm.UserId);
                }
                catch (FaultException ex) 
                {
                    ShowHideInterpoolFailMessage(ex.Message, true);
                }
            }
        }

		//// Asynchronous callbacks for displaying results.
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        void client_StartGameCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            try
            {
                this.client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
                this.client.GetCurrentCityAsync(this.gm.UserId);
            }
            catch (FaultException ex) 
            {
                ShowHideInterpoolFailMessage(ex.Message, true);
            }           
        }

        void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            DataCity dc = (DataCity)e.Result;
            this.gm.Info = dc.GameInfo;
            gm.Left = dc.Left;
            gm.Top = dc.Top;
            gm.DeadLineDateTime = dc.DeadLine;
            gm.CurrentDateTime = dc.CurrentDate;
            this.gm.SetCurrentCity(dc.NameCity);
            this.gm.PictureCityLink = dc.NameFileCity;
            this.gm.CurrentLevel = dc.GameInfo.newLevel;
            if (gm.UserInfo.UserState == UserState.REGISTERED_NO_PLAYING)
            {
                NavigationService.Navigate(new Uri("/GamePages/StartCompleted.xaml", UriKind.RelativeOrAbsolute));
            }
            else if (gm.UserInfo.UserState == UserState.REGISTERED_PLAYING)
            {
                NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
            }
            try
            {
                this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
                this.client.CloseAsync();
            }
            catch (FaultException ex) 
            {
                ShowHideInterpoolFailMessage(ex.Message, true);
            }
        }

        private void OptionButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {        	
            NavigationService.Navigate(new Uri("/GamePages/Options.xaml", UriKind.RelativeOrAbsolute));
        }

        private void PlayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            //// If no mail was entered or the user has tried more than 3 times to start the game
            if (String.IsNullOrEmpty(gm.UserEmail) || gm.GetUserInfoTries >= 3)
            {
                gm.FromMainPage = true;
                NavigationService.Navigate(new Uri("/GamePages/Login.xaml", UriKind.RelativeOrAbsolute));
            }
            else
            {
                ////gm.UserEmail = "taru_borio@hotmail.com";
                ////client.GetUserIdFacebookAsync(gm.UserEmail);
                try
                {
                    this.client.GetUserInfoCompleted += new EventHandler<GetUserInfoCompletedEventArgs>(client_GetUserInfoCompleted);
                    client.GetUserInfoAsync(gm.UserEmail);
                }
                catch (FaultException ex) 
                {
                    ShowHideInterpoolFailMessage(ex.Message, true);
                }
            }
        }

        private void HelpButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Help.xaml", UriKind.RelativeOrAbsolute));
        }

   	 	private void ExitButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	////NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }

        private void CreditButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Credits.xaml", UriKind.RelativeOrAbsolute));
        }

        private void YesFailButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowHideInterpoolFailMessage("", false);
        }

        private void ShowHideInterpoolFailMessage(string message, bool flag)
        {
            failMessageText.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            if (flag)
            {
                failMessageText.Text = message;
            }
            MessageImage.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            YesFailButton.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
        }
    }
}
