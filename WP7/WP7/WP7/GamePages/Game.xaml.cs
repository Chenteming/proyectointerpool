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
    using System.Windows.Media.Imaging;

    public partial class Game : PhoneApplicationPage
    {
        private GameManager gm = GameManager.GetInstance();
		private LanguageManager lm = LanguageManager.GetInstance();

        public Game()
        {
            InitializeComponent();
            if (this.lm.GetXDoc() != null)
                this.lm.TranslatePage(this);            
            if (gm.ShowAnimation == true)
                ToastyStoryboard.Begin();
            gm.ShowAnimation = false;
			TextCity.Text = gm.GetCurrentCity();
			DateTime dt = gm.CurrentDateTime;
			DateTime dl = gm.DeadLineDateTime;
			string hour1 = dt.Hour < 10 ? "0" + dt.Hour : String.Empty + dt.Hour;
            string hour2 = dl.Hour < 10 ? "0" + dl.Hour : String.Empty + dl.Hour;
			string time1 = " pm";
			if (dt.Hour >= 0 && dt.Hour <= 12)
				time1 = " am";
			string time2 = " pm";
			if (dl.Hour >= 0 && dl.Hour <= 12)
				time2 = " am";			
			TextDate.Text = dateTB.Text + GetDayOfWeek(dt, lm.GetCurrentLanguage() == "English") + 
			" " + hour1 + time1;
            TextDeadline.Text =  deadlineTB.Text + GetDayOfWeek(dl, lm.GetCurrentLanguage() == "English") + 
			" " + hour2 + time2;
            TextLevel.Text = gm.Info == null ? "" : gm.Info.newLevel;
            string cityURI = "../cities3_Images/" + gm.PictureCityLink;
            imageCity.Source = new BitmapImage(new Uri(cityURI, UriKind.Relative));
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
            if (gm.OrderArrest == null)
			    NavigationService.Navigate(new Uri("/GamePages/Filter.xaml", UriKind.RelativeOrAbsolute));
            else
                NavigationService.Navigate(new Uri("/GamePages/Suspect.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Laptop_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            //gm.SetFamousIndex(2);
            NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Newspaper_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            //gm.SetFamousIndex(1);
			NavigationService.Navigate(new Uri("/GamePages/Famous.xaml", UriKind.RelativeOrAbsolute));
		}

		private void Phone_Click(object sender, System.Windows.RoutedEventArgs e)
		{			
            //gm.SetFamousIndex(0);
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
                   return english ? "Saturday" : "Sábado";
               case DayOfWeek.Sunday:
                   return english ? "Sunday" : "Domingo";
               case DayOfWeek.Thursday:
                   return english? "Thursday" : "Jueves";
               case DayOfWeek.Tuesday:
                   return english ? "Tuesday" : "Martes";
               case DayOfWeek.Wednesday:
                   return english ? "Wednesday" : "Miércoles";
               default:
                   return string.Empty;
           }
       }

       protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
       {
           NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
       }

       private void YesFailButton_Click(object sender, System.Windows.RoutedEventArgs e)
       {
           ShowHideInterpoolFailMessage("", false);
       }

       private void ShowHideInterpoolFailMessage(string message, bool flag)
       {
           failMessageText.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
           if (flag)
               failMessageText.Text = message;
           MessageImage.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
           YesFailButton.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
       }

	}
}