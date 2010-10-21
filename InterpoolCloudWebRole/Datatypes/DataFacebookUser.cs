
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

        public OAuthFacebook oAuth { get; set; }

        public string id_friend { get; set; }

        public string first_name { get; set; }

        public string last_name { get; set; }

        public string birthday { get; set; }

        public string hometown { get; set; }

        public string gender { get; set; }

        public string music { get; set; }

        public string cinema { get; set; }

        public string television { get; set; }

        public string pictureLink { get; set; }
    }
}