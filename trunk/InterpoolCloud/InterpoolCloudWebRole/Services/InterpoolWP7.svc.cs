using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolCloudWebRole.Controller;
using InterpoolCloudWebRole.Data;

namespace InterpoolCloudWebRole.Services
{
    public class InterpoolWP7 : IInterpoolWP7
    {
        private IProcessController controller = new ProcessController();

        public void StartGame(User user)
        {
            
        }

        public string GetCurrentCity(String s)
        {
            return controller.GetCurrentCity(s);
        }

        public List<string> GetPossibleCities()
        {
            return controller.GetPossibleCities();
        }

        public List<string> GetCurrentFamous(string city)
        {
            return controller.GetCurrentFamous(city);
        }

        

      
    }
}
