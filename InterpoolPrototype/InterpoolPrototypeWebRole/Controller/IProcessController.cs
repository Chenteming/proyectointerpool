using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolPrototypeWebRole.Data;


namespace InterpoolPrototypeWebRole.Controller
{
    interface IProcessController
    {
        public void StartGame(User user);
        public void CreateClue();
        public void GetSuspects();
        public void BuiltTravel(User user);
        public IQueryable<Clue> CreateClue(City city, User user, Suspect suspect);

    }
}
