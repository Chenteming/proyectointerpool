using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace InterpoolPrototypeWebRole.FacebookComunication
{
    public class FacebookController : IFacebookController
    {
        public string GetUserId(oAuthFacebook oAuth)
        {
            String url = String.Format("https://graph.facebook.com/me?access_token={0}",
                    oAuth.Token);
            string jsonUser = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
            Dictionary<string, string> userValues = JsonConvert.DeserializeObject<Dictionary<string, string>>(jsonUser);
            string userId;
            if (userValues.TryGetValue("id", out userId))
                return userId;
            else
                return "";
        }

        public List<string> GetFriendsId(string userId)
        {
            oAuthFacebook oAuth = GetOAuthFacebook(userId);
            
            if (oAuth != null && oAuth.Token.Length > 0)
            {
                //We now have the credentials, so we can start making API calls
                String url = String.Format("https://graph.facebook.com/{0}/friends?access_token={1}",
                    userId, oAuth.Token);
                string jsonFriends = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
                List<string> friendsId = GetFriendsIdByJson(jsonFriends);
                return friendsId;
            }
            else
            {
                return null;
            }
        }

        public FacebookUserData GetFriendInfo(string userId, string userFriendId)
        {
            oAuthFacebook oAuth = GetOAuthFacebook(userId);
            FacebookUserData friendData;
            
            String url = String.Format("https://graph.facebook.com/{0}?access_token={1}",
                userFriendId, oAuth.Token);
            string jsonFriendInfo = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
            friendData = GetFriendInfoByJson(jsonFriendInfo);
            
            return friendData;
        }

        // TODO: see if this method will stay in this class
        private FacebookUserData GetFriendInfoByJson(string jsonFriendInfo)
        {
            throw new NotImplementedException();
        }
            
        // TODO: see if this method will stay in this class
        private oAuthFacebook GetOAuthFacebook(string userId)
        {
 	        throw new NotImplementedException();
        }

        // TODO: see if this method will stay in this class
        private List<string> GetFriendsIdByJson(string jsonFriends)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriends);
            List<string> friendsId = new List<string>();

            string id = (string)jsonFriendObject.SelectToken("data[0].id");

            int i = 1;
            while (id != null)
            {
                friendsId.Add(id);
                id = (string)jsonFriendObject.SelectToken("data[" + i + "].id");
                i++;
            }
            return friendsId;
        }

        // Only for the Prototype
        public List<string> GetFriendsNames(oAuthFacebook oAuth, string userId)
        {
            if (oAuth != null && oAuth.Token.Length > 0)
            {
                //We now have the credentials, so we can start making API calls
                String url = String.Format("https://graph.facebook.com/{0}/friends?access_token={1}",
                    userId, oAuth.Token);
                string jsonFriends = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
                List<string> friendsId = GetFriendsNamesByJson(jsonFriends);
                return friendsId;
            }
            else
            {
                return null;
            }
        }

        // Only for the Prototype
        private List<string> GetFriendsNamesByJson(string jsonFriends)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriends);
            List<string> friendsNames = new List<string>();

            string name = (string)jsonFriendObject.SelectToken("data[0].name");

            int i = 1;
            while (name != null)
            {
                friendsNames.Add(name);
                name = (string)jsonFriendObject.SelectToken("data[" + i + "].name");
                i++;
            }
            return friendsNames;
        }
    }
}