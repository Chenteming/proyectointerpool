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

    public partial class Famous : PhoneApplicationPage
    {
        private InterpoolWP7Client client;
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.getInstance();

        public Famous()
        {
            InitializeComponent();
			AnimationPage.Begin();
            //// Change the language of the page            
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);
			////famousImage.Source = "/WP7;component/FamousImages/Lety.jpg";
			this.client = new InterpoolWP7Client();
            this.client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback);
            this.client.GetClueByFamousAsync(gm.UserId, gm.GetCurrentFamous() - 1);
            this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            this.client.CloseAsync();
            this.client = new InterpoolWP7Client();
            this.client.GetCurrentFamousCompleted += new EventHandler<GetCurrentFamousCompletedEventArgs>(this.client_GetCurrentFamousCompleted);
            this.client.GetCurrentFamousAsync(this.gm.UserId, this.gm.GetCurrentFamous() - 1);
            this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            this.client.CloseAsync();
            ////Set the textboxes with the name of the famous
            famousName.Visibility = System.Windows.Visibility.Collapsed;
            Bubble.Visibility = Visibility.Collapsed;
            dialogText.Visibility = Visibility.Collapsed;
        }       
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void GetClueByFamousCallback(object sender, GetClueByFamousCompletedEventArgs e)
        {
            DataClue data = e.Result;
            gm.CurrentDateTime = data.CurrentDate;
            dialogText.Text = data.Clue;
            gm.Data = data;
            switch (data.States)
            {
                case DataClue.State.LOSE_EOAW:
                    NavigationService.Navigate(new Uri("/GamePages/GameOver.xaml?animation =" + 0, UriKind.RelativeOrAbsolute));
                    break;
                case DataClue.State.LOSE_NEOA:
                    NavigationService.Navigate(new Uri("/GamePages/GameOver.xaml?animation =" + 1, UriKind.RelativeOrAbsolute));
                    break;
                case DataClue.State.WIN:
                    NavigationService.Navigate(new Uri("/GamePages/Finish.xaml?", UriKind.RelativeOrAbsolute));
                    break;
                case DataClue.State.LOSE_TO:
                    NavigationService.Navigate(new Uri("/GamePages/GameOver.xaml?animation =" + 2, UriKind.RelativeOrAbsolute));
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
            int num = this.gm.GetCurrentFamous();
            this.gm.AddFamous(num - 1, dataF.NameFamous);
            famousName.Visibility = System.Windows.Visibility.Visible;
            ////Show in the content of the button the name of the famous is going to be interrogated
            famousName.Text = dataF.NameFamous;
            string famousURI = "../FamousImages/" + dataF.FileFamous;
            famousImage.Source = new BitmapImage(new Uri(famousURI, UriKind.Relative));
            Bubble.Visibility = Visibility.Visible;
            dialogText.Visibility = Visibility.Visible;
		}
    }
}
