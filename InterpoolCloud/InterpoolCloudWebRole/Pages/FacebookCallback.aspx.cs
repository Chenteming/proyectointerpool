
namespace InterpoolCloudWebRole
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using Newtonsoft.Json;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Utilities;

    public partial class Face : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            oAuthFacebook oAuth = new oAuthFacebook();
            // Para probar
            var code = Request["code"];

            if (Request["code"] == null)
            {
                // Redirect the user back to Facebook for authorization.
                Response.Redirect(oAuth.AuthorizationLinkGet());
            }
            else
            {
                // Get the access token and secret.
                oAuth.AccessTokenGet(Request["code"]);

                // Para probar
                var token = oAuth.Token;

                if (oAuth.Token.Length > 0)
                {
                    IFacebookController facebookController = new FacebookController();
                    IDataManager dataManager = new DataManager();
                    InterpoolContainer container = dataManager.GetContainer();
                    User user = new User();
                    user.SubLevel = 0;
                    string codLevel = dataManager.GetParameter(Parameters.LEVEL_ROOKIE, container);
                    user.Level = container.Levels.Where(l => l.LevelName == codLevel).First();
                    user.UserIdFacebook = facebookController.GetUserId(oAuth);
                    user.UserTokenFacebook = oAuth.Token;
                    dataManager.StoreUser(user, container);

                    Response.Redirect(Constants.REDIRECT_URL_AFTER_LOGIN_FACEBOOK);

                    /*string userId = facebookController.GetUserId(oAuth);
                    //add userId - oAuth [multiplayer feature]
                    facebookController.AddFriend("", userId, oAuth);                   


                    if (!userId.Equals(""))
                    {
                        List<string> friendsIds = facebookController.GetFriendsId(userId);
                        //List<string> friendsNames = facebookController.GetFriendsNames(oAuth, userId);
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
                        foreach (string id in friendsIds)
                        {
                            pFriends = new Friends();
                            pFriends.Id_face = id;
                            context.AddToFriends(pFriends);
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
                            fbud.Add(facebookController.GetFriendInfo(userId, id_face));
                        }

                        foreach (FacebookUserData facebud in fbud)
                        {
                            pFriends = new Friends();
                            pFriends.Id_face = facebud.id_friend;
                            pFriends.First_name = facebud.first_name;
                            pFriends.Last_name  = facebud.last_name;
                            pFriends.Birthday = facebud.birthday;
                            pFriends.Sex = facebud.gender;
                            pFriends.Hometown = facebud.hometown;
                            pFriends.Likes = facebud.likes;

                            context.AddToFriends(pFriends);
                        }
                        context.SaveChanges();


                           
                    }*/
                }
            }
        }
    }
}