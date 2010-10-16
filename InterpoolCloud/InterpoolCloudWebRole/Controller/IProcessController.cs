using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;
using InterpoolCloudWebRole.Datatypes;

namespace InterpoolCloudWebRole.Controller
{
    interface IProcessController
    {
        void StartGame(string userIdFacebook);

        InterpoolContainer GetContainer();

        DataCity GetCurrentCity(string userIdFacebook);

        List<DataCity> GetPossibleCities(string userIdFacebook);

        DataFamous GetCurrentFamous(string userIdFacebook, int numClue);

        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        DataCity Travel(string userIdFacebook, string nameNextCity);

        void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect);

        List<DataCity> GetCities(string userId);

        DataClue GetClueByFamous(string userIdFacebook, int numFamous);

        string GetLastUserIdFacebook(string idLogin);
    }
}
