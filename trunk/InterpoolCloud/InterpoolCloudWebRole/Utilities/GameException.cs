//-----------------------------------------------------------------------
// <copyright file="GameException.cs" company="Interpool">
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
    /// Class statement GameException
    /// </summary>
    public class GameException : Exception
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private string msg;

        /// <summary>
        /// Initializes a new instance of the GameException class.</summary>
        /// <param name="msg"> Parameter description for msg goes here</param>
        public GameException(string msg, Exception innerException)
            : base(msg, innerException)
        {
            ////TODO internationalize the msg to show 
            this.msg = msg;
        }   
    }
}