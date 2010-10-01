﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolCloudWebRole.Utilities
{
    public class GameException : Exception
    {
        private string msg;
        
       
        public GameException(string msg)
            : base(msg)
        {
            //TODO internationalize the msg to show 
            this.msg = msg;
        }

        
    }
}