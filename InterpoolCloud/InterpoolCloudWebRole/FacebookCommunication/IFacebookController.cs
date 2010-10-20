
namespace InterpoolCloudWebRole.FacebookCommunication
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;

    interface IFacebookController
    {
        string GetUserId(OAuthFacebook oAuth);

        // Returns the user's friends ids
        List<string> GetFriendsId(string userId);

        // Returns the data of the friend with id userFriendId, who is a friend
        // of the user with id userId
        DataFacebookUser GetFriendInfo(string userId, string userFriendId);

        // Only for the protoype
        List<string> GetFriendsNames(OAuthFacebook oAuth, string userId);

        // Add a new user to the datatype userID - oAuth (for multiplayer)
        void AddFriend(string name, string id, OAuthFacebook oAuth);

        // Save all the friends information in the db.
        void DownloadFacebookUserData(OAuthFacebook oAuth, Game game, InterpoolContainer context);
    }
}
