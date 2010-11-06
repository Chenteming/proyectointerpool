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
    public enum UserState
    {
        /// <summary>
        /// listed field
        /// </summary>
        NO_REGISTERED,

        /// <summary>
        /// listed field
        /// </summary>
        REGISTERED_PLAYING,

        /// <summary>
        /// listed field
        /// </summary>
        REGISTERED_NO_PLAYING,

        /// <summary>
        /// listed field
        /// </summary>
        REGISTERED_NO_PLAYING_LOGIN_REQUIRED
    }
}