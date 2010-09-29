using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolCloudWebRole.Data;


namespace InterpoolCloudWebRole.Controller
{
    interface IProcessController
    {
        void StartGame(string userIdFacebook);

        string GetCurrentCity(string userIdFacebook);

        List<string> GetPossibleCities(string userIdFacebook);

        List<string> GetCurrentFamous(string userIdFacebook);

        String Travel(string userIdFacebook);


        void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect);
    }
}
