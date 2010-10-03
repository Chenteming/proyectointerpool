using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolCloudWebRole.Datatypes
{
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