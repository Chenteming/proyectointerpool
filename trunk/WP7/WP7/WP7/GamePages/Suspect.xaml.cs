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

        public Suspect()
        {
            InitializeComponent();
			// Change the language of the page
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
			InterpoolWP7Client client = new InterpoolWP7Client();
            DataFacebookUser dfu = new DataFacebookUser();
            client.FilterSuspectsCompleted += new EventHandler<FilterSuspectsCompletedEventArgs>(client_FilterSuspectsCompleted);
            client.FilterSuspectsAsync(gm.userId, dfu);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);

            client.CloseAsync();
            suspectsList = new List<String>();
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }

        void client_FilterSuspectsCompleted(object sender, FilterSuspectsCompletedEventArgs e)
        {
            throw new NotImplementedException();
        }
    /*
        public void GetProbablySuspectsCallback(object sender, GetProbablySuspectsCompletedEventArgs e)
        {
            
            List<String> list = e.Result.ToList();
            gm.SetSuspectsList(list);
            index = 0;
            if (list.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                Name_Suspect.Text = list.ElementAt(0);
        }
        */
        private void LeftArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	GameManager gm = GameManager.getInstance();
            List<String> suspectsList = gm.GetSuspects();
            if (suspectsList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                if (index == 0)
                    Name_Suspect.Text = suspectsList.ElementAt(0);
                else
                {
                    index--;
                    Name_Suspect.Text = suspectsList.ElementAt(index);
                }
        }

        private void RightArrow1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	GameManager gm = GameManager.getInstance();
            List<String> suspectsList = gm.GetSuspects();
            if (suspectsList.Count == 0)
                Name_Suspect.Text = "There are no suspects";
            else
                if (index == suspectsList.Count - 1)
                    Name_Suspect.Text = suspectsList.ElementAt(suspectsList.Count - 1);
                else
                {
                    index++;
                    Name_Suspect.Text = suspectsList.ElementAt(index);
                }	
        }

      
    }
}