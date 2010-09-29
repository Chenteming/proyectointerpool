﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Controller;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;

namespace InterpoolCloudWebRole.Services
{
    public class InterpoolWP7 : IInterpoolWP7
    {
        private IProcessController controller = new ProcessController();

        public void StartGame(User user)
        {
            
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

        public List<FacebookUserData> FilterSuspects(string userIdFacebook, FacebookUserData fbud)
        {
            return null;
        }

        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            controller.EmitOrderOfArrest(userIdFacebook, userIdFacebookSuspect);
        }
      
    }
}
