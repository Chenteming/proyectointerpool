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
            FilterButton.Visibility = System.Windows.Visibility.Collapsed;

            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            DataFacebookUser dfu = new DataFacebookUser();
            client.FilterSuspectsAsync(gm.userId, dfu);

			// Setting comboGender
		   /* gender = new List<String>();
		    gender.Add("Masculino");			
		    gender.Add("Femenino");
		    comboGender.ItemsSource = gender;
			
		    homeTown = new List<String>();
		    homeTown.Add("Masculino");			
		    homeTown.Add("Femenino");
		    comboHomeTown.ItemsSource = homeTown;
			
		    film = new List<String>();
		    film.Add("MATRIX");			
		    film.Add("EXTERMINIO");
		    comboFilm.ItemsSource = film;
			
		    music = new List<String>();
		    music.Add("ROCK");			
		    music.Add("POP");
		    comboMusic.ItemsSource = music;	*/
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            List<DataFacebookUser> dfu = e.Result.ToList();
            gender = new List<String>();
            film = new List<String>();
            homeTown = new List<String>();
            music = new List<String>();
            // TODO chequear repetidos no se agregen
            foreach(DataFacebookUser df in dfu) 
            {
                film.Add(df.cinema);
                gender.Add(df.gender);
                homeTown.Add(df.hometown);
                music.Add(df.music);
            }                     
            comboGender.ItemsSource = gender;
            comboHomeTown.ItemsSource = homeTown;
            comboFilm.ItemsSource = film;
            comboMusic.ItemsSource = music;	
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	FilterStoryboard.Begin();
			comboHomeTown.Visibility = System.Windows.Visibility.Visible;
			comboFilm.Visibility = System.Windows.Visibility.Visible;
			comboMusic.Visibility = System.Windows.Visibility.Visible;
			comboGender.Visibility = System.Windows.Visibility.Visible;
            OpenButton.Visibility = System.Windows.Visibility.Collapsed;
            OpenButton.Visibility = System.Windows.Visibility.Visible;
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}