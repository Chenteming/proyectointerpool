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
    public partial class Famous : PhoneApplicationPage
    {
        private InterpoolWP7Client client;
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.getInstance();

        public Famous()
        {
            InitializeComponent();
            
            // Change the language of the page            
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
			//famousImage.Source = "/WP7;component/FamousImages/Lety.jpg";
            client = new InterpoolWP7Client();
            client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(client_GetCurrentFamousCompleted);
            client.GetCurrentFamousAsync(gm.userId, gm.GetCurrentFamous() - 1);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            // Set the textboxes with the name of the famous
            famousNameButton.Visibility = System.Windows.Visibility.Collapsed;                     
        }       
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void GetClueByFamousCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            dialogText.Text = clue;           
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        void client_GetCurrentFamousCompleted(object sender, GetCurrentFamousCompletedEventArgs e)
        {
            DataFamous dataF = e.Result;
            int num = gm.GetCurrentFamous();
            gm.AddFamous(num-1, dataF.nameFamous);
            famousNameButton.Visibility = System.Windows.Visibility.Visible;
            //Show in the content of the button the name of the famous is going to be interrogated
            famousNameButton.Content = dataF.nameFamous;
        }

        private void famousNameClick(object sender, RoutedEventArgs e)
        {            
            client = new InterpoolWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            client.GetClueByFamousAsync(gm.userId, gm.GetCurrentFamous() - 1);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync(); 
        }
    }
}
