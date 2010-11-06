//-----------------------------------------------------------------------
// <copyright file="InterpoolWP7.svc.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.Text;
    using InterpoolCloudWebRole.Controller;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// Class statement InterpoolWP7
    /// </summary>
    public class InterpoolWP7 : IInterpoolWP7
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private IProcessController controller = new ProcessController(new InterpoolContainer());

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        public void StartGame(string userIdFacebook)
        {
            this.controller.StartGame(userIdFacebook); 
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataCity GetCurrentCity(string userIdFacebook)
        {
            return this.controller.GetCurrentCity(userIdFacebook);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public List<DataCity> GetPossibleCities(string userIdFacebook)
        {
            return this.controller.GetPossibleCities(userIdFacebook);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numClue"> Parameter description for numClue goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataFamous GetCurrentFamous(string userIdFacebook, int numClue)
        {
            return this.controller.GetCurrentFamous(userIdFacebook, numClue);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataListFacebookUser FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            return this.controller.FilterSuspects(userIdFacebook, fbud);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="userIdFacebookSuspect"> Parameter description for userIdFacebookSuspect goes here</param>
        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            this.controller.EmitOrderOfArrest(userIdFacebook, userIdFacebookSuspect);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public List<DataCity> GetCities(string userIdFacebook)
        {
            return this.controller.GetCities(userIdFacebook);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="nameNextCity"> Parameter description for nameNextCity goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataCity Travel(string userIdFacebook, string nameNextCity)
        {
            return this.controller.Travel(userIdFacebook, nameNextCity);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numFamous"> Parameter description for numFamous goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataClue GetClueByFamous(string userIdFacebook, int numFamous)
        {
            return this.controller.GetClueByFamous(userIdFacebook, numFamous);
        }

        //// TODO: delete if GetUserInfo works fine
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="idLogin"> Parameter description for idLogin goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetUserIdFacebook(string idLogin)
        {
            ////return this.controller.GetLastUserIdFacebook(idLogin);
            //// This is the correct function to use when the login screen is ready
            return this.controller.GetUserIdFacebook(idLogin);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="idLogin"> Parameter description for idLogin goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataUserInfo GetUserInfo(string idLogin)
        {
            //// This is the correct function to use when the login screen is ready
            return this.controller.GetUserInfo(idLogin);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        public void DeleteGame(User user)
        {
            this.controller.DeleteGame(user);
        }
    }
}
