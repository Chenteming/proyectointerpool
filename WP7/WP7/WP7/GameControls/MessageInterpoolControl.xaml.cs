namespace WP7
{
    using System;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;
    using Microsoft.Phone.Tasks;
    using System.Windows.Navigation;

    public partial class MessageInterpoolControl : UserControl
    {
        private bool accept = false;
        private bool cancel = false;
        private bool login = false;

		public MessageInterpoolControl()
		{
			// Required to initialize variables
			InitializeComponent();			
			acceptButton.Visibility = Visibility.Collapsed;
			cancelButton.Visibility = Visibility.Collapsed;
			message.Visibility = Visibility.Collapsed;
			MailIcon.Visibility = Visibility.Collapsed;
			MailText.Visibility = Visibility.Collapsed;
			MessageInterpoolStoryboard.Begin();
		}

		public void ShowMessageInterpool(string msg, bool acceptBtn, bool cancelBtn, string type)
		{
			if (acceptBtn)
				acceptButton.Visibility = Visibility.Visible;
			if (cancelBtn)
                cancelButton.Visibility = Visibility.Visible;
			if (type == "messageCity")
			{
				message.Visibility = Visibility.Visible;
                message.Text = msg;
				MailIcon.Visibility = Visibility.Collapsed;
				MailText.Visibility = Visibility.Collapsed;
			}				
			if (type == "login")
			{
				message.Visibility = Visibility.Collapsed;
				MailIcon.Visibility = Visibility.Visible;
				MailText.Visibility = Visibility.Visible;
                login = true;
			}
		}

		private void acceptButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.accept = true;
			this.cancel = false;			
			acceptButton.Visibility = Visibility.Collapsed;
			cancelButton.Visibility = Visibility.Collapsed;
			this.Visibility = Visibility.Collapsed;
            if (login)
            {
                /*WebBrowserTask task = new WebBrowserTask();
                task.URL = "http://google.com";
                task.Show();*/
                GameManager gm = GameManager.GetInstance();
                gm.Logged = true;
                login = false;                
            }
		}

        private void cancelButton_Click(object sender, System.Windows.RoutedEventArgs e)
		{
			this.accept = false;
			this.cancel = true;			
			acceptButton.Visibility = Visibility.Collapsed;
			cancelButton.Visibility = Visibility.Collapsed;
			this.Visibility = Visibility.Collapsed;
		}

		public bool MessageAccept()
		{
			return this.accept;
		}
		
		public bool MessageCancel()
		{
			return this.cancel;
		}
	}
}