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
        private List<String> tv;
		
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private GameManager gm = GameManager.getInstance();
		private LanguageManager language = LanguageManager.GetInstance();
		
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
            }
        }

        /*private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	//FilterSuspectsStoryboard.Begin();
			comboHomeTown.Visibility = System.Windows.Visibility.Visible;
			comboFilm.Visibility = System.Windows.Visibility.Visible;
			comboMusic.Visibility = System.Windows.Visibility.Visible;
			comboGender.Visibility = System.Windows.Visibility.Visible;
			comboTV.Visibility = System.Windows.Visibility.Visible;
            OpenButton.Visibility = System.Windows.Visibility.Collapsed;

            string[] filterField = gm.GetFilterField();			
			comboHomeTown.SelectedItem = filterField[3];
			comboGender.SelectedItem = filterField[4];
			comboMusic.SelectedItem = filterField[5];
			comboFilm.SelectedItem = filterField[6];
			comboTV.SelectedItem = filterField[7];
			FilterButton.Visibility = System.Windows.Visibility.Visible;
        }*/

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
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
			 if (comboTV.SelectedItem != null)
                filterField[7] = comboTV.SelectedItem.ToString();
            else
                filterField[7] = "";
            NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
        }       

        private void ComboList_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (ComboList.SelectedIndex != -1)
            {
                string s = ComboList.SelectedItem.ToString();
                ComboList.Visibility = Visibility.Collapsed;
                ContentGrid2.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
            }
        }        

        private void HometownBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = filterField[3];
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void GenderBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = filterField[4];
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void MusicBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = filterField[5];
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void CinemaBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = filterField[6];
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void TVBtn_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            string[] filterField = gm.GetFilterField();
            ComboList.ItemsSource = filterField[7];
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }       
    }
}