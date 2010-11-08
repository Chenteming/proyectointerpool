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

    public class DataGameInfo
    {
        public string SuspectName {get; set;}
        public int DiffInDays {get; set;}
        public int DiffInMinutes { get; set; }
        public int DiffInseconds { get; set; }
        public long ScoreWin {get; set;}
        public long Score {get; set;}
        public string LinkBigSuspect {get; set;}
        public string newLevel { get; set; }
        public GameState state { get; set; }

    }
}