using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterpoolCloudWebRole.FacebookCommunication;

namespace InterpoolCloudWebRole.Data
{
    public class DataManager : IDataManager
    {
        public IQueryable<City> getCities(InterpoolContainer context)
        {
             
            return context.Cities;
        }

        public IQueryable<Level> getLevels(InterpoolContainer context)
        {
            return context.Levels;
        }


        public IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context)
        {
            return from f in context.Famous
                      where f.City == city
                      select f;
                    
        }

        public IQueryable<CityProperty> GetCityPropertyByCity(City city, InterpoolContainer context)
        {
            return from c in context.CityPropertySet
                   where c.City == city
                   select c;

        }

        public IQueryable<Suspect> GetSuspectByGame(Game g, InterpoolContainer context)
        {
            return from s in context.Suspects
                   where s.Game_1 == g
                   select s;
        }

        public string GetParameter(string name, InterpoolContainer context)
        {
            var query = from p in context.Parameters
                   where p.ParameterName == name
                   select p.ParameterValue;
            return query.First();
        }


        public void StoreUser(FacebookUserData fbud, InterpoolContainer context)
        {
            // The user may have more information stored in order to use it
            // for future games
            User user = new User();
            user.UserIdFacebook = fbud.userId;
            user.UserTokenFacebook = fbud.oAuth.Token;
            context.AddToUsers(user);
            context.SaveChanges();
        }
    }
}