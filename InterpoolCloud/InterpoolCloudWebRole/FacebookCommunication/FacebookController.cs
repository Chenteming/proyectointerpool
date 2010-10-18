﻿
namespace InterpoolCloudWebRole.FacebookCommunication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Collections;
    using Newtonsoft.Json.Linq;
    using Newtonsoft.Json;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Utilities;
    using InterpoolCloudWebRole.Datatypes;

    public class FacebookController : IFacebookController
    {
        // Stores al recovered data form user facebook friends.
        private Dictionary<String,DataFacebookUser> userIdOauth = new Dictionary<string, DataFacebookUser>();
        private IDataManager dataManager = new DataManager();

        // Downloads from Facebook all the information from user and user's friends
        // and stores it on the data base.
        public void DownloadFacebookUserData(oAuthFacebook oAuth, Game game, InterpoolContainer context)
        {
            string userId = this.GetUserId(oAuth);
            if (!userId.Equals(""))
            {
                DataFacebookUser fbud = new DataFacebookUser();
                fbud.userId = userId;
                fbud.oAuth = oAuth;

                // TODO: this must be stored in the database
                userIdOauth.Add(userId, fbud);

                List<string> friendsIds = this.GetFriendsId(userId);
                Suspect suspect;
                List<Suspect> suspects = new List<Suspect>();
                DataFacebookUser fbudOfSuspect; 

                // Creates and stores the suspects for the current user
                int limit = Constants.MAX_SUSPECTS;
                int i = 0;
                int numberSuspect = new Random().Next(0, limit);
                foreach (string friendId in friendsIds)
                {
                    fbudOfSuspect = this.GetFriendInfo(userId, friendId);
                    suspect = NewSuspectFromFacebookUserData(fbudOfSuspect);
                    if (haveEnouthFields(suspect, Constants.DATA_REQUIRED))
                    {
                        dataManager.StoreSuspect(suspect, context);
                        if (numberSuspect == i)
                            game.Suspect = suspect;
                        else
                            game.PossibleSuspect.Add(suspect);
                        i++;
                    }

                    if (i >= limit)
                    {
                        break;
                    }
                }

                // Stores the changes made to the game
                dataManager.SaveChanges(context);
            }
        }

        private Boolean haveEnouthFields(Suspect fbudOfSuspect, int cantDataRequired)
        {
            int cant = 0;
            if (fbudOfSuspect.SuspectBirthday != "")
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectCinema != "")
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectGender != "")
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectHometown != "")
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectMusic != "")
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectTelevision != "")
            {
                cant++;
            }

            if (cant < cantDataRequired)
            {
                return false;
            }
            else
            {
                return true;
            } 
        }

        private Suspect NewSuspectFromFacebookUserData(DataFacebookUser fbudOfSuspect)
        {
            Suspect suspect = new Suspect();
            suspect.SuspectFacebookId = (fbudOfSuspect.id_friend == null) ? "" : fbudOfSuspect.id_friend;
            suspect.SuspectFirstName = fbudOfSuspect.first_name;
            suspect.SuspectLastName = fbudOfSuspect.last_name;
            suspect.SuspectBirthday = (fbudOfSuspect.birthday == null) ? "" : fbudOfSuspect.birthday;
            suspect.SuspectHometown = (fbudOfSuspect.hometown == null) ? "" : fbudOfSuspect.hometown;
            suspect.SuspectMusic = (fbudOfSuspect.music == null) ? "" : fbudOfSuspect.music;
            suspect.SuspectTelevision = (fbudOfSuspect.television == null) ? "" : fbudOfSuspect.television;
            suspect.SuspectCinema = (fbudOfSuspect.cinema == null) ? "" : fbudOfSuspect.cinema;
            suspect.SuspectGender = (fbudOfSuspect.gender == null) ? "" : fbudOfSuspect.gender;
            suspect.SuspectPicLInk = (fbudOfSuspect.pictureLink == null) ? "" : fbudOfSuspect.pictureLink;
            
            return suspect;
        }

        public oAuthFacebook GetOauth(string userId) 
        {
            DataFacebookUser fbud;
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
            DataFacebookUser fbud = new DataFacebookUser();
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

        public DataFacebookUser GetFriendInfo(string userId, string userFriendId)
        {
            oAuthFacebook oAuth = GetOAuthFacebook(userId);
            DataFacebookUser friendData = new DataFacebookUser();
            
            String url = String.Format("https://graph.facebook.com/{0}?access_token={1}",
                userFriendId, oAuth.Token);
            string jsonFriendInfo = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);
            friendData = GetFriendStandardInfoByJson(jsonFriendInfo);

            url = String.Format("https://graph.facebook.com/{0}/likes?access_token={1}",
                userFriendId, oAuth.Token);
            jsonFriendInfo = oAuth.WebRequest(oAuthFacebook.Method.GET, url, String.Empty);

            // The likes will be discriminates as Television, Cinema and Music
            friendData = GetFriendLikesInfoByJson(jsonFriendInfo, friendData);

            friendData.pictureLink = String.Format("https://graph.facebook.com/{0}/picture",
                friendData.id_friend, "");
            
            return friendData;
        }

        // TODO: see if this method will stay in this class
        private DataFacebookUser GetFriendStandardInfoByJson(string jsonFriendInfo)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriendInfo);
            List<string> friendsId = new List<string>();
            DataFacebookUser fbud = new DataFacebookUser();
            fbud.birthday = "";
            fbud.cinema = "";
            fbud.first_name = "";
            fbud.hometown = "";
            fbud.last_name = "";
            fbud.music = "";
            fbud.television = "";
            fbud.userId = "";
            fbud.id_friend = "";
            fbud.gender = "";
            fbud.pictureLink = "";

            //string id = (string)jsonFriendObject.SelectToken("name");

            //================GETTING STANDARD FRIENDS DATA=====================//
            // Error = if true rise exception when does not match token.
            bool error = false;
            fbud.id_friend = (string)jsonFriendObject.SelectToken("id", error);
            fbud.first_name = (string)jsonFriendObject.SelectToken("first_name", error);
            fbud.last_name = (string)jsonFriendObject.SelectToken("last_name", error);
            fbud.birthday = (string)jsonFriendObject.SelectToken("birthday", error);
            //TODO: check the output format
            if (fbud.birthday != null)
            {
                string[] fecha = fbud.birthday.Split('/');
                switch(fecha[1])
                {
                    case "1":
                        fbud.birthday = "Enero";
                        break;
                    case "2":
                        fbud.birthday = "Febrero";
                        break;
                    case "3":
                        fbud.birthday = "Marzo";
                        break;
                    case "4":
                        fbud.birthday = "Abril";
                        break;
                    case "5":
                        fbud.birthday = "Mayo";
                        break;
                    case "6":
                        fbud.birthday = "Junio";
                        break;
                    case "7":
                        fbud.birthday = "Julio";
                        break;
                    case "8":
                        fbud.birthday = "Agosto";
                        break;
                    case "9":
                        fbud.birthday = "Setiembre";
                        break;
                    case "10":
                        fbud.birthday = "Octubre";
                        break;
                    case "11":
                        fbud.birthday = "Noviembre";
                        break;
                    case "12":
                        fbud.birthday = "Diciembre";
                        break;
                }
            }

            fbud.gender = (string)jsonFriendObject.SelectToken("gender", error);
            JObject jsonFriendObjectAnid = (JObject)jsonFriendObject.SelectToken("hometown", error);
            if (jsonFriendObjectAnid != null)
            {
                fbud.hometown = (string)jsonFriendObjectAnid.SelectToken("name", error);
            }

            return fbud;                           
        }

        // TODO: see if this method will stay in this class
        private DataFacebookUser GetFriendLikesInfoByJson(string jsonFriendInfo, DataFacebookUser friendData)
        {
            JObject jsonFriendObject = JObject.Parse(jsonFriendInfo);
                        
            //================GETTING LIKES FRIENDS DATA=====================//
            string like_category = (string)jsonFriendObject.SelectToken("data[0].category");


            int i = 0;
            bool exit = false;
            friendData.music = "";
            friendData.television = "";
            friendData.cinema = "";
            
            while (like_category != null && !exit)
            {
                switch(like_category)
                {
                    case "Music":
                    case "Musicians":
                        friendData.music = (string)jsonFriendObject.SelectToken("data[" + i + "].name");
                        break;
                    case "Television":
                        friendData.television = (string)jsonFriendObject.SelectToken("data[" + i + "].name");
                        break;
                    case "Movie":
                    case "Film":
                        friendData.cinema = (string)jsonFriendObject.SelectToken("data[" + i + "].name");
                        break;
                }

                i++;
                like_category = (string)jsonFriendObject.SelectToken("data[" + i + "].category");
                if (friendData.music != "" && friendData.television != "" && friendData.cinema != "")
                {
                    exit = true;
                }           
            }
            return friendData;           
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