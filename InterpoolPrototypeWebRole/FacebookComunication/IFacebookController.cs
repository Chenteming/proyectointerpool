using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace InterpoolPrototypeWebRole.FacebookComunication
{
    interface IFacebookController
    {
        string GetUserId(oAuthFacebook oAuth);

        // Returns the user's friends ids
        List<string> GetFriendsId(string userId);

        // Returns the data of the friend with id userFriendId, who is a friend
        // of the user with id userId
        FacebookUserData GetFriendInfo(String userId, String userFriendId);

        // Only for the protoype
        List<string> GetFriendsNames(oAuthFacebook oAuth, string userId);
    }
}
