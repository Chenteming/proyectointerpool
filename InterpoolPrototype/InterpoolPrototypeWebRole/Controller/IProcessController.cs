using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolPrototypeWebRole.Data;


namespace InterpoolPrototypeWebRole.Controller
{
    interface IProcessController
    {
        void StartGame(User user);

        City NextCity(User user, City city);

        string GetCurrentCity();

        List<string> GetPossibleCities();

        List<string> GetCurrentFamous(string city);
    }
}
