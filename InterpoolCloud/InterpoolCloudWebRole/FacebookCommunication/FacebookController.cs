using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.Utilities;

namespace InterpoolCloudWebRole.FacebookCommunication
{
    public class FacebookController : IFacebookController
    {
        // Stores al recovered data form user facebook friends.
        private Dictionary<String,FacebookUserData> userIdOauth = new Dictionary<string,FacebookUserData>();
        private IDataManager dataManager = new DataManager();

        // Downloads from Facebook all the information from user and user's friends
        // and stores it on the data base.
        public void DownloadFacebookUserData(oAuthFacebook oAuth, Game game, InterpoolContainer context)
        {
            string userId = this.GetUserId(oAuth);
            if (!userId.Equals(""))
            {
                FacebookUserData fbud = new FacebookUserData();
                fbud.userId = userId;
                fbud.oAuth = oAuth;

                // TODO: this must be stored in the database
                userIdOauth.Add(userId, fbud);

                List<string> friendsIds = this.GetFriendsId(userId);
                Suspect suspect;
                List<Suspect> suspects = new List<Suspect>();
                FacebookUserData fbudOfSuspect; 
                // Creates and stores the suspects for the current user
                int limit = Constants.MAX_SUSPECTS;
                int i = 0;
                int numberSuspect = new Random().Next(0, limit);
                foreach (string friendId in friendsIds)
                {
                    fbudOfSuspect = this.GetFriendInfo(userId, friendId);
                    suspect = NewSuspectFromFacebookUserData(fbudOfSuspect);
                    dataManager.StoreSuspect(suspect, context);
                    if (numberSuspect == i)
                        game.Suspect = suspect;
                    else
                        game.PossibleSuspect.Add(suspect);
                    i++;
                    if (i >= limit)
                    {
                        break;
                    }
                }
                // Stores the changes made to the game
                dataManager.SaveChanges(context);
            }
        }

        private Suspect NewSuspectFromFacebookUserData(FacebookUserData fbudOfSuspect)
        {
            Suspect suspect = new Suspect();
            // FIX: add field FacebookId to Suspect
            // TODO: add this
            // suspect.FacebookId = fbudOfSuspect.userId;
            suspect.SuspectName = fbudOfSuspect.first_name + " " + fbudOfSuspect.last_name;
            suspect.SuspectPreferenceMusic = fbudOfSuspect.hometown;
            return suspect;
        }

        public oAuthFacebook GetOauth(string userId) 
        {
            FacebookUserData fbud;
            if (userIdOauth.TryGetValue(userId, out fbud) == true) 
            {
                return fbud.oAuth;
            }

            return null;
        }


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

        public void AddFriend(string name, string id, oAuthFacebook oAuth)
        {
            FacebookUserData fbud = new FacebookUserData();
            fbud.nombre = name;
            fbud.oAuth = oAuth;
            fbud.userId = id;

            userIdOauth.Add(id, fbud);
        }

        public List<string> GetFriendsId(string userId)
        {
            oAuthFacebook oAuth = this.GetOAuthFacebook(userId);

            if (oAuth != null && oAuth.Token.Length > 0)
            {
                // We now have the credentials, so we can start making API calls
                String url = String.Format("https://graph.facebook.com/{0}/friends?access_token={1}",
                    userId, oAuth.Token);
                string jsonFriends = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
                List<string> friendsId = GetFriendsIdByJson(jsonFriends);
                return friendsId;
            }
            return null;
        }    
        

        public void UploadUserFriendsInformation()
        {

        }

        public FacebookUserData GetFriendInfo(string userId, string userFriendId)
        {
            oAuthFacebook oAuth = GetOAuthFacebook(userId);
            FacebookUserData friendData = new FacebookUserData();
            
            String url = String.Format("https://graph.facebook.com/{0}?access_token={1}",
                userFriendId, oAuth.Token);
            string jsonFriendInfo = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
            friendData = GetFriendStandardInfoByJson(jsonFriendInfo);

            url = String.Format("https://graph.facebook.com/{0}/likes?access_token={1}",
                userFriendId, oAuth.Token);
            jsonFriendInfo = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);

            friendData.likes = GetFriendLikesInfoByJson(jsonFriendInfo);
            
            return friendData;
        }

        // TODO: see if this method will stay in this class
        private FacebookUserData GetFriendStandardInfoByJson(string jsonFriendInfo)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriendInfo);
            List<string> friendsId = new List<string>();
            FacebookUserData fbud = new FacebookUserData();

            //string id = (string)jsonFriendObject.SelectToken("name");

            //================GETTING STANDARD FRIENDS DATA=====================//
            // Error = if true rise exception when does not match token.
            bool error = false;
            fbud.id_friend = (string)jsonFriendObject.SelectToken("id", error);
            fbud.first_name = (string)jsonFriendObject.SelectToken("first_name", error);
            fbud.first_name = (string)jsonFriendObject.SelectToken("first_name", error);
            fbud.last_name = (string)jsonFriendObject.SelectToken("last_name", error);
            fbud.birthday = (string)jsonFriendObject.SelectToken("birthday", error);
            fbud.gender = (string)jsonFriendObject.SelectToken("gender", error);
            JObject jsonFriendObjectAnid= (JObject)jsonFriendObject.SelectToken("hometown", error);
            if (jsonFriendObjectAnid != null)
            {
                fbud.hometown = (string)jsonFriendObjectAnid.SelectToken("name", error);
            }



            return fbud;
        }

        // TODO: see if this method will stay in this class
        private string GetFriendLikesInfoByJson(string jsonFriendInfo)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriendInfo);
            string likes = "";
            
            //================GETTING LIKES FRIENDS DATA=====================//
            string like_name = (string)jsonFriendObject.SelectToken("data[0].name");

            int i = 1;
            while (like_name != null)
            {
                likes = likes + " " + like_name;
                like_name = (string)jsonFriendObject.SelectToken("data[" + i + "].name");
                i++;
            }
            //fbud = new FacebookUserData();
            return likes;
        }
            
        // TODO: see if this method will stay in this class
        private oAuthFacebook GetOAuthFacebook(string userId)
        {
            // this is for single user game
            return this.GetOauth(userId);
            
 	        // TODO: [multiUserGame]we must ask for the oAuth for the actual user
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


        public void DownloadFacebookUserData(oAuthFacebook oAuth, InterpoolContainer context)
        {
            throw new NotImplementedException();
        }
    }
}