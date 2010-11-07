
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

        /// <summary>
        /// Initializes a new instance of the GameOver class.</summary>
        public GameOver()
        {            
            InitializeComponent();
            incorrectOrderOfArrest.Visibility = Visibility.Collapsed;
            didNotEmitOrderOfArrest.Visibility = Visibility.Collapsed;
            afterDeadLine.Visibility = Visibility.Collapsed;                            
            gameOverStoryboard.Begin();
            gameOverStoryboard.Completed += new EventHandler(this.GameOverStoryboardCompleted);
        }

        public void GameOverStoryboardCompleted(object sender, EventArgs e)
        {
            ScoreText.Text = this.gm.Data.GameInfo.Score.ToString();
            TotalText.Text = this.gm.Data.GameInfo.ScoreWin.ToString();
            TimeLeftText.Text = this.gm.Data.GameInfo.DiffInDays.ToString() + ":" + this.gm.Data.GameInfo.DiffInMinutes.ToString() + 
                ":" + this.gm.Data.GameInfo.DiffInseconds.ToString();
            NewLevelText.Text = this.gm.Data.GameInfo.newLevel.ToString();
            switch (this.animation)
            {
                case 0:
                    incorrectOrderOfArrest.Visibility = Visibility.Visible;
                    break;
                case 1:
                    didNotEmitOrderOfArrest.Visibility = Visibility.Visible;
                    break;
                case 2:
                    afterDeadLine.Visibility = Visibility.Visible;
                    break;
                default:
                    break;
            }            
        }
    }
}
