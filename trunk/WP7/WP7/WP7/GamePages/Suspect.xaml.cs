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

namespace WP7
{
    public partial class Suspect : PhoneApplicationPage
    {
        private List<String> suspectsList;
        private static int index;
		private LanguageManager language;
		private GameManager gm = GameManager.getInstance();
        private List<DataFacebookUser> dfu = new List<DataFacebookUser>();

        public Suspect()
        {
            InitializeComponent();
			// Change the language of the page
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
			InterpoolWP7Client client = new InterpoolWP7Client();
            DataFacebookUser dfu = new DataFacebookUser();
            /*0 = first_name
              1 = last_name 
              2 =  birthday
              3 = hometown
              4 = gender
              5 = music
              6 = cinema*/
            string[] filterField = gm.GetFilterField();
            dfu.first_name = filterField[0];
            dfu.last_name = filterField[1];
            dfu.birthday = filterField[2];
            dfu.hometown = filterField[3];
            dfu.gender = filterField[4];
            dfu.music = filterField[5];
            dfu.cinema = filterField[6];
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            client.FilterSuspectsAsync(gm.userId, dfu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            index = 0;
            if (dfu.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                {
                    dfu = e.Result.ToList();
                    Name_Suspect.Text = suspectsList.ElementAt(0);
                    hometown.Text = dfu.ElementAt(0).hometown;
                    birthdayTB.Text = dfu.ElementAt(0).birthday;
                    hometownTB.Text = dfu.ElementAt(0).hometown;
                    genderTB.Text = dfu.ElementAt(0).gender;
                    musicTB.Text = dfu.ElementAt(0).music;
                    cinemaTB.Text = dfu.ElementAt(0).cinema;
                }
        }
    
        private void LeftArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dfu.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                if (index == 0)
                {
                    Name_Suspect.Text = suspectsList.ElementAt(0);
                    hometown.Text = dfu.ElementAt(0).hometown;
                    birthdayTB.Text = dfu.ElementAt(0).birthday;
                    hometownTB.Text = dfu.ElementAt(0).hometown;
                    genderTB.Text = dfu.ElementAt(0).gender;
                    musicTB.Text = dfu.ElementAt(0).music;
                    cinemaTB.Text = dfu.ElementAt(0).cinema;
                }
                else
                {
                    index--;
                    Name_Suspect.Text = dfu.ElementAt(index).first_name;
                    hometown.Text = dfu.ElementAt(index).hometown;
                    birthdayTB.Text = dfu.ElementAt(index).birthday;
                    hometownTB.Text = dfu.ElementAt(index).hometown;
                    genderTB.Text = dfu.ElementAt(index).gender;
                    musicTB.Text = dfu.ElementAt(index).music;
                    cinemaTB.Text = dfu.ElementAt(index).cinema;
                }
        }

        private void RightArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            if (dfu.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                if (index == suspectsList.Count - 1)
                {
                    Name_Suspect.Text = suspectsList.ElementAt(0);
                    hometown.Text = dfu.ElementAt(0).hometown;
                    birthdayTB.Text = dfu.ElementAt(0).birthday;
                    hometownTB.Text = dfu.ElementAt(0).hometown;
                    genderTB.Text = dfu.ElementAt(0).gender;
                    musicTB.Text = dfu.ElementAt(0).music;
                    cinemaTB.Text = dfu.ElementAt(0).cinema;
                }
                else
                {
                    index++;
                    Name_Suspect.Text = dfu.ElementAt(index).first_name;                    
                    birthdayTB.Text = dfu.ElementAt(index).birthday;
                    hometownTB.Text = dfu.ElementAt(index).hometown;
                    genderTB.Text = dfu.ElementAt(index).gender;
                    musicTB.Text = dfu.ElementAt(index).music;
                    cinemaTB.Text = dfu.ElementAt(index).cinema;

                }	
        }

      
    }
}