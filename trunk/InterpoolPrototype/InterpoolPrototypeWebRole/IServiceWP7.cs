using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace InterpoolPrototypeWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceWP7" in both code and config file together.
    [ServiceContract]
    public interface IServiceWP7
    {
        [OperationContract]
        List<string> GetCities();

        [OperationContract]
        string GetClueByFamous(string cadena);

        [OperationContract]
        void StartGame();

        [OperationContract]
        string GetCurrentCity();

        [OperationContract]
        List<string> GetPossibleCities();

        [OperationContract]
        List<string> GetCurrentFamous(string city);

        [OperationContract]
        void Travel(string City);

        [OperationContract]
        List<string> GetProbablySuspects();
    }
    

}
