
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
            PL,
            LOSE_NEOA,
            LOSE_EOAW,
            LOSE_TO,
            WIN
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public State States { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Clue { get; set; }
    }
}