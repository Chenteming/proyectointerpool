
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
    using Microsoft.Phone.Tasks;

    /// <summary>
    /// Partial class declaration Finish
    /// </summary>
    public partial class Finish : PhoneApplicationPage
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private GameManager gm = GameManager.GetInstance();
        private LanguageManager lm = LanguageManager.GetInstance();

        bool first;

        /// <summary>
        /// Initializes a new instance of the Finish class.</summary>
        public Finish()
        {
            InitializeComponent();
            if (this.lm.GetXDoc() != null)
                this.lm.TranslatePage(this);            
            gm.OrderArrest = null;
            WinText.Visibility = Visibility.Visible;
            WinMessageStoryboard.Begin();
            WinMessageStoryboard.Completed += new EventHandler(WinMessageStoryboard_Completed);
        }

        void WinMessageStoryboard_Completed(object sender, EventArgs e)
        {
            if (gm.Info.newLevel != gm.CurrentLevel)
            {
                NewLevelTB.Visibility = Visibility.Visible;                
                NewLevelStoryboard.Begin();
                NewLevelStoryboard.Completed += new EventHandler(NewLevelStoryboard_Completed);
            }
            else
            {
                ShowInfoUser();
            }
        }

        void NewLevelStoryboard_Completed(object sender, EventArgs e)
        {
            NewLevelText.Visibility = Visibility.Visible;
            NewLevelText.Text = this.gm.Info.newLevel.ToString();
            BigSuspectButton.Visibility = System.Windows.Visibility.Visible;
            ShowInfoUser();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }

        private void BigSuspectButton_Click(object sender, RoutedEventArgs e)
        {
            WebBrowserTask task = new WebBrowserTask();
            task.URL = gm.Info.LinkBigSuspect;
            task.Show();
        }

        public void ShowInfoUser()
        {
            NameSuspectTB.Visibility = Visibility.Visible;
            NameSuspectText.Visibility = Visibility.Visible;
            ScoreTB.Visibility = Visibility.Visible;
            ScoreText.Visibility = Visibility.Visible;
            TimeLeftTB.Visibility = Visibility.Visible;
            TimeLeftText.Visibility = Visibility.Visible;
            TotalTB.Visibility = Visibility.Visible;
            TotalText.Visibility = Visibility.Visible;
            NameSuspectText.Text = this.gm.Info.SuspectName;
            ScoreText.Text = this.gm.Info.Score.ToString();
            
            
            TimeLeftText.Text = this.gm.Info.DiffInDays.ToString() +" "+ this.gm.Info.DiffInHours+ ":" + this.gm.Info.DiffInMinutes.ToString() +
                ":" + this.gm.Info.DiffInseconds.ToString();
            TotalText.Text = this.gm.Info.ScoreWin.ToString();            
        }
    }
}

