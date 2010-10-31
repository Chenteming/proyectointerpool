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
using System.Xml.Linq;

namespace WP7.GamePages
{
    public partial class Options : PhoneApplicationPage
    {
        private LanguageManager language = LanguageManager.GetInstance();
 
        public Options()
        {
            InitializeComponent();
        }		

		private void spanishCkeckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			englishCheckBox.IsEnabled = false;
			this.language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
		    this.language.SetCurrentLanguage("English");			
            this.language.TranslatePage(this);
		}

		private void englishCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
			spanishCheckBox.IsEnabled = false;
			this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
		    this.language.SetCurrentLanguage("Spanish");
			this.language.TranslatePage(this);
		}    
    }
}