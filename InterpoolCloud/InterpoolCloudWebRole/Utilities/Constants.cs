using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolCloudWebRole.Utilities
{
    public static class Constants
    {
        public static int NUMBERLASTCITY = 4;
        public static int NUMBER_SUB_LEVELS = 3;
        public static int MAX_LEVELS = 10;
        public static int MAX_SUSPECTS = 10;
        public static string REDIRECT_URL_AFTER_LOGIN_FACEBOOK = "http://127.0.0.1:81/Default.aspx/";
        // public static string REDIRECT_URL_AFTER_LOGIN_FACEBOOK = "http://pis2010.cloudapp.net/Default.aspx";

        // for cloud aplication
        //public static string CONSUMER_KEY = "123625261023469";
        //public static string CONSUMER_SECRET = "2ea5107535d2ee3f514a06a186139be6";

        // for local test only
        public static string CONSUMER_KEY = "146049795426501";
        public static string CONSUMER_SECRET = "ea1aab4d4b19644875b4b22a54e17163";
        //constants used in the search with BING
        public static string APPID = "A00C4105122186E4F9F0DFD82CDF594DD866BC1F";
        public static string MARKET = "es-Mx";
        public static string REQUEST_VERSION = "2.0";
        public static uint NEWS_OFFSET = 0;
        public static uint NEWS_COUNT = 10;
  


    }
}