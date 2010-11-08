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
    using System.Xml.Linq;
    using Microsoft.Devices;
    using Microsoft.Phone.Controls;

    /// <summary>
    /// Partial class declaration Options
    /// </summary>
    public partial class Options : PhoneApplicationPage
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private LanguageManager language = LanguageManager.GetInstance();

        /// <summary>
        /// Store for the property
        /// </summary>
        private GameManager gm = GameManager.GetInstance();

        /// <summary>
        /// Initializes a new instance of the Options class.</summary>
        public Options()
        {
            InitializeComponent();
            if (this.language.GetXDoc() != null)
            {
                this.language.TranslatePage(this);
            }
        }
        
        private void SpanishCkeckBoxChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (englishCheckBox.IsChecked == true)
            {
                englishCheckBox.IsChecked = false;
            }
            
            this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
            this.language.SetCurrentLanguage("Spanish");
            this.language.TranslatePage(this);
        }
        
        private void EnglishCheckBoxChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            if (spanishCheckBox.IsChecked == true)
            {
                spanishCheckBox.IsChecked = false;
            }
            
            this.language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
            this.language.SetCurrentLanguage("English");
            this.language.TranslatePage(this);
        }
        
        private void EnglishCheckBoxUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
            this.language.SetCurrentLanguage("Spanish");
            this.language.TranslatePage(this);
        }
        
        private void SpanishCkeckBoxUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.language.SetXDoc(XDocument.Load("GameLanguages/English.xml"));
            this.language.SetCurrentLanguage("English");
            this.language.TranslatePage(this);
        }
        
        private void VibrationCheckBoxChecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gm.Vibration = true;
        }
        
        private void VibrationCheckBoxUnchecked(object sender, System.Windows.RoutedEventArgs e)
        {
            this.gm.Vibration = false;
        }
    }
}