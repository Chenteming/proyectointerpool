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
        void GetSuspects();
        void BuiltTravel(User user);
        List<Clue> CreateClue(City city, User user, Suspect suspect);
        City NextCity(User user, City city);
    }
}
