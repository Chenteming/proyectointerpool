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

    public partial class Suspect : PhoneApplicationPage
    {
        private static int index;
		private LanguageManager language;
		private GameManager gm = GameManager.getInstance();
        private List<DataFacebookUser> dfbuList = new List<DataFacebookUser>();

        public Suspect()
        {
            InitializeComponent();
			AnimationPage.Begin();
			////Change the language of the page
            this.language = LanguageManager.GetInstance();
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);
			InterpoolWP7Client client = new InterpoolWP7Client();
            DataFacebookUser dfbu = new DataFacebookUser();
            /*0 = first_name
              1 = last_name 
              2 =  birthday
              3 = hometown
              4 = gender
              5 = music
              6 = cinema
			  7 = television*/
            string[] filterField = gm.GetFilterField();
            dfbu.FirstName = filterField[0];
            dfbu.LastName = filterField[1];
            dfbu.Birthday = filterField[2];
            dfbu.Hometown = filterField[3];
            dfbu.Gender = filterField[4];
            dfbu.Music = filterField[5];
            dfbu.Cinema = filterField[6];
			dfbu.Television = filterField[7];
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(this.client_FilterSuspectsCompleted);
            client.FilterSuspectsAsync(gm.UserId, dfbu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            client.CloseAsync();
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        { 
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            index = 0;
            this.dfbuList = e.Result.ListFacebookUser.ToList();
            if (this.dfbuList.Count == 0)

                Name_Suspect.Text = "There are no suspects";
            else
            {   
                Name_Suspect.Text = this.dfbuList.ElementAt(0).FirstName + " " + this.dfbuList.ElementAt(0).LastName;
                hometownTB.Text = this.dfbuList.ElementAt(0).Hometown;
                birthdayTB.Text = this.dfbuList.ElementAt(0).Birthday;
                hometownTB.Text = this.dfbuList.ElementAt(0).Hometown;
                genderTB.Text = this.dfbuList.ElementAt(0).Gender;
                musicTB.Text = this.dfbuList.ElementAt(0).Music;
                cinemaTB.Text = this.dfbuList.ElementAt(0).Cinema;
				televisionTB.Text = this.dfbuList.ElementAt(0).Television;
                LoadPicture(this.dfbuList.ElementAt(0).PictureLink);
            }
        }

        private void LeftArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.dfbuList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
            {
                if (index == 0)
                    index = this.dfbuList.Count - 1;
                else
                    index--;

                Name_Suspect.Text = this.dfbuList.ElementAt(index).FirstName + " " + this.dfbuList.ElementAt(index).LastName;
                birthdayTB.Text = this.dfbuList.ElementAt(index).Birthday;
                hometownTB.Text = this.dfbuList.ElementAt(index).Hometown;
                genderTB.Text = this.dfbuList.ElementAt(index).Gender;
                musicTB.Text = this.dfbuList.ElementAt(index).Music;
                cinemaTB.Text = this.dfbuList.ElementAt(index).Cinema;
				televisionTB.Text = this.dfbuList.ElementAt(index).Television;
                LoadPicture(this.dfbuList.ElementAt(index).PictureLink);
            }
        }

        private void RightArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (this.dfbuList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
            {
                if (index == this.dfbuList.Count - 1)
                    index = 0;
                else
                    index++;

                Name_Suspect.Text = this.dfbuList.ElementAt(index).FirstName + " " + dfbuList.ElementAt(index).LastName;
                birthdayTB.Text = this.dfbuList.ElementAt(index).Birthday;
                hometownTB.Text = this.dfbuList.ElementAt(index).Hometown;
                genderTB.Text = this.dfbuList.ElementAt(index).Gender;
                musicTB.Text = this.dfbuList.ElementAt(index).Music;
                cinemaTB.Text = this.dfbuList.ElementAt(index).Cinema;
				televisionTB.Text = this.dfbuList.ElementAt(index).Television;
                LoadPicture(this.dfbuList.ElementAt(index).PictureLink);
            }
        }

        private void LoadPicture(string pictureLink)
        {
            if (null != pictureLink && pictureLink.Equals(string.Empty))
            {
                WebClient webClientImgDownloader = new WebClient();
                webClientImgDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(webClientImgDownloader_OpenReadCompleted);
                webClientImgDownloader.OpenReadAsync(new Uri(pictureLink, UriKind.Absolute));
            }
            else 
            {
                string famousURI = "/WP7;component/interpool_Images/pantalla_6_Images/Capa 3.png";
                imageSuspect.Source = new BitmapImage(new Uri(famousURI, UriKind.Relative));
            }

        }

        void webClientImgDownloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            BitmapImage bitmap = new BitmapImage();
            bitmap.SetSource(e.Result);
            imageSuspect.Source = bitmap;
        }

        private void Emit_Click(object sender, RoutedEventArgs e)
        {
            InterpoolWP7Client client = new InterpoolWP7Client();
            client.EmitOrderOfArrestCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_EmitOrderOfArrestCompleted);
            client.EmitOrderOfArrestAsync(gm.UserId, this.dfbuList.ElementAt(index).IdFriend);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            Emit.IsEnabled = false;
            MessageBox.Show("Se ha emitido una orden de arresto para " + Name_Suspect.Text);
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));            
        }

        void client_EmitOrderOfArrestCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void Return_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}