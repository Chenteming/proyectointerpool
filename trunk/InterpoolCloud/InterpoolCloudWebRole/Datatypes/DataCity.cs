//-----------------------------------------------------------------------
// <copyright file="DataCity.cs" company="Interpool">
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
    /// Class statement DataCity
    /// </summary>
    public class DataCity
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public string NameCity { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string NameFileCity { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public double Top { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public double Left { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public double GameTime { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DateTime CurrentDate
        {
            get;
            set;
        }
    }
}