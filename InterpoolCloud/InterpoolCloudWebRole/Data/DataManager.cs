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

        // Pre: the table Users must have at least 1 user
        public string GetLastUserId(InterpoolContainer context)
        {
            int userId = (from u in context.Users
                        select u.UserId).Max();
            return  (from u in context.Users
                    where u.UserId == userId
                    select u.UserIdFacebook).First();
        }

        public string GetParameter(string name, InterpoolContainer context)
        {
            var query = from p in context.Parameters
                   where p.ParameterName == name
                   select p.ParameterValue;
            return query.First();
        }


        public void StoreUser(User user, InterpoolContainer context)
        {
            bool userExists = context.Users.Where(u => u.UserId == user.UserId).Count() > 0;
            if (userExists)
            {
                User userDB = context.Users.Where(u => u.UserId == user.UserId).First();
                userDB.UserTokenFacebook = user.UserTokenFacebook;
            }
            else
                context.AddToUsers(user);
            context.SaveChanges();
        }

        public Game GetGameByUser(string userIdFaceook, InterpoolContainer conteiner)
        {
            var query = from user in conteiner.Users join game in conteiner.Games 
                        on user.Game equals game 
                        where user.UserIdFacebook == userIdFaceook
                        select game;
            
            return query.First();
        }

        public void StoreSuspect(Suspect suspect, InterpoolContainer context)
        {
            context.AddToSuspects(suspect);
            context.SaveChanges();
        }

        public void SaveChanges(InterpoolContainer context)
        {
            context.SaveChanges();
        }

        public InterpoolContainer GetContainer()
        {
            InterpoolContainer container = new InterpoolContainer();
            return container;
        }


        public oAuthFacebook GetLastUserToken(InterpoolContainer context)
        {
            int userId = (from u in context.Users
                            select u.UserId).Max();
            string token = (from u in context.Users
                            where u.UserId == userId
                            select u.UserTokenFacebook).First();
            return new oAuthFacebook() { Token = token }; 
        }

    }
}