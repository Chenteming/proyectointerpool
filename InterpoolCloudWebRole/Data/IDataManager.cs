//-----------------------------------------------------------------------
// <copyright file="IDataManager.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// Interface Description IDataManager
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        IQueryable<City> GetCities(InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        IQueryable<Level> GetLevels(InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="city"> Parameter description for city goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="city"> Parameter description for city goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        IQueryable<CityProperty> GetCityPropertyByCity(City city, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="g"> Parameter description for g goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        IQueryable<Suspect> GetSuspectByGame(Game g, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetLastUserIdFacebook(InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetUserIdFacebookByLoginId(string userLoginId, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        OAuthFacebook GetLastUserToken(InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        void StoreUser(User user, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="suspect"> Parameter description for suspect goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        void StoreSuspect(Suspect suspect, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        void SaveChanges(InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        InterpoolContainer GetContainer();

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="name"> Parameter description for name goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetParameter(string name, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFaceook"> Parameter description for userIdFaceook goes here</param>
        /// <param name="conteiner"> Parameter description for conteiner goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        Game GetGameByUser(string userIdFaceook, InterpoolContainer conteiner);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud fbud here</param>
        /// <param name="container"> Parameter description for container goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud, InterpoolContainer container);

        /// <summary>
        /// Description for Method.
        /// </summary>
        /// <param name="context">Parameter description for context goes here</param>
        /// <param name="userIdFacebook">Parameter description for userIdFacbeook goes here</param>
        /// <returns> 
        /// Return results are described through the returns tag.</returns>
        IQueryable<User> GetUserByIdFacebook(InterpoolContainer context, string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <param name="subLevel"> Parameter description for subLevel goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        bool UserHasSubLevel(int userId, int subLevel, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="token"> Parameter description for token goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        User GetUserByToken(string token, InterpoolContainer context);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for token goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataUserInfo GetUserInfoByLoginId(string userLoginId, InterpoolContainer context);
    }
}
