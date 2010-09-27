using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.FacebookCommunication;

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
        List<FacebookUserData> FilterSuspects(string userIdFacebook, FacebookUserData fbud);
    }
}
