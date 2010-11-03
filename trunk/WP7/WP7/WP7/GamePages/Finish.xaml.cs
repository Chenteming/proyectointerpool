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

namespace WP7
{
    public partial class Finish : PhoneApplicationPage
    {
        private GameManager gm = GameManager.getInstance();

        public Finish()
        {
            InitializeComponent();
            NameSuspectText.Text = gm.Data.GameInfo.SuspectName;
            ScoreText.Text = gm.Data.GameInfo.Score.ToString();
            TotalText.Text = gm.Data.GameInfo.ScoreWin.ToString();
            TimeLeftText.Text = gm.Data.GameInfo.DiffInDays.ToString() + ":" + gm.Data.GameInfo.DiffInMinutes.ToString() +
                ":" + gm.Data.GameInfo.DiffInseconds.ToString();
            NewLevelText.Text = gm.Data.GameInfo.newLevel.ToString();
        }
    }
}
