using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Data;

namespace InterpoolCloudWebRole.Services
{
    [ServiceContract]
    public interface IInterpoolWP7
    {
        [OperationContract]
        void StartGame(User user);

        [OperationContract]
        string GetCurrentCity();

        [OperationContract]
        List<string> GetPossibleCities();

        [OperationContract]
        List<string> GetCurrentFamous(string city);
    }
}
