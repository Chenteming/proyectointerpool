//-----------------------------------------------------------------------
// <copyright file="IProcessController.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.ServiceModel;
    using System.Text;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// Interface Description IProcessController
    /// </summary>
    public interface IProcessController
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        [FaultContract(typeof(FaultException))]
        void StartGame(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        InterpoolContainer GetContainer();

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataCity GetCurrentCity(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        List<DataCity> GetPossibleCities(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numClue"> Parameter description for fbud numClue here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataFamous GetCurrentFamous(string userIdFacebook, int numClue);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataListFacebookUser FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="nameNextCity"> Parameter description for nameNextCity fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataCity Travel(string userIdFacebook, string nameNextCity);

        /// <summary>
        /// Description for Method.</summary>                 
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="userIdFacebookSuspect"> Parameter description for userIdFacebookSuspect fbud here</param>
        [FaultContract(typeof(FaultException))]
        void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        List<DataCity> GetCities(string userId);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numFamous"> Parameter description for numFamous fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataClue GetClueByFamous(string userIdFacebook, int numFamous);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="idLogin"> Parameter description for idLogin goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        string GetLastUserIdFacebook(string idLogin);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        string GetUserIdFacebook(string userLoginId);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataUserInfo GetUserInfo(string userLoginId);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="game"> Parameter description for game goes here</param>
        /// <param name="privatesProperties"> Parameter description for privatesProperties goes here</param> 
        [FaultContract(typeof(FaultException))]
        void CreateHardCodeSuspects(Game game, List<string> privatesProperties);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        [FaultContract(typeof(FaultException))]
        void DeleteGame(User user);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        DataFacebookUser GetFilters(string userIdFacebook);
    }
}
