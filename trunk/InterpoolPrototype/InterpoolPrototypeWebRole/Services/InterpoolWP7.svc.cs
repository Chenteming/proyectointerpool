using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolPrototypeWebRole.Controller;
using InterpoolPrototypeWebRole.Data;

namespace InterpoolPrototypeWebRole.Services
{
    public class InterpoolWP7 : IInterpoolWP7
    {
        public IProcessController controller = new ProcessController();

        public void StartGame(User user)
        {
            controller.StartGame(user);
        }

        public string GetCurrentCity()
        {
            return controller.GetCurrentCity();
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
