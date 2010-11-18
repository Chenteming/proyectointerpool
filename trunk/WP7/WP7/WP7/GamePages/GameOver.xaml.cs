
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

    /// <summary>
    /// Partial class declaration GameOver
    /// </summary>
    public partial class GameOver : PhoneApplicationPage
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private int animation = 0;

        /// <summary>
        /// Store for the property
        /// </summary>
        private GameManager gm = GameManager.GetInstance();
        private LanguageManager lm = LanguageManager.GetInstance();

        /// <summary>
        /// Initializes a new instance of the GameOver class.</summary>
        public GameOver()
        {
            
            InitializeComponent();
            if (this.lm.GetXDoc() != null)
                this.lm.TranslatePage(this);            
            gm.OrderArrest = null;
            LoseText.Visibility = Visibility.Visible;
            incorrectOrderOfArrest.Visibility = Visibility.Collapsed;
            didNotEmitOrderOfArrest.Visibility = Visibility.Collapsed;
            afterDeadLine.Visibility = Visibility.Collapsed;                            
            gameOverStoryboard.Begin();
            gameOverStoryboard.Completed += new EventHandler(this.GameOverStoryboardCompleted);
        }

        public void GameOverStoryboardCompleted(object sender, EventArgs e)
        {
            LoseStoryboard.Begin();
            LoseStoryboard.Completed += new EventHandler(LoseStoryboard_Completed);                        
        }

        void LoseStoryboard_Completed(object sender, EventArgs e)
        {            
            switch (this.gm.Info.State)
            {
                case GameState.LOSE_EOAW:
                    incorrectOrderOfArrest.Visibility = Visibility.Visible;
                    break;
                case GameState.LOSE_NEOA:
                    didNotEmitOrderOfArrest.Visibility = Visibility.Visible;
                    break;
                case GameState.LOSE_TO:
                    afterDeadLine.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }            
            NewLevelTB.Visibility = Visibility.Visible;
            NewLevelText.Visibility = Visibility.Visible;
            TotalText.Visibility = Visibility.Visible;
            TotalTB.Visibility = Visibility.Visible;
            TotalText.Text = this.gm.Info.ScoreWin.ToString();
            NewLevelText.Text = this.gm.Info.NewLevel.ToString();
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/MainPage.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}
