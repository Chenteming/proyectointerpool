//-----------------------------------------------------------------------
// <copyright file="UserState.cs" company="Interpool">
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
    /// Class statement UserState
    /// </summary>
    public class DataUserInfo
    {
        public string UserIdFacebook { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public UserState UserState { get; set; }
    }
}