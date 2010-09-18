﻿using System;
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
using System.Xml.Linq;

namespace WP7
{
    public partial class MainPage : PhoneApplicationPage
    {
        private ServiceWP7Client client;
        private LanguageManager language;

        public MainPage()
        {
            InitializeComponent();
            language = LanguageManager.GetInstance();
            if (language.GetXDoc() != null)
                language.TranslatePage(this);               
            client = new ServiceWP7Client();
            client.StartGameCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_StartGameCompleted);
            client.StartGameAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            GameManager gm = GameManager.getInstance();
            client = new ServiceWP7Client();
            client.GetCurrentCityCompleted += new EventHandler<GetCurrentCityCompletedEventArgs>(GetCurrentCityCallback);
            client.GetCurrentCityAsync();
            client.CloseCompleted += new EventHandler<System.ComponentModel.AsyncCompletedEventArgs>(client_CloseCompleted);
            client.CloseAsync();
            NavigationService.Navigate(new Uri("/GamePages/Game.xaml", UriKind.RelativeOrAbsolute));
        }

		// Asynchronous callbacks for displaying results.
        void client_CloseCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }

        void client_StartGameCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
        {

        }
        
        static void GetCurrentCityCallback(object sender, GetCurrentCityCompletedEventArgs e)
        {
            String initialCity = e.Result;
            GameManager gm = GameManager.getInstance();
            gm.SetCurrentCity(initialCity);
        }

        private void Setting_Click(object sender, RoutedEventArgs e)
        {
            language.SetCurrentLanguage("English");
            language.SetCurrentLanguage("Spanish");
            String current = language.GetCurrentLanguage();
            if (current.Equals("English"))
                language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
            else
                language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
            language.TranslatePage(this);
        }
    }
}