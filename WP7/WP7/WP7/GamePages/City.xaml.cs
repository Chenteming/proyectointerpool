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
    using WP7.Utilities;

    public partial class City : PhoneApplicationPage
    {
        private InterpoolWP7Client client;
        private LanguageManager language;
        private GameManager gm = GameManager.GetInstance();
        private double[] coordX;
        private double[] coordY;
        private int currentTravel;
        private string nameCity;

        public City()
        {
            InitializeComponent();
            this.coordX = new double[Constants.MaxCities];
            this.coordY = new double[Constants.MaxCities];
			AnimationPage.Begin();
            this.language = LanguageManager.GetInstance();
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);
            this.client = new InterpoolWP7Client();
            this.client.GetCitiesCompleted += new EventHandler<GetCitiesCompletedEventArgs>(this.client_GetCitiesCompleted);
            this.client.GetCitiesAsync(this.gm.UserId);
            this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            this.client.CloseAsync();	
        }

        void client_GetCitiesCompleted(object sender, GetCitiesCompletedEventArgs e)
        {
            if (e.Result == null)
            {
                textToTravel.Visibility = Visibility.Collapsed;
                button1.Visibility = Visibility.Collapsed;
                button2.Visibility = Visibility.Collapsed;
                button3.Visibility = Visibility.Collapsed;
                
            }
            else
            {
                List<DataCity> dataCities = e.Result.ToList();
                if (dataCities != null)
                {
                    button1.Content = dataCities.ElementAt(0).NameCity;
                    button2.Content = dataCities.ElementAt(1).NameCity;
                    button3.Content = dataCities.ElementAt(2).NameCity;
                    for (int i = 0; i < dataCities.Count; i++)
                    {
                        coordX[i] = dataCities.ElementAt(i).Left;
                        coordY[i] = dataCities.ElementAt(i).Top;
                    }
                    InitPoint.Visibility = Visibility.Visible;
                    YellowPoint.Visibility = Visibility.Visible;
                    BluePoint.Visibility = Visibility.Visible;
                    GreenPoint.Visibility = Visibility.Visible;
					////load images button cities
					////initial city
                    Thickness margin = InitPoint.Margin;
            		margin.Left = gm.Left;
            		margin.Top = gm.Top;
            		InitPoint.Margin = margin;
                    //city button1
                    Thickness margin1 = YellowPoint.Margin;
                    margin1.Left = coordX[0];
                    margin1.Top = coordY[0];
                    YellowPoint.Margin = margin1;
                    //city button2
                    Thickness margin2 = BluePoint.Margin;
                    margin2.Left = coordX[1];
                    margin2.Top = coordY[1];
                    BluePoint.Margin = margin2;
                    //city button3
                    Thickness margin3 = GreenPoint.Margin;
                    margin3.Left = coordX[2];
                    margin3.Top = coordY[2];
                    GreenPoint.Margin = margin3; 

                    ShowContext();
                }
            }
        }       

        void client_TravelCompleted(object sender, TravelCompletedEventArgs e)
        {
            DataCity city = e.Result;
            gm.CurrentDateTime = city.CurrentDate;
            gm.Info = city.GameInfo;

            if (city.CityNumber == -1)
            {
                ShowHideInterpoolFailMessage(true);
                if (city.GameInfo.state == GameState.LOSE_TO)
                {
                    gm.Info = city.GameInfo;
                    NavigationService.Navigate(new Uri("/GamePages/GameOver.xaml", UriKind.RelativeOrAbsolute));
                    return;
                }
            }
            else
            {

                gm.ResetCurrentFamous();
                if (city.GameInfo.state == GameState.LOSE_TO)
                {
                    gm.Info = city.GameInfo;
                    NavigationService.Navigate(new Uri("/GamePages/GameOver.xaml", UriKind.RelativeOrAbsolute));
                    return;
                }
                ////Coords. cities



                double coordX_cityI = gm.Left;
                double coordY_cityI = gm.Top;
                double coordX_cityE = coordX[currentTravel];
                double coordY_cityE = coordY[currentTravel];
                gm.PictureCityLink = city.NameFileCity;
                gm.SetCurrentCity(nameCity);
                int coef = 1;
                double sc1 = 1.0;
                double sc2 = 7.4;
                double sc3 = 7.4;
                double sc4 = 0.396;
                ////pregunto si coordX_I < coordY_I, si es true, esta bien asi
                ////sino cambiar el scale del X a -1(para q el plane quede mirando para el lado que va)
                if (coordX_cityI > coordX_cityE)
                {
                    coef = -1;

                    ////avion.RenderTransform.SetValue(CompositeTransform.ScaleXProperty, -5.0);
                }
                ////Start Frame 1, Position (init_X,init_Y)
                DoubleKeyFrame keyframeX = translateX.KeyFrames[0];
                double init_frame = 0.0, init_X = coordX_cityI;
                keyframeX.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame);
                trX1.SetValue(EasingDoubleKeyFrame.ValueProperty, init_X);
                DoubleKeyFrame keyframeY = translateY.KeyFrames[0];
                double init_Y = coordY_cityI;
                keyframeY.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame);
                trY1.SetValue(EasingDoubleKeyFrame.ValueProperty, init_Y);
                DoubleKeyFrame keyframeScaleX = scaleX.KeyFrames[0];
                keyframeScaleX.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame);
                scX1.SetValue(EasingDoubleKeyFrame.ValueProperty, coef * sc1);

                ////Start Frame 2, Position (init_X2,init_Y2)
                DoubleKeyFrame keyframeX2 = translateX.KeyFrames[1];
                double init_frame2 = 3.0, init_X2 = coordX_cityI + coef * 40;
                keyframeX2.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame2);
                trX2.SetValue(EasingDoubleKeyFrame.ValueProperty, init_X2);
                DoubleKeyFrame keyframeY2 = translateY.KeyFrames[1];
                double init_Y2 = coordY_cityI - 45;
                keyframeY2.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame2);
                trY2.SetValue(EasingDoubleKeyFrame.ValueProperty, init_Y2);
                DoubleKeyFrame keyframeScaleX2 = scaleX.KeyFrames[1];
                keyframeScaleX2.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame2);
                scX2.SetValue(EasingDoubleKeyFrame.ValueProperty, coef * sc2);

                ////Start Frame 3, Position (init_X3,init_Y3)
                DoubleKeyFrame keyframeX3 = translateX.KeyFrames[2];
                double init_frame3 = 6.0, init_X3 = coordX_cityI + coef * 240;
                keyframeX3.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame3);
                trX3.SetValue(EasingDoubleKeyFrame.ValueProperty, init_X3);
                DoubleKeyFrame keyframeY3 = translateY.KeyFrames[2];
                double init_Y3 = init_Y2;
                keyframeY3.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame3);
                trY3.SetValue(EasingDoubleKeyFrame.ValueProperty, init_Y3);
                DoubleKeyFrame keyframeScaleX3 = scaleX.KeyFrames[2];
                keyframeScaleX3.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame3);
                scX3.SetValue(EasingDoubleKeyFrame.ValueProperty, coef * sc3);

                ////Start Frame 4, Position (init_X4,init_Y4)
                DoubleKeyFrame keyframeX4 = translateX.KeyFrames[3];
                double init_frame4 = 9.0, init_X4 = coordX_cityE;
                ////coordX_cityI + 380;
                keyframeX4.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame4);
                trX4.SetValue(EasingDoubleKeyFrame.ValueProperty, init_X4);
                DoubleKeyFrame keyframeY4 = translateY.KeyFrames[3];
                double init_Y4 = coordY_cityE;
                keyframeY4.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame4);
                trY4.SetValue(EasingDoubleKeyFrame.ValueProperty, init_Y4);
                DoubleKeyFrame keyframeScaleX4 = scaleX.KeyFrames[3];
                keyframeScaleX4.SetValue(EasingDoubleKeyFrame.ValueProperty, init_frame4);
                scX4.SetValue(EasingDoubleKeyFrame.ValueProperty, coef * sc4);

                plane_sound.Play();
                ////Start plane animation 			
                animacion2.Begin();
                animacion2.Completed += new EventHandler(animacion2_Completed);
                gm.Left = coordX[currentTravel];
                gm.Top = coordY[currentTravel];
                gm.ShowAnimation = (city.CityNumber == 3);
            }       
        }
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {
        }

        void animacion2_Completed(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void button1_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentTravel = 0;
            nameCity = button1.Content.ToString();
            EditContext(false);
            this.ShowHideInterpoolMessage(button1.Content.ToString(), true);
        }

        private void button2_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentTravel = 1;
            nameCity = button2.Content.ToString();
            EditContext(false);
            this.ShowHideInterpoolMessage(button2.Content.ToString(), true);
        }

        private void button3_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            currentTravel = 2;
            nameCity = button3.Content.ToString();
            EditContext(false);
            this.ShowHideInterpoolMessage(button3.Content.ToString(), true);
        }

        private void travel1()
        {
            InterpoolWP7Client client = new InterpoolWP7Client();
            client.TravelCompleted += new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
            client.TravelAsync(this.gm.UserId, button1.Content.ToString());
            
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(this.client_CloseCompleted);
            client.CloseAsync();
        }

        private void travel2()
        {        
                InterpoolWP7Client client = new InterpoolWP7Client();
                client.TravelCompleted += new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
                client.TravelAsync(this.gm.UserId, button2.Content.ToString());
                
                this.client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
                client.CloseAsync();            
        }

        private void travel3()
        {	               
                InterpoolWP7Client client = new InterpoolWP7Client();
                client.TravelCompleted += new EventHandler<TravelCompletedEventArgs>(client_TravelCompleted);
                client.TravelAsync(this.gm.UserId, button3.Content.ToString());
                
                client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
                client.CloseAsync();
        }

        private void NoButton_Click(object sender, RoutedEventArgs e)
        {
            EditContext(true);
            ShowHideInterpoolMessage("", false);
        }

        private void YesButton_Click(object sender, RoutedEventArgs e)
        {
            ShowHideInterpoolMessage("", false);
            EditContext(false);
            switch (currentTravel)
            {
                case 0:
                    travel1();
                    break;
                case 1:
                    travel2();
                    break;
                case 2:
                    travel3();
                    break;
                default:
                    break;
            }
        }

        private void YesFailButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ShowHideInterpoolFailMessage(false);
            EditContext(true);
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

        private void ShowHideInterpoolMessage(string message, bool flag)
        {
            MessageImage.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            messageText.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            if (flag)
                messageText.Text = messageText2.Text + message + "?";
            YesButton.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            NoButton.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;            
        }        

        private void ShowHideInterpoolFailMessage(bool flag)
        {
            failMessageText.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            MessageImage.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;
            YesFailButton.Visibility = (flag == true) ? Visibility.Visible : Visibility.Collapsed;            
        }

        private void ShowContext()
        {
            textToTravel.Visibility = Visibility.Visible;
            button1.Visibility = Visibility.Visible;
            button2.Visibility = Visibility.Visible;
            button3.Visibility = Visibility.Visible;
        }

        private void EditContext(bool enabled)
        {
            button1.IsEnabled = enabled;
            button2.IsEnabled = enabled;
            button3.IsEnabled = enabled;
        }

        protected override void OnBackKeyPress(System.ComponentModel.CancelEventArgs e)
        {
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }
    }
}