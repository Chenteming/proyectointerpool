namespace WP7.GamePages
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
    using System.Xml.Linq;
    using Microsoft.Devices;

    public partial class Options : PhoneApplicationPage
    {
        private LanguageManager language = LanguageManager.GetInstance();
        private GameManager gm = GameManager.getInstance();
 
        public Options()
        {
            InitializeComponent();
            if (this.language.GetXDoc() != null)
                this.language.TranslatePage(this);
        }		

		private void spanishCkeckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
            if (englishCheckBox.IsChecked == true)
                englishCheckBox.IsChecked = false;            
			this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
		    this.language.SetCurrentLanguage("Spanish");			
            this.language.TranslatePage(this);
		}

		private void englishCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{			
            if (spanishCheckBox.IsChecked == true)
                spanishCheckBox.IsChecked = false;            
			this.language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
		    this.language.SetCurrentLanguage("English");
			this.language.TranslatePage(this);
		}
		
		private void englishCheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
		{
			this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
		    this.language.SetCurrentLanguage("Spanish");			
            this.language.TranslatePage(this);
		}

		private void spanishCkeckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
		{
			this.language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
			this.language.SetCurrentLanguage("English");
			this.language.TranslatePage(this);
		}
		
		private void vibrationCheckBox_Checked(object sender, System.Windows.RoutedEventArgs e)
		{
            gm.Vibration = true;
            ////Vibration code
			////VibrateController vibrate = VibrateController.Default;
			////vibrate.Start(TimeSpan.FromSeconds(1));
		}

		private void vibrationCheckBox_Unchecked(object sender, System.Windows.RoutedEventArgs e)
		{
			gm.Vibration = false;
		}
    }
}