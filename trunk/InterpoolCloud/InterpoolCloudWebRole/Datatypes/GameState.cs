//-----------------------------------------------------------------------
// <copyright file="GameState.cs" company="Interpool">
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
    /// Class statement GameState
    /// </summary>
    public enum GameState
    {
        /// <summary>
        /// listed field
        /// </summary>
        PL,

        /// <summary>
        /// listed field
        /// </summary>
        LOSE_NEOA,

        /// <summary>
        /// listed field
        /// </summary>
        LOSE_EOAW,

        /// <summary>
        /// listed field
        /// </summary>
        LOSE_TO,

        /// <summary>
        /// listed field
        /// </summary>
        WIN
    }
}