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


namespace WP7
{
    public partial class City : PhoneApplicationPage
    {
        private InterpoolWP7Client client;
        private LanguageManager language;
        private GameManager gm = GameManager.getInstance();

        public City()
        {
			// AnimationPage.Begin();
            InitializeComponent();
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);
            client = new InterpoolWP7Client();
            client.GetCitiesCompleted += new EventHandler<GetCitiesCompletedEventArgs>(client_GetCitiesCompleted);
            client.GetCitiesAsync(gm.userId);
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            button1.Visibility = System.Windows.Visibility.Collapsed;
            button2.Visibility = System.Windows.Visibility.Collapsed;
            button3.Visibility = System.Windows.Visibility.Collapsed;
        }

        void client_GetCitiesCompleted(object sender, GetCitiesCompletedEventArgs e)
        {
            List<DataCity> dataCities = e.Result.ToList();
            List<string> cities = new List<string>(); 
            foreach (DataCity dataCity in dataCities)
            {
                cities.Add(dataCity.name_city);
            }
            gm.SetCurrentCities(cities);

            button1.Visibility = System.Windows.Visibility.Visible;
            button2.Visibility = System.Windows.Visibility.Visible;
            button3.Visibility = System.Windows.Visibility.Visible;

            button1.Content = cities.ElementAt(0);
            button2.Content = cities.ElementAt(1);
            button3.Content = cities.ElementAt(2);
        }

        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        void client_TravelCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted += new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button1.Content.ToString());
            gm.SetCurrentCity(button1.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        void client_TravelCompleted(object sender, TravelCompletedEventArgs e)
        {
            //Coords. cities
			double coordX_cityI=165;
			double coordY_cityI=300;
			double coordX_cityE=570;
			double coordY_cityE=120;
			//pregunto si coordX_I < coordY_I, si es true, esta bien asi
			//sino cambiar el scale del X a -1(para q el plane quede mirando para el lado que va)
			
			//Start Frame 1, Position (init_X,init_Y)
			DoubleKeyFrame keyframeX= translateX.KeyFrames[0];
			double init_frame=0.0, init_X=coordX_cityI;
			keyframeX.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame);
			trX1.SetValue(EasingDoubleKeyFrame.ValueProperty,init_X);
			DoubleKeyFrame keyframeY= translateY.KeyFrames[0];
			double init_Y=coordY_cityI;
			keyframeY.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame);
			trY1.SetValue(EasingDoubleKeyFrame.ValueProperty,init_Y);
			
			//Start Frame 2, Position (init_X2,init_Y2)
			DoubleKeyFrame keyframeX2= translateX.KeyFrames[1];
			double init_frame2=3.0, init_X2=coordX_cityI + 40;
			keyframeX2.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame2);
			trX2.SetValue(EasingDoubleKeyFrame.ValueProperty,init_X2);
			DoubleKeyFrame keyframeY2= translateY.KeyFrames[1];
			double init_Y2=coordY_cityI - 45;
			keyframeY2.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame2);
			trY2.SetValue(EasingDoubleKeyFrame.ValueProperty,init_Y2);
			
			//Start Frame 3, Position (init_X3,init_Y3)
			DoubleKeyFrame keyframeX3= translateX.KeyFrames[2];
			double init_frame3=6.0, init_X3=coordX_cityI + 240;
			keyframeX3.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame3);
			trX3.SetValue(EasingDoubleKeyFrame.ValueProperty,init_X3);
			DoubleKeyFrame keyframeY3= translateY.KeyFrames[2];
			double init_Y3=init_Y2;
			keyframeY3.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame3);
			trY3.SetValue(EasingDoubleKeyFrame.ValueProperty,init_Y3);
			
			//Start Frame 4, Position (init_X4,init_Y4)
			DoubleKeyFrame keyframeX4= translateX.KeyFrames[3];
			double init_frame4=9.0, init_X4=coordX_cityE;//coordX_cityI + 380;
			keyframeX4.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame4);
			trX4.SetValue(EasingDoubleKeyFrame.ValueProperty,init_X4);
			DoubleKeyFrame keyframeY4= translateY.KeyFrames[3];
			double init_Y4=coordY_cityE;
			keyframeY4.SetValue(EasingDoubleKeyFrame.ValueProperty,init_frame4);
			trY4.SetValue(EasingDoubleKeyFrame.ValueProperty,init_Y4);
			
			//plane_sound.Play();
			//Start plane animation 			
			animacion2.Begin();
			
			
			//MessageBox.Show("Ha viajado a " + gm.GetCurrentCity());
            //NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted +=new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button2.Content.ToString());
            gm.SetCurrentCity(button2.Content.ToString());
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
        	InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted +=new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(gm.userId, button3.Content.ToString());
            gm.SetCurrentCity(button3.Content.ToString());
			client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
			
        }
    }
}