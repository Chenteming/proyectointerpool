﻿using System;
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

        public string GetCurrentCity(string userIdFacebook)
        {
            return controller.GetCurrentCity(userIdFacebook);
        }

        public List<string> GetPossibleCities(string userIdFacebook)
        {
            return controller.GetPossibleCities(userIdFacebook);
        }

        public List<string> GetCurrentFamous(string userIdFacebook)
        {
            return controller.GetCurrentFamous(userIdFacebook);
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

        public void Travel(string userIdFacebook)
        {
            controller.Travel(userIdFacebook);
        }

        public string GetUserIdFacebook(string idLogin)
        {
            return controller.GetLastUserIdFacebook(idLogin);
        }
    }
}