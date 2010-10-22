
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
        public enum State
        {
            PL,
            LOSE_NEOA,
            LOSE_EOAW,
            LOSE_TO,
            WIN
        }

        public State States { get; set; }

        public string Clue { get; set; }
    }
}