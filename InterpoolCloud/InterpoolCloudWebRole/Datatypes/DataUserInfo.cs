//-----------------------------------------------------------------------
// <copyright file="DataUserInfo.cs" company="Interpool">
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
        /// <summary>
        /// Gets or sets for Method.</summary>
        public string UserIdFacebook { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public UserState UserState { get; set; }
    }
}