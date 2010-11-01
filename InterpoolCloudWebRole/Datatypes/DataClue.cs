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
        /// Class statement DataClue
        /// </summary>
        public enum State
        {
            /// <summary>
            /// listed field
            /// </summary>
            PL,

            /// <summary>
            /// listed field
            /// </summary>
            LOSE_NEOA,

            /// <summary>
            /// listed field
            /// </summary>
            LOSE_EOAW,

            /// <summary>
            /// listed field
            /// </summary>
            LOSE_TO,

            /// <summary>
            /// listed field
            /// </summary>
            WIN
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public State States { get; set; }

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
    }
}