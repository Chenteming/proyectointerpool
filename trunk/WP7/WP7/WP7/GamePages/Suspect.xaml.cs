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
		private GameManager gm = GameManager.GetInstance();
        private List<DataFacebookUser> dfbuList = new List<DataFacebookUser>();

        public Suspect()
        {
            InitializeComponent();
			AnimationPage.Begin();
			////Change the language of the page
            this.language = LanguageManager.GetInstance();
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);            
            if (gm.EmitOrder)
            {
                ShowCurrentSuspect();
                HideButtons();
            }
            else {
                InterpoolWP7Client client = new InterpoolWP7Client();
                DataFacebookUser dfbu = SetFiltersFields();
                client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(this.client_FilterSuspectsCompleted);
                client.FilterSuspectsAsync(gm.UserId, dfbu);
                client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
                client.CloseAsync();
            }
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
                ShowSuspect(0);
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
                ShowSuspect(index);
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
                ShowSuspect(index);
            }
        }

        private void LoadPicture(string pictureLink)
        {
            try
            {
                if (pictureLink != null && !pictureLink.Equals(string.Empty))                    
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
            catch (Exception excepcion)
            {
                string famousURI = "/WP7;component/interpool_Images/pantalla_6_Images/Capa 3.png";
                imageSuspect.Source = new BitmapImage(new Uri(famousURI, UriKind.Relative));
            }

        }

        void webClientImgDownloader_OpenReadCompleted(object sender, OpenReadCompletedEventArgs e)
        {
            try
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.SetSource(e.Result);
                imageSuspect.Source = bitmap;
            }
            catch (Exception excepcion)
            {
                string famousURI = "/WP7;component/interpool_Images/pantalla_6_Images/Capa 3.png";
                imageSuspect.Source = new BitmapImage(new Uri(famousURI, UriKind.Relative));
            }
        }

        private void Emit_Click(object sender, RoutedEventArgs e)
        {
            InterpoolWP7Client client = new InterpoolWP7Client();
            client.EmitOrderOfArrestCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_EmitOrderOfArrestCompleted);
            client.EmitOrderOfArrestAsync(gm.UserId, this.dfbuList.ElementAt(index).IdFriend);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            Emit.IsEnabled = false;            
            gm.EmitOrder = true;
            string[] filterField = gm.GetFilterField();
            filterField[0] = this.dfbuList.ElementAt(index).FirstName;
            filterField[1] = this.dfbuList.ElementAt(index).LastName;
            gm.PictureLink = this.dfbuList.ElementAt(index).PictureLink;
            ShowHideInterpoolFailMessage(messageText.Text + Name_Suspect.Text, true);            
        }

        void client_EmitOrderOfArrestCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void Return_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowSuspect(int index) 
        {
            Name_Suspect.Text = this.dfbuList.ElementAt(index).FirstName + " " + dfbuList.ElementAt(index).LastName;
            birthdayTB.Text = this.dfbuList.ElementAt(index).Birthday;
            hometownTB.Text = this.dfbuList.ElementAt(index).Hometown;
            genderTB.Text = this.dfbuList.ElementAt(index).Gender;
            musicTB.Text = this.dfbuList.ElementAt(index).Music;
            cinemaTB.Text = this.dfbuList.ElementAt(index).Cinema;
            televisionTB.Text = this.dfbuList.ElementAt(index).Television;
            LoadPicture(this.dfbuList.ElementAt(index).PictureLink);        
        }

        private void ShowCurrentSuspect()
        {
            string[] filterField = gm.GetFilterField();
            Name_Suspect.Text = filterField[0] + " " + filterField[1];
            birthdayTB.Text = filterField[2];
            hometownTB.Text = filterField[3];
            genderTB.Text = filterField[4];
            musicTB.Text = filterField[5];
            cinemaTB.Text = filterField[6];
            televisionTB.Text = filterField[7];
            LoadPicture(gm.PictureLink);
        }

        private DataFacebookUser SetFiltersFields()
        {
            string[] filterField = gm.GetFilterField();
            DataFacebookUser dfbu = new DataFacebookUser();
            dfbu.FirstName = filterField[0];
            dfbu.LastName = filterField[1];
            dfbu.Birthday = filterField[2];
            dfbu.Hometown = filterField[3];
            dfbu.Gender = filterField[4];
            dfbu.Music = filterField[5];
            dfbu.Cinema = filterField[6];
            dfbu.Television = filterField[7];
            return dfbu;
        }

        private void HideButtons()        
        {
            rightArrow.Visibility = Visibility.Collapsed;
            leftArrow.Visibility = Visibility.Collapsed;
            Emit.Visibility = Visibility.Collapsed;
        }

        private void YesFailButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowHideInterpoolFailMessage("", false);
			NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));            
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