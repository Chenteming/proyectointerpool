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

        public void StartGame(string userIdFacebook)
        {
            this.controller.StartGame(userIdFacebook); 
        }

        public DataCity GetCurrentCity(string userIdFacebook)
        {
            return this.controller.GetCurrentCity(userIdFacebook);
        }

        public List<DataCity> GetPossibleCities(string userIdFacebook)
        {
            return this.controller.GetPossibleCities(userIdFacebook);
        }

        public DataFamous GetCurrentFamous(string userIdFacebook, int numClue)
        {
            return this.controller.GetCurrentFamous(userIdFacebook, numClue);
        }

        public List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            return this.controller.FilterSuspects(userIdFacebook, fbud);
        }

        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            this.controller.EmitOrderOfArrest(userIdFacebook, userIdFacebookSuspect);
        }

        public List<DataCity> GetCities(string userIdFacebook)
        {
            return this.controller.GetCities(userIdFacebook);
        }

        public DataCity Travel(string userIdFacebook, string nameNextCity)
        {
            return this.controller.Travel(userIdFacebook, nameNextCity);
        }

        public DataClue GetClueByFamous(string userIdFacebook, int numFamous)
        {
            return this.controller.GetClueByFamous(userIdFacebook, numFamous);
        }

        public string GetUserIdFacebook(string idLogin)
        {
            return this.controller.GetLastUserIdFacebook(idLogin);
            //// This is the correct function to use when the login screen is ready
            ////return controller.GetUserIdFacebook(idLogin);
        }
    }
}
