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

namespace WP7.GamePages
{
    public partial class Filter : PhoneApplicationPage
    {
		private List<String> gender;
		private List<String> homeTown;
		private List<String> film;
		private List<String> music;
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private GameManager gm = GameManager.getInstance();

        public Filter()
        {			
            InitializeComponent();
			comboHomeTown.Visibility = System.Windows.Visibility.Collapsed;
			comboFilm.Visibility = System.Windows.Visibility.Collapsed;
			comboMusic.Visibility = System.Windows.Visibility.Collapsed;
			comboGender.Visibility = System.Windows.Visibility.Collapsed;
			string[] filterField = gm.GetFilterField();
			
			comboHomeTown.SelectedItem = filterField[3];
			comboGender.SelectedItem = filterField[4];
			comboMusic.SelectedItem = filterField[5];
			comboFilm.SelectedItem = filterField[6];
			
			
            FilterButton.Visibility = System.Windows.Visibility.Visible;
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            DataFacebookUser dfu = new DataFacebookUser();
            client.FilterSuspectsAsync(gm.userId, dfu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
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
            }                     
            comboGender.ItemsSource = gender;
            comboHomeTown.ItemsSource = homeTown;
            comboFilm.ItemsSource = film;
            comboMusic.ItemsSource = music;	
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	//FilterSuspectsStoryboard.Begin();
			comboHomeTown.Visibility = System.Windows.Visibility.Visible;
			comboFilm.Visibility = System.Windows.Visibility.Visible;
			comboMusic.Visibility = System.Windows.Visibility.Visible;
			comboGender.Visibility = System.Windows.Visibility.Visible;
            OpenButton.Visibility = System.Windows.Visibility.Collapsed;
            OpenButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            /*0 = first_name
              1 = last_name 
              2 =  birthday
              3 = hometown
              4 = gender
              5 = music
              6 = cinema
			  7 = television*/
            if (comboHomeTown.SelectedItem != null)
                 filterField[3] = comboHomeTown.SelectedItem.ToString();
            else
                filterField[3] = "";
            if (comboGender.SelectedItem != null)
                filterField[4] = comboGender.SelectedItem.ToString();
            else
                filterField[4] = "";
            if (comboMusic.SelectedItem != null)
                filterField[5] = comboMusic.SelectedItem.ToString();
            else
                filterField[5] = "";
            if (comboFilm.SelectedItem != null)
                filterField[6] = comboFilm.SelectedItem.ToString();
            else
                filterField[6] = "";
            NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}