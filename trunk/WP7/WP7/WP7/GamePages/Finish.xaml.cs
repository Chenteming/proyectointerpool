
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
    /// Partial class declaration Finish
    /// </summary>
    public partial class Finish : PhoneApplicationPage
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private GameManager gm = GameManager.GetInstance();

        /// <summary>
        /// Initializes a new instance of the Finish class.</summary>
        public Finish()
        {
            InitializeComponent();
            NameSuspectText.Text = this.gm.Data.GameInfo.SuspectName;
            ScoreText.Text = this.gm.Data.GameInfo.Score.ToString();
            TotalText.Text = this.gm.Data.GameInfo.ScoreWin.ToString();
            TimeLeftText.Text = this.gm.Data.GameInfo.DiffInDays.ToString() + ":" + this.gm.Data.GameInfo.DiffInMinutes.ToString() +
                ":" + this.gm.Data.GameInfo.DiffInseconds.ToString();
            NewLevelText.Text = this.gm.Data.GameInfo.newLevel.ToString();
        }
    }
}
