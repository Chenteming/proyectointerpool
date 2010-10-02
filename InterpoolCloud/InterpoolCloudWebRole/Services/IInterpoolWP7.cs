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
        void StartGame(User user);

        [OperationContract]
        string GetCurrentCity(string userIdFacebook);

        [OperationContract]
        List<string> GetPossibleCities(string userIdFacebook);

        [OperationContract]
        List<string> GetCurrentFamous(string userIdFacebook);

        [OperationContract]
        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud);

        [OperationContract]
        List<DataCity> GetCities(string userIdFacebook);

        [OperationContract]
        void Traveler(string userIdFacebook);
    }
}
