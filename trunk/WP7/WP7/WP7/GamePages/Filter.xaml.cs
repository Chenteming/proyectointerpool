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

namespace WP7.GamePages
{
    public partial class PagePruv : PhoneApplicationPage
    {
		private List<String> gender;
		private List<String> homeTown;
		private List<String> film;
		private List<String> music;
        public PagePruv()
        {			
            InitializeComponent();
			comboHomeTown.Visibility = System.Windows.Visibility.Collapsed;
			comboFilm.Visibility = System.Windows.Visibility.Collapsed;
			comboMusic.Visibility = System.Windows.Visibility.Collapsed;
			comboGender.Visibility = System.Windows.Visibility.Collapsed;
            FilterButton.Visibility = System.Windows.Visibility.Collapsed;
			
			// Setting comboGender
			gender = new List<String>();
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
			comboMusic.ItemsSource = music;	
			
//			// Setting comboHomeTown
//			cboxitem = new ComboBoxItem();
//			cboxitem.Content = "Montevideo";
//			comboHomeTown.Items.Add(cboxitem);
//			cboxitem2 = new ComboBoxItem();
//			cboxitem2.Content = "Rocha";
//			comboHomeTown.Items.Add(cboxitem2);
//
//			// Setting comboFilm
//			cboxitem = new ComboBoxItem();
//			cboxitem.Content = "Matrix";
//			comboFilm.Items.Add(cboxitem);
//			cboxitem2 = new ComboBoxItem();
//			cboxitem2.Content = "Jurasic Park";
//			comboFilm.Items.Add(cboxitem2);
//			cboxitem3 = new ComboBoxItem();
//			cboxitem3.Content = "Sponge Bob";
//			comboFilm.Items.Add(cboxitem3);
//			cboxitem4 = new ComboBoxItem();
//			cboxitem4.Content = "The saw";
//			comboFilm.Items.Add(cboxitem4);
//			
//			// Setting comboMusic
//			cboxitem = new ComboBoxItem();
//			cboxitem.Content = "No te va gustar";
//			comboMusic.Items.Add(cboxitem);
//			cboxitem2 = new ComboBoxItem();
//			cboxitem2.Content = "Redondos";
//			comboMusic.Items.Add(cboxitem2);
//			cboxitem3 = new ComboBoxItem();
//			cboxitem3.Content = "Beatles";
//			comboMusic.Items.Add(cboxitem3);
//			cboxitem4 = new ComboBoxItem();
//			cboxitem4.Content = "Mana";
//			comboMusic.Items.Add(cboxitem4);
//			
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