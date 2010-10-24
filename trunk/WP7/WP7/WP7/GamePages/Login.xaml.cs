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
using System.Text.RegularExpressions;

namespace WP7
{
    public partial class Login : PhoneApplicationPage
    {
		private LanguageManager language = LanguageManager.GetInstance();
		private GameManager gm = GameManager.getInstance();
        public Login()
        {
            InitializeComponent();
			if (language.GetXDoc() != null)
			{
                language.TranslatePage(this);     
			}
			else
			{
				language.SetXDoc(XDocument.Load("GameLanguages/Spanish.xml"));	
				language.TranslatePage(this);
			}			
        }
		
        private void ContinueButton_Click(object sender, System.Windows.RoutedEventArgs e)
        {
			/*if (userEmail.Text.Trim() != "")
            {
                Match rex = Regex.Match(userEmail.Text.Trim(' '), "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.) + [a-zA-Z]{2,3}$)", RegexOptions.IgnoreCase);
                if (rex.Success == false)                
                    MessageBox.Show("Please Enter a valid Email-Address ");
            }
            else
            {*/               
				ContinueButton.Visibility = Visibility.Collapsed;
				loginMessage.Visibility = Visibility.Collapsed;
				loginImage.Visibility = Visibility.Collapsed;
				gm.userEmail = userEmail.Text;
				userEmail.Visibility = Visibility.Collapsed;				
				WebBrowser.Visibility = Visibility.Visible;			
				WebBrowser.Source = new Uri("http://pis2010.cloudapp.net", UriKind.Absolute);
           //}

			
        }
    }
}
