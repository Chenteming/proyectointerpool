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
using UI_wp7.ServiceReference;

namespace UI_wp7
{
    public partial class Sospechoso : PhoneApplicationPage
    {
		private List<String> suspectsList;
		private static int index;
        public Sospechoso()
        {
            InitializeComponent();
            ServiceWP7Client client = new ServiceWP7Client();
            client.GetProbablySuspectsCompleted += new EventHandler<GetProbablySuspectsCompletedEventArgs>(GetProbablySuspectsCallback);
            client.GetProbablySuspectsAsync();
            client.CloseAsync();
			suspectsList = new List<String>();			
        }

        public void GetProbablySuspectsCallback(object sender, GetProbablySuspectsCompletedEventArgs e)
        {
            GameManager gm = GameManager.getInstance();
			List<String> list = e.Result.ToList();
			gm.SetSuspectsList(list);
			index = 0;			
			if (list.Count == 0)
				Name_Suspect.Text = "No hay sospechosos";
			else
				Name_Suspect.Text = list.ElementAt(0);
        }

        private void LeftArrow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			GameManager gm = GameManager.getInstance();
			List<String> suspectsList = gm.GetSuspects();
			if (suspectsList.Count == 0)
				Name_Suspect.Text = "No hay sospechosos";
			else
				if (index == 0)
					Name_Suspect.Text = suspectsList.ElementAt(0);
				else
				{
					index --;
					Name_Suspect.Text = suspectsList.ElementAt(index);
				}	        	
        }

        private void RightArrow_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			GameManager gm = GameManager.getInstance();
			List<String> suspectsList = gm.GetSuspects();
        	if (suspectsList.Count == 0)
				Name_Suspect.Text = "No hay sospechosos";
			else
				if (index == suspectsList.Count - 1)
					Name_Suspect.Text = suspectsList.ElementAt(suspectsList.Count - 1);
				else
				{
					index ++;
					Name_Suspect.Text = suspectsList.ElementAt(index);
				}	     
        }
    }
}