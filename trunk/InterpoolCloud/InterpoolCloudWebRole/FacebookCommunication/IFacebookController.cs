//-----------------------------------------------------------------------
// <copyright file="IFacebookController.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
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
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="auth"> Parameter description for auth goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetUserId(OAuthFacebook auth);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<string> GetFriendsId(string userId);

        //// Returns the data of the friend with id userFriendId, who is a friend
        //// of the user with id userId

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <param name="userFriendId"> Parameter description for userFriendId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataFacebookUser GetFriendInfo(string userId, string userFriendId);

		/// <summary>
        /// Returns all the user info by the token
        /// </summary>
        /// <param name="auth"></param>
        /// <returns></returns>
        DataFacebookUser GetUserInfoByToken(OAuthFacebook auth);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="auth"> Parameter description for auth goes here</param>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<string> GetFriendsNames(OAuthFacebook auth, string userId);

        //// Add a new user to the datatype userID - auth (for multiplayer)

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="name"> Parameter description for name goes here</param>
        /// <param name="id"> Parameter description for id goes here</param>
        /// <param name="auth"> Parameter description for auth goes here</param>
        void AddFriend(string name, string id, OAuthFacebook auth);

        //// Save all the friends information in the db.

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="auth"> Parameter description for auth goes here</param>
        /// <param name="game"> Parameter description for game goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        void DownloadFacebookUserData(OAuthFacebook auth, Game game, InterpoolContainer context);
    }
}
