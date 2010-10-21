
namespace InterpoolCloudWebRole.FacebookCommunication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;

    /// <summary>
    /// Interface Description IFacebookController
    /// </summary>
    interface IFacebookController
    {
        string GetUserId(OAuthFacebook auth);

        // Returns the user's friends ids
        List<string> GetFriendsId(string userId);

        // Returns the data of the friend with id userFriendId, who is a friend
        // of the user with id userId
        DataFacebookUser GetFriendInfo(string userId, string userFriendId);

        // Only for the protoype
        List<string> GetFriendsNames(OAuthFacebook auth, string userId);

        // Add a new user to the datatype userID - auth (for multiplayer)
        void AddFriend(string name, string id, OAuthFacebook auth);

        // Save all the friends information in the db.
        void DownloadFacebookUserData(OAuthFacebook auth, Game game, InterpoolContainer context);
    }
}
