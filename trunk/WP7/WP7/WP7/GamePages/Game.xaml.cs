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

    public partial class Game : PhoneApplicationPage
    {
        private GameManager gm = GameManager.GetInstance();
		private LanguageManager lm = LanguageManager.GetInstance();

        public Game()
        {
            InitializeComponent();            
            if (gm.ShowAnimation == true)
                ToastyStoryboard.Begin();
            gm.ShowAnimation = false;
			TextCity.Text = gm.GetCurrentCity();
			DateTime dt = gm.CurrentDateTime;
			string hour = dt.Hour < 10 ? "0" + dt.Hour : String.Empty + dt.Hour;
			string time = " pm";
			if (dt.Hour >= 0 && dt.Hour <= 12)
				time = " am";
			TextDate.Text = GetDayOfWeek(dt, lm.GetCurrentLanguage() == "English") + 
			" " + hour + time;
            ////string cityURI = "../CitiesImages/" + gm.PictureCityLink;
            ////cityImage.Source = new BitmapImage(new Uri(cityURI, UriKind.Relative));
        }

		private void Door_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Earth_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			NavigationService.Navigate(new Uri("/GamePages/City.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Files_Click(object sender, System.Windows.RoutedEventArgs e)
		{
            if (!gm.EmitOrder)
			    NavigationService.Navigate(new Uri("/GamePages/Filter.xaml", UriKind.RelativeOrAbsolute));
            else
                NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Laptop_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(2);
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Newspaper_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(1);
			NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Phone_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            gm.SetFamousIndex(0);
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

        private void help_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Help.xaml", UriKind.RelativeOrAbsolute));
        }

        private void options_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Options.xaml", UriKind.RelativeOrAbsolute));
        }

		 /// <summary>
       /// Get Day of week 
       /// </summary>
       /// <param name="currentDate">Parameter description for currentDate goes here</param>
       /// <returns>
       /// the day of the week</returns>
       private string GetDayOfWeek(DateTime currentDate, bool english)
       {
           switch (currentDate.DayOfWeek)
           {
               case DayOfWeek.Friday:
                   return english ? "Friday" : "Viernes";
               case DayOfWeek.Monday:
                   return english ? "Monday" : "Lunes";
               case DayOfWeek.Saturday:
                   return english ? "Saturday" : "Sabado";
               case DayOfWeek.Sunday:
                   return english ? "Sunday" : "Domingo";
               case DayOfWeek.Thursday:
                   return english? "Thursday" : "Jueves";
               case DayOfWeek.Tuesday:
                   return english ? "Tuesday" : "Martes";
               case DayOfWeek.Wednesday:
                   return english ? "Wednesday" : "Miércoles";
               default:
                   return "no existe día de la semana";
           }
       }
 
	}
}