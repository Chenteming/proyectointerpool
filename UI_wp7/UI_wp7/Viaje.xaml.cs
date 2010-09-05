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

namespace UI_wp7
{
    public partial class Viaje : PhoneApplicationPage
    {
        public Viaje()
        {
            InitializeComponent();
        }

        private void SetAndStartViaje(object sender, EventArgs e)
        {
            // Start the storyboard.
            ViajeStoryboard.Begin();
        }

        private void button1_Click_1(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Juego.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}