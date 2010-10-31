namespace WP7.GamePages
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
    using WP7.Utilities;

    public partial class Filter : PhoneApplicationPage
    {
        private List<String> gender;
        private List<string> homeTown;
        private List<string> film;
        private List<string> music;
        private List<string> tv;
        private List<string> birthday;
        private string[] filters;
        private InterpoolWP7Client client = new InterpoolWP7Client();
        private GameManager gm = GameManager.getInstance();
        private LanguageManager language = LanguageManager.GetInstance();
        private int btnPosition = 0;
        private int item = 0;
		
        public Filter()
        {			
            InitializeComponent();
			AnimationPage.Begin();
			////Change the language of the page            
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);
            this.client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(this.client_FilterSuspectsCompleted);
            DataFacebookUser dfu = new DataFacebookUser();
            this.client.FilterSuspectsAsync(this.gm.UserId, dfu);
            this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            this.client.CloseAsync();
            filters = new string[Constants.MAX_FILTERFIELD]; 
			this.updateFilters();
            ComboList.Visibility = Visibility.Collapsed;
        }

        void updateFilters() {
            string[] filterField = this.gm.GetFilterField();
            GenderText.Text = filterField[4];
            HometownText.Text = filterField[3];
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
            List<DataFacebookUser> dfu = e.Result.ListFacebookUser.ToList();
            this.gender = new List<string>();
            this.film = new List<string>();
            this.homeTown = new List<string>();
            this.music = new List<string>();
			this.tv = new List<string>();
            this.birthday = new List<string>();
            foreach (DataFacebookUser df in dfu)
            {
                if (!this.film.Contains(df.Cinema))
                    this.film.Add(df.Cinema);
                if (!this.gender.Contains(df.Gender))
                    this.gender.Add(df.Gender);
                if (!this.homeTown.Contains(df.Hometown))
                    this.homeTown.Add(df.Hometown);
                if (!this.music.Contains(df.Music))
                    this.music.Add(df.Music);
                if (!this.tv.Contains(df.Television))
                    this.tv.Add(df.Television);
                if (!this.birthday.Contains(df.Birthday))
                    this.birthday.Add(df.Birthday);
            }
        }

        private void FilterButton_Click(object sender, RoutedEventArgs e)
        {
            string[] filterField = this.gm.GetFilterField();
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
				string[] filterField = this.gm.GetFilterField();
                filterField[this.btnPosition] = ComboList.SelectedItem.ToString();
                this.filters[this.btnPosition] = ComboList.SelectedItem.ToString();
                ComboList.Visibility = Visibility.Collapsed;
                ContentGrid2.Visibility = Visibility.Collapsed;
                ContentGrid.Visibility = Visibility.Visible;
				this.updateFilters();
            }
        }        


        private void TVButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.btnPosition = 7;
            string[] filterField = this.gm.GetFilterField();
            ComboList.ItemsSource = tv;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void CinemaButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.btnPosition = 6;
            string[] filterField = this.gm.GetFilterField();
            ComboList.ItemsSource = this.film;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void HometownButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	this.btnPosition = 3;
            string[] filterField = this.gm.GetFilterField();
            ComboList.ItemsSource = homeTown;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void BirthdayButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.btnPosition = 2;
            string[] filterField = this.gm.GetFilterField();
            ComboList.ItemsSource = this.birthday;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void GenderButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            this.btnPosition = 4;
            string[] filterField = this.gm.GetFilterField();
            ComboList.ItemsSource = gender;
            ContentGrid.Visibility = Visibility.Collapsed;
            ContentGrid2.Visibility = Visibility.Visible;
            ComboList.Visibility = Visibility.Visible;
        }

        private void MusicButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
 			this.btnPosition = 5;
            string[] filterField = this.gm.GetFilterField();
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