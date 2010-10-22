
namespace InterpoolCloudWebRole.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// Interface Description IProcessController
    /// </summary>
    interface IProcessController
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        void StartGame(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        InterpoolContainer GetContainer();

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataCity GetCurrentCity(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<DataCity> GetPossibleCities(string userIdFacebook);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numClue"> Parameter description for fbud numClue here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataFamous GetCurrentFamous(string userIdFacebook, int numClue);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="nameNextCity"> Parameter description for nameNextCity fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataCity Travel(string userIdFacebook, string nameNextCity);

        /// <summary>
        /// Description for Method.</summary>                 
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="userIdFacebookSuspect"> Parameter description for userIdFacebookSuspect fbud here</param>
        void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        List<DataCity> GetCities(string userId);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numFamous"> Parameter description for numFamous fbud here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        DataClue GetClueByFamous(string userIdFacebook, int numFamous);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="idLogin"> Parameter description for idLogin goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetLastUserIdFacebook(string idLogin);

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        string GetUserIdFacebook(string userLoginId);
    }
}
