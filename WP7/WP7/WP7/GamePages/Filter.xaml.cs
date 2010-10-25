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
using WP7.Utilities;

namespace WP7.GamePages
{
    public partial class Filter : PhoneApplicationPage
    {
		private List<String> gender;
		private List<String> homeTown;
		private List<String> film;
		private List<String> music;
        private List<String> tv;
        private List<String> birthday;
        private string[] filters;
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private GameManager gm = GameManager.getInstance();
		private LanguageManager language = LanguageManager.GetInstance();
        int btnPosition = 0;
        private int Item = 0;
		
        public Filter()
        {			
            InitializeComponent();
			AnimationPage.Begin();
			// Change the language of the page            
            if (language.GetXDoc() != null)
                language.TranslatePage(this);	
            FilterButton.Visibility = System.Windows.Visibility.Collapsed;
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            DataFacebookUser dfu = new DataFacebookUser();
            client.FilterSuspectsAsync(gm.userId, dfu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            filters = new string[Constants.MAX_FILTERFIELD]; 
			updateFilters();
			ComboList.Visibility = Visibility.Collapsed;
		}

		void updateFilters() {
			string[] filterField = gm.GetFilterField();			
			GenderText.Text = filterField[3];
			HometownText.Text = filterField[4];
			MusicText.Text = filterField[5];
			TVText.Text = filterField[7];
			CinemaText.Text = filterField[6];
			BirthdayText.Text = filterField[2];
		} 
			
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
           
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            List<DataFacebookUser> dfu = e.Result.ToList();
            gender = new List<String>();
            film = new List<String>();
            homeTown = new List<String>();
            music = new List<String>();
			tv = new List<String>();
            birthday = new List<String>();
            foreach(DataFacebookUser df in dfu) 
            {
                if (!film.Contains(df.cinema))
                    film.Add(df.cinema);
                if (!gender.Contains(df.gender))
                    gender.Add(df.gender);
                if (!homeTown.Contains(df.hometown))
                    homeTown.Add(df.hometown);
                if (!music.Contains(df.music))
                    music.Add(df.music);
				if (!tv.Contains(df.television))
                    tv.Add(df.television);
                if (!birthday.Contains(df.birthday))
                    birthday.Add(df.birthday);
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            for (int i = 0; i < 8; i++) 
            {
                filterField[i] = filters[i];
            }                
            NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }       

        private void ComboList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ComboList.SelectedIndex != -1)
            {
				string[] filterField = gm.GetFilterField();
				filterField[btnPosition] = ComboList.SelectedItem.ToString();
                filters[btnPosition] = ComboList.SelectedItem.ToString();
                ComboList.Visibility = Visibility.Collapsed;
                ContentGrid2.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
				updateFilters();
            }
        }        


        private void TVButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	btnPosition = 7;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = tv;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void CinemaButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	btnPosition = 6;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = film;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void HometownButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	btnPosition = 3;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = homeTown;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void BirthdayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnPosition = 2;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = birthday;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void GenderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            btnPosition = 4;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = gender;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void MusicButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
 			btnPosition = 5;
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = music;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            /*if (ComboList.Visibility == Visibility.Visible)
            {
                Item = 1;
                NavigationService.Navigate(new Uri("/GamePages/Filter.xaml?Item=" + Item, UriKind.Relative));
            }
            else
                NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.Relative));*/
        }
    }
}