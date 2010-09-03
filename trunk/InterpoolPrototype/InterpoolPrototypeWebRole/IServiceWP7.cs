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
    }
}
