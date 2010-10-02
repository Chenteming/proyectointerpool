using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Controller;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;
using InterpoolCloudWebRole.Datatypes;

namespace InterpoolCloudWebRole.Services
{
    public class InterpoolWP7 : IInterpoolWP7
    {
        private IProcessController controller = new ProcessController();

        public void StartGame(string userIdFacebook)
        {
            controller.StartGame(userIdFacebook); 
        }

        public DataCity GetCurrentCity(string userIdFacebook)
        {
            return controller.GetCurrentCity(userIdFacebook);
        }

        public List<string> GetPossibleCities(string userIdFacebook)
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

		public string GetClueByFamous(string userIdFacebook, int numFamous)
        {
            return controller.GetClueByFamous(userIdFacebook, numFamous);
        }

        public string GetUserIdFacebook(string idLogin)
        {
            return controller.GetLastUserIdFacebook(idLogin);
        }

    }
}
