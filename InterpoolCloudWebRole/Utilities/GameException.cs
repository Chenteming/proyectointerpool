
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
               
        public GameException(string msg)
            : base(msg)
        {
            ////TODO internationalize the msg to show 
            this.msg = msg;
        }   
    }
}