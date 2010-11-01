//-----------------------------------------------------------------------
// <copyright file="DataFacebookUser.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Datatypes
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// Class statement DataFacebookUser
    /// </summary>
    public class DataFacebookUser
    {
        /// <summary>
        /// Gets or sets for Method.</summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public OAuthFacebook OAuth { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string IdFriend { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Birthday { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Hometown { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Music { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Cinema { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string Television { get; set; }

        /// <summary>
        /// Gets or sets for Method.</summary>
        public string PictureLink { get; set; }
    }
}