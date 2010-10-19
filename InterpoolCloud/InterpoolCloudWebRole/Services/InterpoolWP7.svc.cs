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
    
    public class InterpoolWP7 : IInterpoolWP7
    {   
        private IProcessController controller = new ProcessController(new InterpoolContainer());

        public void StartGame(string userIdFacebook)
        {
            controller.StartGame(userIdFacebook); 
        }

        public DataCity GetCurrentCity(string userIdFacebook)
        {
            return controller.GetCurrentCity(userIdFacebook);
        }

        public List<DataCity> GetPossibleCities(string userIdFacebook)
        {
            return controller.GetPossibleCities(userIdFacebook);
        }

        public DataFamous GetCurrentFamous(string userIdFacebook, int numClue)
        {
            return controller.GetCurrentFamous(userIdFacebook, numClue);
        }

        public List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            return controller.FilterSuspects(userIdFacebook, fbud);
        }

        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            controller.EmitOrderOfArrest(userIdFacebook, userIdFacebookSuspect);
        }

        public List<DataCity> GetCities(string userIdFacebook)
        {
            return controller.GetCities(userIdFacebook);
        }

        public DataCity Travel(string userIdFacebook, string nameNextCity)
        {
            return controller.Travel(userIdFacebook, nameNextCity);
        }

        public DataClue GetClueByFamous(string userIdFacebook, int numFamous)
        {
            return controller.GetClueByFamous(userIdFacebook, numFamous);
        }

        public string GetUserIdFacebook(string idLogin)
        {
            return controller.GetLastUserIdFacebook(idLogin);
            //// This is the correct function to use when the login screen is ready
            //return controller.GetUserIdFacebook(idLogin);
        }
    }
}
