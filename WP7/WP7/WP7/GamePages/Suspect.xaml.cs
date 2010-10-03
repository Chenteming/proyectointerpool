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

namespace WP7
{
    public partial class Suspect : PhoneApplicationPage
    {
        private List<String> suspectsList;
        private static int index;
		private LanguageManager language;
		private GameManager gm = GameManager.getInstance();
        private List<DataFacebookUser> dfbuList = new List<DataFacebookUser>();

        public Suspect()
        {
            InitializeComponent();
			// Change the language of the page
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
			InterpoolWP7Client client = new InterpoolWP7Client();
            DataFacebookUser dfbu = new DataFacebookUser();
            /*0 = first_name
              1 = last_name 
              2 =  birthday
              3 = hometown
              4 = gender
              5 = music
              6 = cinema*/
            string[] filterField = gm.GetFilterField();
            dfbu.first_name = filterField[0];
            dfbu.last_name = filterField[1];
            dfbu.birthday = filterField[2];
            dfbu.hometown = filterField[3];
            dfbu.gender = filterField[4];
            dfbu.music = filterField[5];
            dfbu.cinema = filterField[6];
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            client.FilterSuspectsAsync(gm.userId, dfbu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            index = 0;
            dfbuList = e.Result.ToList();
            if (dfbuList.Count == 0)

                Name_Suspect.Text = "There are no suspects";
            else
            {                    
                dfbuList = e.Result.ToList();
                Name_Suspect.Text = dfbuList.ElementAt(0).first_name + dfbuList.ElementAt(0).last_name;
                hometown.Text = dfbuList.ElementAt(0).hometown;
                birthdayTB.Text = dfbuList.ElementAt(0).birthday;
                hometownTB.Text = dfbuList.ElementAt(0).hometown;
                genderTB.Text = dfbuList.ElementAt(0).gender;
                musicTB.Text = dfbuList.ElementAt(0).music;
                cinemaTB.Text = dfbuList.ElementAt(0).cinema;

                LoadPicture(dfbuList.ElementAt(0).pictureLink);
            }
        }

        private void LeftArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dfbuList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
            {
                if (index == 0)
                    index = dfbuList.Count - 1;
                else
                    index--;

                Name_Suspect.Text = dfbuList.ElementAt(index).first_name + dfbuList.ElementAt(index).last_name;
                birthdayTB.Text = dfbuList.ElementAt(index).birthday;
                hometownTB.Text = dfbuList.ElementAt(index).hometown;
                genderTB.Text = dfbuList.ElementAt(index).gender;
                musicTB.Text = dfbuList.ElementAt(index).music;
                cinemaTB.Text = dfbuList.ElementAt(index).cinema;

                LoadPicture(dfbuList.ElementAt(index).pictureLink);
            }
        }

        private void RightArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dfbuList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
            {
                if (index == dfbuList.Count - 1)
                    index = 0;
                else
                    index++;

                Name_Suspect.Text = dfbuList.ElementAt(index).first_name + dfbuList.ElementAt(index).last_name;
                birthdayTB.Text = dfbuList.ElementAt(index).birthday;
                hometownTB.Text = dfbuList.ElementAt(index).hometown;
                genderTB.Text = dfbuList.ElementAt(index).gender;
                musicTB.Text = dfbuList.ElementAt(index).music;
                cinemaTB.Text = dfbuList.ElementAt(index).cinema;

                LoadPicture(dfbuList.ElementAt(index).pictureLink);
            }
        }

        private void LoadPicture(string pictureLink)
        {
            WebClient webClientImgDownloader = new WebClient();
            webClientImgDownloader.OpenReadCompleted += new OpenReadCompletedEventHandler(webClientImgDownloader_OpenReadCompleted);
            webClientImgDownloader.OpenReadAsync(new Uri(pictureLink, UriKind.Absolute));
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
            client.EmitOrderOfArrestCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_EmitOrderOfArrestCompleted);
            client.EmitOrderOfArrestAsync(gm.userId, dfbuList.ElementAt(index).id_friend);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        void client_EmitOrderOfArrestCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }
    }
}