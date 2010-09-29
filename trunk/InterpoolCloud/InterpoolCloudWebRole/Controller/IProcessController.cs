using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;


namespace InterpoolCloudWebRole.Controller
{
    interface IProcessController
    {
        void StartGame(string userIdFacebook);

        string GetCurrentCity(string userIdFacebook);

        List<string> GetPossibleCities(string userIdFacebook);

        List<string> GetCurrentFamous(string userIdFacebook);

        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        String Travel(string userIdFacebook);

        void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect);
    }
}
