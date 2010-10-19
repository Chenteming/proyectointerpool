
namespace InterpoolCloudWebRole.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    public static class Constants
    {
        public static int NumberLastCity = 4;
        public static int NumberSubLevels = 3;
        public static int MaxLevels = 10;
        public static int MaxSuspects = 10;
        public static int DataRequired = 3;
        public static string RedirectUrlAfterLoginFacebook = "http://127.0.0.1:81/Default.aspx/";
        ////public static string RedirectUrlAfterLoginFacebook = "http://pis2010.cloudapp.net/Default.aspx";

        //for cloud aplication
        //public static string ConsumerKey = "123625261023469";
        //public static string ConsumerSecret = "2ea5107535d2ee3f514a06a186139be6";

        // for local test only
        public static string ConsumerKey = "146049795426501";
        public static string ConsumerSecret = "ea1aab4d4b19644875b4b22a54e17163";
        ////constants used in the search with BING
        public static string AppId = "A00C4105122186E4F9F0DFD82CDF594DD866BC1F";
        public static string Market = "es-Mx";
        public static string RequestVersion = "2.0";
        public static uint NewsOffset = 0;
        public static uint NewsCount = 10;
        ////public static string FacebookCallbackUrl = "http://pis2010.cloudapp.net/Pages/FacebookCallback.aspx/";
        public static string FacebookCallbackUrl = "http://127.0.0.1:81/Pages/FacebookCallback.aspx/";
    }
}