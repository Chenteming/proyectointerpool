namespace WP7
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using System.Xml.Linq;
    using Microsoft.Phone.Controls;

    /// <summary>
    /// Partial class declaration Login
    /// </summary>
    public partial class Login : PhoneApplicationPage
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
        /// Initializes a new instance of the Login class.</summary>
        public Login()
        {
            InitializeComponent();
            if (this.language.GetXDoc() != null)
            {
                this.language.TranslatePage(this);
            }
            else
            {
                this.language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));
                this.language.TranslatePage(this);
            }
        }
        
        private void ContinueButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            ContinueButton.Visibility = Visibility.Collapsed;
            loginMessage.Visibility = Visibility.Collapsed;
            loginImage.Visibility = Visibility.Collapsed;
            this.gm.UserEmail = userEmail.Text;
            userEmail.Visibility = Visibility.Collapsed;
            WebBrowser.Visibility = Visibility.Visible;
            WebBrowser.Source = new Uri("http://pis2010.cloudapp.net", UriKind.Absolute);
        }
    }
}
