using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using InterpoolPrototypeWebRole.Data;

namespace InterpoolPrototypeWebRole.FacebookComunication
{
    public class FacebookController : IFacebookController
    {
        //stores al recovered data form user facebook friends.
        private Dictionary<String,FacebookUserData> userIdOauth = new Dictionary<string,FacebookUserData>();
        private List<FacebookUserData> faceUserData = new List<FacebookUserData>();

        // Download from Facebook all the information from user and user's friends
        //and store it on the data base.
        public void DownloadUserFacebookData(oAuthFacebook oAuth)
        {
            string userId = this.GetUserId(oAuth);
            this.AddFriend("", userId, oAuth);
            if (!userId.Equals(""))
            {
                List<string> friendsIds = this.GetFriendsId(userId);
                InterpoolContainer context = new InterpoolContainer();
                List<Friends> listFriends = new List<Friends>(context.Friends);
                // Deletes all the existing suspects
                foreach (Friends pFriendsDelete in listFriends)
                {
                    context.DeleteObject(pFriendsDelete);
                }
                context.SaveChanges();

                Friends pFriends;
                // Creates the suspects for the current user
                int limit = 10;
                int i = 0;
               foreach (string id in friendsIds)
                {
                    pFriends = new Friends();
                    pFriends.Id_face = id;
                    context.AddToFriends(pFriends);
                    i++;
                    if (i > limit)
                    {
                        break;
                    }
                }
                context.SaveChanges();

                //create a new list of friends ID
                List<string> friendsIdList = new List<string>();
                foreach (Friends pFriends2 in context.Friends)
                {
                    friendsIdList.Add(pFriends2.Id_face);
                }

                //getting and saving the information of all user friends
                List<FacebookUserData> fbud = new List<FacebookUserData>();
                foreach (string id_face in friendsIdList)
                {
                    fbud.Add(this.GetFriendInfo(userId, id_face));
                }

                foreach (FacebookUserData facebud in fbud)
                {
                    pFriends = new Friends();
                    pFriends.Id_face = facebud.id_friend;
                    pFriends.First_name = facebud.first_name;
                    pFriends.Last_name = facebud.last_name;
                    pFriends.Birthday = facebud.birthday;
                    pFriends.Sex = facebud.gender;
                    pFriends.Hometown = facebud.hometown;
                    pFriends.Likes = facebud.likes;

                    context.AddToFriends(pFriends);
                }
                context.SaveChanges();

            }
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
            //this is for single user game
            return this.GetOauth(userId);
            
 	        //TODO: [multiUserGame]we must ask for the oAuth for de actual user
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