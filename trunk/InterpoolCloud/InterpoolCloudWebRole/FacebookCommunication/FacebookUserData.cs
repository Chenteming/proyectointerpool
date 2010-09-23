using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace InterpoolCloudWebRole.FacebookCommunication
{
    public class FacebookUserData
    {
       // private string userId;
        //private oAuthFacebook oAut;
        //private string nombre;

        public string userId { get; set; }
        public oAuthFacebook oAuth { get; set; }
        public string nombre { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string birthday { get; set; }
        public string hometown { get; set; }
        public string gender { get; set; }
        public string likes { get; set; }
        public string id_friend { get; set; }

    }
}