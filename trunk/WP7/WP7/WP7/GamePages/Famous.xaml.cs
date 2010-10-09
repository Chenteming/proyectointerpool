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
			interrogateButton.Visibility = System.Windows.Visibility.Collapsed;
            famousName.Visibility = System.Windows.Visibility.Collapsed;                     
        }       
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void GetClueByFamousCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            DataClue dc = e.Result;
            dialogText.Text = dc.clue;
            switch (dc.state)
            {
                case DataClue.State.LOSE_EOAW:
                    MessageBox.Show("Haz emitido la orden de arresto de forma incorrecta.");
                    NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case DataClue.State.LOSE_NEOA:
                    MessageBox.Show("No haz emitido una orden de arresto.");
                    NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case DataClue.State.WIN:
                    MessageBox.Show("Ganaste!!!!!!!");
                    NavigationService.Navigate(new Uri("MainPage.xaml", UriKind.RelativeOrAbsolute));
                    break;
                default:
                    break;
            }

        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        void client_GetCurrentFamousCompleted(object sender, GetCurrentFamousCompletedEventArgs e)
        {
            DataFamous dataF = e.Result;
            int num = gm.GetCurrentFamous();
            gm.AddFamous(num-1, dataF.nameFamous);
            famousName.Visibility = System.Windows.Visibility.Visible;
			interrogateButton.Visibility = System.Windows.Visibility.Visible;
            //Show in the content of the button the name of the famous is going to be interrogated
            famousName.Text = dataF.nameFamous;
            string famousURI = "../FamousImages/" + dataF.fileFamous;
            famousImage.Source = new BitmapImage(new Uri(famousURI, UriKind.Relative));
		}
		
        private void interrogateButtonClick(object sender, System.Windows.RoutedEventArgs e)
        {
        	client = new InterpoolWP7Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            client.GetClueByFamousAsync(gm.userId, gm.GetCurrentFamous() - 1);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync(); 
        }
    }
}
