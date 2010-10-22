
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
        public string UserId { get; set; }

        public OAuthFacebook OAuth { get; set; }

        public string IdFriend { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Birthday { get; set; }

        public string Hometown { get; set; }

        public string Gender { get; set; }

        public string Music { get; set; }

        public string Cinema { get; set; }

        public string Television { get; set; }

        public string PictureLink { get; set; }
    }
}