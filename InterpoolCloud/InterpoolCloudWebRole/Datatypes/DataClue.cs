//-----------------------------------------------------------------------
// <copyright file="DataClue.cs" company="Interpool">
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
    /// Class statement DataClue
    /// </summary>
    public class DataClue
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public GameState States { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Clue { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DateTime CurrentDate
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DataGameInfo GameInfo
        {
            get;
            set;
        }
    }
}