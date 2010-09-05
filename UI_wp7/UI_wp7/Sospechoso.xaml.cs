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
        public Sospechoso()
        {
            InitializeComponent();
            Iweb2Client client = new Iweb2Client();
            /*client.GetProbablySuspectsCompleted += new EventHandler<GetProbablySuspectsCompletedEventArgs>(GetProbablySuspectsCallback);
            client.GetProbablySuspectsAsync();
            client.CloseAsync();*/
        }

        private void GetProbablySuspectsCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            SuspectsList.ItemsSource = e.Result;            
        }    
    }
}