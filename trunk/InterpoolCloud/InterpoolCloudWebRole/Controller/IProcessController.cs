using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolCloudWebRole.Data;


namespace InterpoolCloudWebRole.Controller
{
    interface IProcessController
    {
        void StartGame(User user);
       
        string GetCurrentCity();

        List<string> GetPossibleCities();

        List<string> GetCurrentFamous(string city);
   
    }
}
