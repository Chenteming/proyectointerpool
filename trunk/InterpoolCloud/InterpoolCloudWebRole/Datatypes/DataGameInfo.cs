//-----------------------------------------------------------------------
// <copyright file="DataGameInfo.cs" company="Interpool">
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
    /// Class statement DataGameInfo
    /// </summary>
    public class DataGameInfo
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public string SuspectName { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public int DiffInDays { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public int DiffInHours { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public int DiffInMinutes { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public int DiffInseconds { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public long ScoreWin { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public long Score { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string LinkBigSuspect { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string NewLevel { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public GameState State { get; set; }
    }
}