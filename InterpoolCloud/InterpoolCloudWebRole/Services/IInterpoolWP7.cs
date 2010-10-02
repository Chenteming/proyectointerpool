using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;
using InterpoolCloudWebRole.Datatypes;

namespace InterpoolCloudWebRole.Services
{
    [ServiceContract]
    public interface IInterpoolWP7
    {
        [OperationContract]
        void StartGame(string userIdFacebook);

        [OperationContract]
        string GetUserIdFacebook(string idLogin);

        [OperationContract]
        DataCity GetCurrentCity(string userIdFacebook);

        [OperationContract]
        List<DataCity> GetPossibleCities(string userIdFacebook);

        [OperationContract]
        DataFamous GetCurrentFamous(string userIdFacebook, int numClue);

        [OperationContract]
        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        [OperationContract]
        List<DataCity> GetCities(string userIdFacebook);

        [OperationContract]
        DataCity Travel(string userIdFacebook, string nameNextCity);

		[OperationContract]
        string GetClueByFamous(string userIdFacebook, int numFamous);
    }
}
