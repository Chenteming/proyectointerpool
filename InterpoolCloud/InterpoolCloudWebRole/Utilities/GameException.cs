
namespace InterpoolCloudWebRole.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;

    /// <summary> 
    /// Class statement GameException
    /// </summary>
    public class GameException : Exception
    {
        private string msg;

        /// <summary>
        /// Initializes a new instance of the GameException class.</summary>
        /// <param name="msg"> Parameter description for msg goes here</param>
        public GameException(string msg)
            : base(msg)
        {
            ////TODO internationalize the msg to show 
            this.msg = msg;
        }   
    }
}