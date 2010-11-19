//-----------------------------------------------------------------------
// <copyright file="Constants.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Class statement Constants
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Gets for the property 
        /// </summary>
        public static string ReplaceNombrePais
        {
            get
            {
                return "\"este país\"";
            }
        }
        
        /// <summary>
        /// Gets for the property 
        /// </summary>
        public static string ReplaceNombreFamoso
        { 
            get
            {
                return "\"Yo\"";
            }
        }
        
        /// <summary>
        /// Gets for the property 
        /// </summary>
        public static string ReplaceNombreCiudad
        { 
            get
            {
                return "\"esta ciudad\"";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int NumberLastCity
        {
            get
            {
                return 4;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int NumberSubLevels
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int MaxLevels
        {
            get
            {
                return 5;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int MaxSuspects
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int DataRequired
        {
            get
            {
                return 3;
            }
        }
        
        //// All this constants are for the cloud application ////
        /*
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string RedirectUrlAfterLoginFacebook
        {
            get
            {
                return "http://pis2010.cloudapp.net/LoginSuccess.aspx/";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string ConsumerKey
        {
            get
            {
                return "123625261023469";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string ConsumerSecret
        {
            get
            {
                return "2ea5107535d2ee3f514a06a186139be6";
            }
        }
        
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string FacebookCallbackUrl
        {
            get
            {
                return "http://pis2010.cloudapp.net/Pages/FacebookCallback.aspx/";
            }
        }
        
        //// End of constants for the cloud application ////
        
        */
        //// All these constants are for local test ////
        
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string RedirectUrlAfterLoginFacebook
        {
            get
            {
                return "http://127.0.0.1:81/LoginSuccess.aspx/";
            }
        }
        
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string ConsumerKey
        {
            get
            {
                return "146049795426501";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string ConsumerSecret
        {
            get
            {
                return "ea1aab4d4b19644875b4b22a54e17163";
            }
        }
        
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string FacebookCallbackUrl
        {
            get
            {
                return "http://127.0.0.1:81/Pages/FacebookCallback.aspx/";
            }
        }
       
        //// End of constants for local test ////
        
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int AmountHardCodeSuspects
        {
            get
            {
                return 10;
            }
        }

        ////constants used in the search with BING

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string AppId
        {
            get
            {
                return "A00C4105122186E4F9F0DFD82CDF594DD866BC1F";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string Market
        {
            get
            {
                return "es-Mx";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string RequestVersion
        {
            get
            {
                return "2.0";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static uint NewsOffset
        {
            get
            {
                return 0;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static uint NewsCount
        {
            get
            {
                return 10;
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string FilterSuspect
        {
            get
            {
                return "FILTER_SUSPECT";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string TravelWrong
        {
            get
            {
                return "TRAVEL_WRONG";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string TravelGood
        {
            get
            {
                return "TRAVEL_GOOD";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string QuestionFamous
        {
            get
            {
                return "QUESTION_FAMOUS";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static int HourWakeUp
        {
            get
            {
                return 8;
            }
        }

        /// <summary>
        /// Gets the property HoursToSleep
        /// </summary>
        public static int HoursToSleep
        {
            get
            {
                return 8;
            }
        }

        /// <summary>
        /// Gets the property PosiblesCities
        /// </summary>
        public static int PosiblesCities
        {
            get
            {
                return 2;
            }
        }

        /// <summary>
        /// Gets the property FamousCities
        /// </summary>
        public static int FamousCities
        {
            get
            {
                return 3;
            }
        }

        /// <summary>
        /// Gets the property GenderMaleES
        /// </summary>
        public static string GenderMaleES
        {
            get
            {
                return "hombre";
            }
        }

        /// <summary>
        /// Gets the property GenderMaleEN
        /// </summary>
        public static string GenderMaleEN
        {
            get
            {
                return "male";
            }
        }

        /// <summary>
        /// Gets the property GenderFemaleES
        /// </summary>
        public static string GenderFemaleES
        {
            get
            {
                return "mujer";
            }
        }

        /// <summary>
        /// Gets the property GenderFemaleEN
        /// </summary>
        public static string GenderFemaleEN
        {
            get
            {
                return "female";
            }
            
        }

        /// <summary>
        /// Gets the property PropertySuspectGender
        /// </summary>
        public static string PropertySuspectGender
        {
            get
            {
                return "SuspectGender";
            }
        } 


    }
}