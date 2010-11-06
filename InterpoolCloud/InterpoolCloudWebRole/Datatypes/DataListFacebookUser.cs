//-----------------------------------------------------------------------
// <copyright file="DataListFacebookUser.cs" company="Interpool">
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
    /// Data List facebook user
    /// </summary>
    public class DataListFacebookUser
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public List<DataFacebookUser> ListFacebookUser
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public DateTime CurrentDate
        {
            get;
            set;
        }

        public DataGameInfo GameInfo
        {
            get;
            set;
        }
    }
}