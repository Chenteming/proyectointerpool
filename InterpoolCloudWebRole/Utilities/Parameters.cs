//-----------------------------------------------------------------------
// <copyright file="Parameters.cs" company="Interpool">
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
    /// Class statement Parameters
    /// </summary>
    public static class Parameters
    {
        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string LevelRookie
        {
            get
            {
                return "LEVEL_ROOKIE";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string None
        {
            get
            {
                return "none";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string AmountCities
        {
            get
            {
                return "AMOUNT_CITIES";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string LastClue1Esp
        {
            get
            {
                return "LAST_CLUE1_ESP";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string LastClue2Esp
        {
            get
            {
                return "LAST_CLUE2_ESP";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string LastClue3Esp
        {
            get
            {
                return "LAST_CLUE3_ESP";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string MinHoursQuestionFamous
        {
            get
            {
                return "MIN_HOURS_QUESTION_FAMOUS";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string MaxHoursQuestionFamous
        {
            get
            {
                return "MAX_HOURS_QUESTION_FAMOUS";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string MinHoursTravel
        {
            get
            {
                return "MIN_HOURS_TRAVEL";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string MaxHoursTravel
        {
            get
            {
                return "MAX_HOURS_TRAVEL";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string HoursFilterSuspect
        {
            get
            {
                return "HOURS_FILTER_SUSPECT";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string MaxDistance
        {
            get
            {
                return "MAX_DISTANCE";
            }
        }

        /// <summary>
        /// Gets for the property
        /// </summary>
        public static string PreprefixClueContent
        {
            get
            {
                return "PREFIXCLUECONTENT";
            }
        }
        public static string PreprefixClueFamousContent
        {
            get
            {
                return "PREFIXCLUEFAMOUSCONTENT";
            }
        }
        
        public static string DefaultClueContent
        {
            get
            {
                return "DEFAULTCLUECONTENT";
            }

        }

        public static string DefaultFamousClueContent
        {
            get
            {
                return "DEFAULTFAMOUSCLUECONTENT";
            }
        }
    }
}