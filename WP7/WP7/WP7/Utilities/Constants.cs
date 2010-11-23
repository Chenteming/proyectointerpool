namespace WP7.Utilities
{
    using System;
    using System.Net;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Ink;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    /// <summary>
    /// Class Description Constants
    /// </summary>
    public class Constants
    {
        public static int MaxFamous
        {
            get
            {
                return 3;
            }
        }

        public static int MaxCities
        {
            get
            {
                return 3;
            }
        }

        public static int MaxFilterfield
        {
            get
            {
                return 8;
            }
        }
        /*
        //// This is for local test
        /// <summary>
        /// Description for the attribute
        /// </summary>
        public static string FACEBOOK_LOGIN_URL
        {
            get
            {
                return "http://127.0.0.1:81/Pages/FacebookRedirect.aspx";
            }
        }
        */
        //// This is for cloud test
        /// <summary>
        /// Description for the attribute
        /// </summary>
        public static string FACEBOOK_LOGIN_URL
        {
            get
            {
                return "http://pis2010.cloudapp.net/Pages/FacebookRedirect.aspx";
            }
        }
        
        public static string DEFAULT_DOMAIN
        {
            get
            {
                return "@hotmail.com";
            }
        }
    }
}
