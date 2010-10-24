//-----------------------------------------------------------------------
// <copyright file="DataGame.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Datatypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary>
    /// Data Game Class
    /// </summary>
    public class DataGame
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public DateTime CurrentDate
        {
            get;
            set;
        }
        
        /// <summary>
        /// Gets or sets for Method.</summary>
        public List<DataFacebookUser> ListSuspect
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DataCity City
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DataClue Clue
        {
            get;
            set;
        }
    }
}