﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using InterpoolCloudWebRole.FacebookCommunication;

namespace InterpoolCloudWebRole.Data
{
    interface IDataManager
    {
        IQueryable<City> getCities(InterpoolContainer context);
        IQueryable<Level> getLevels(InterpoolContainer context);
        IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context);
        IQueryable<CityProperty> GetCityPropertyByCity(City city, InterpoolContainer context);
        IQueryable<Suspect> GetSuspectByGame(Game g, InterpoolContainer context);

        void StoreUser(FacebookUserData fbud, InterpoolContainer context);

        string GetParameter(string name, InterpoolContainer context);
    }
}