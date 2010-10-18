
namespace InterpoolCloudWebRole.Datatypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

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

        public State state { get; set; }

        public string clue { get; set; }
    }
}