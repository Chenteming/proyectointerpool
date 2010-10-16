
namespace InterpoolCloudWebRole.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Datatypes;

    interface IDataManager
    {
        IQueryable<City> getCities(InterpoolContainer context);
        IQueryable<Level> getLevels(InterpoolContainer context);
        IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context);
        IQueryable<CityProperty> GetCityPropertyByCity(City city, InterpoolContainer context);
        IQueryable<Suspect> GetSuspectByGame(Game g, InterpoolContainer context);
        string GetLastUserIdFacebook(InterpoolContainer context);
        string GetUserIdFacebookByLoginId(string userLoginId, InterpoolContainer context);
        oAuthFacebook GetLastUserToken(InterpoolContainer context);

        void StoreUser(User user, InterpoolContainer context);
        void StoreSuspect(Suspect suspect, InterpoolContainer context);

        void SaveChanges(InterpoolContainer context);

        InterpoolContainer GetContainer();

        string GetParameter(string name, InterpoolContainer context);

        Game GetGameByUser(string userIdFaceook, InterpoolContainer conteiner);

        List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud, InterpoolContainer container);
    }
}
