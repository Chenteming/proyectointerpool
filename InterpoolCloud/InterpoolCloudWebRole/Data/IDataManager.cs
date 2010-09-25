using System;
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

        void StoreUser(User user, InterpoolContainer context);
        void StoreSuspect(Suspect suspect, InterpoolContainer context);

        void SaveChanges(InterpoolContainer context);

        InterpoolContainer GetContainer();

        string GetParameter(string name, InterpoolContainer context);

        Game GetGameByUser(string userIdFaceook, InterpoolContainer conteiner);
    }
}
