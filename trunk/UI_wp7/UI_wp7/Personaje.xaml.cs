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
    public partial class Personaje : PhoneApplicationPage
    {
        public Personaje()
        {
            InitializeComponent();
            GameManager gm = GameManager.getInstance();
            List<String> famous = gm.GetFamous();
            List<String> clues = gm.GetClues();
            //Show in the textBoxes the name of the famous
            textBox1.Text = famous.ElementAt(0);
            textBox2.Text = famous.ElementAt(1);
            textBox3.Text = famous.ElementAt(2);
        }
		
		public void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
		{
		}

        private void Famous1_Click(object sender, RoutedEventArgs e)
        {
            //Get the clue
            Iweb2Client client = new Iweb2Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback_1);
            client.GetClueByFamousAsync(textBox1.Text);
            client.CloseAsync();
        }

        private void Famous2_Click(object sender, RoutedEventArgs e)
        {
            //Get the clue
            Iweb2Client client = new Iweb2Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback_2);
            client.GetClueByFamousAsync(textBox2.Text);
            client.CloseAsync();
        }

        private void Famous3_Click(object sender, RoutedEventArgs e)
        {
            //Get the clue
            Iweb2Client client = new Iweb2Client();
            client.GetClueByFamousCompleted += new EventHandler<GetClueByFamousCompletedEventArgs>(GetClueByFamousCallback_3);
            client.GetClueByFamousAsync(textBox3.Text);
            client.CloseAsync();
        }

        private void GetClueByFamousCallback_1(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            textBox4.Text = clue;
            GameManager gm = GameManager.getInstance();
            gm.AddClue(0, clue);
        }

        private void GetClueByFamousCallback_2(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            textBox5.Text = clue;
            GameManager gm = GameManager.getInstance();
            gm.AddClue(1, clue);
        }

        private void GetClueByFamousCallback_3(object sender, GetClueByFamousCompletedEventArgs e)
        {
            String clue = e.Result;
            textBox6.Text = clue;
            GameManager gm = GameManager.getInstance();
            gm.AddClue(2, clue);
        }        
    }
}