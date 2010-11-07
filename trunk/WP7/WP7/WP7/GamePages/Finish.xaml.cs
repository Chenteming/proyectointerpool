
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
            NameSuspectText.Text = this.gm.Info.SuspectName;
            ScoreText.Text = this.gm.Info.Score.ToString();
            TotalText.Text = this.gm.Info.ScoreWin.ToString();
            TimeLeftText.Text = this.gm.Info.DiffInDays.ToString() + ":" +this.gm.Info.DiffInMinutes.ToString() +
                ":" + this.gm.Info.DiffInseconds.ToString();
            NewLevelText.Text = this.gm.Info.newLevel.ToString();
        }
    }
}
