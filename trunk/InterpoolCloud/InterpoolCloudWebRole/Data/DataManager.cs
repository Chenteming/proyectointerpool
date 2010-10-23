
namespace InterpoolCloudWebRole.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Web;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;

    /// <summary>
    /// DataManager IDataManager.
    /// </summary>
    public class DataManager : IDataManager
    {
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<City> GetCities(InterpoolContainer context)
        {    
            return context.Cities;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<Level> GetLevels(InterpoolContainer context)
        {
            return context.Levels;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="city"> Parameter description for city goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<Famous> GetFamousByCity(City city, InterpoolContainer context)
        {
            return from f in context.Famous
                      where f.City == city
                      select f;           
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="city"> Parameter description for city goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<CityProperty> GetCityPropertyByCity(City city, InterpoolContainer context)
        {
            return from c in context.CityPropertySet
                   where c.City == city
                   select c;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="g"> Parameter description for g goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<Suspect> GetSuspectByGame(Game g, InterpoolContainer context)
        {
            return from s in context.Suspects
                   where s.Game_1 == g
                   select s;
        }

        //// Pre: the table Users must have at least 1 user

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetLastUserIdFacebook(InterpoolContainer context)
        {
            int userId = (from u in context.Users
                          select u.UserId).Max();
            return (from u in context.Users
                    where u.UserId == userId
                    select u.UserIdFacebook).First();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="name"> Parameter description for name goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetParameter(string name, InterpoolContainer context)
        {
            var query = from p in context.Parameters
                   where p.ParameterName == name
                   select p.ParameterValue;
            return query.First();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        public void StoreUser(User user, InterpoolContainer context)
        {
            bool userExists = context.Users.Where(u => u.UserIdFacebook == user.UserIdFacebook).Count() > 0;
            if (userExists)
            {
                User userDB = context.Users.Where(u => u.UserIdFacebook == user.UserIdFacebook).First();
                userDB.UserTokenFacebook = user.UserTokenFacebook;
            }
            else
            {
                context.AddToUsers(user);
            }

            context.SaveChanges();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFaceook"> Parameter description for userIdFaceook goes here</param>
        /// <param name="conteiner"> Parameter description for conteiner goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public Game GetGameByUser(string userIdFaceook, InterpoolContainer conteiner)
        {
            var query = from user in conteiner.Users 
                        join game in conteiner.Games 
                        on user.Game.GameId equals game.GameId 
                        where user.UserIdFacebook == userIdFaceook
                        select game;
            
            return query.First();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="suspect"> Parameter description for suspect goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        public void StoreSuspect(Suspect suspect, InterpoolContainer context)
        {
            context.AddToSuspects(suspect);
            //// context.SaveChanges();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        public void SaveChanges(InterpoolContainer context)
        {
           // context.SaveChanges();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public InterpoolContainer GetContainer()
        {
            InterpoolContainer container = new InterpoolContainer();
            return container;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public OAuthFacebook GetLastUserToken(InterpoolContainer context)
        {
            int userId = (from u in context.Users
                            select u.UserId).Max();
            string token = (from u in context.Users
                            where u.UserId == userId
                            select u.UserTokenFacebook).First();
            return new OAuthFacebook() { Token = token }; 
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="entity"> Parameter description for entity goes here</param>
        /// <param name="container"> Parameter description for container goes here</param>
        public void InsertEntity(EntityObject entity, InterpoolContainer container)
        {     
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud goes here</param>
        /// <param name="container"> Parameter description for container goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud, InterpoolContainer container)
        {
            var game = this.GetGameByUser(userIdFacebook, container);
            List<Suspect> list = game.PossibleSuspect.ToList();
            list.Add(game.Suspect);
            var suspects = list.AsEnumerable();
            //// var suspects = game.PossibleSuspect.AsEnumerable();
            
            // If the hometown is not empty, gets all the suspects with the selected hometown
            if (!string.IsNullOrEmpty(fbud.Hometown))
            {
                suspects = from hometownSuspects in suspects
                           where (hometownSuspects.SuspectHometown == fbud.Hometown)
                           select hometownSuspects;
            }

            // If the music is not empty, gets all the suspects with the selected music
            if (!string.IsNullOrEmpty(fbud.Music))
            {
                suspects = from musicSuspects in suspects
                           where (musicSuspects.SuspectMusic == fbud.Music)
                           select musicSuspects;
            }

            // If the gender is not empty, gets all the suspects with the selected gender
            if (!string.IsNullOrEmpty(fbud.Gender))
            {
                suspects = from genderSuspects in suspects
                           where (genderSuspects.SuspectGender == fbud.Gender)
                           select genderSuspects;
            }

            // If the cinema is not empty, gets all the suspects with the selected cinema
            if (!string.IsNullOrEmpty(fbud.Cinema))
            {
                suspects = from cinemaSuspects in suspects
                           where (cinemaSuspects.SuspectCinema == fbud.Cinema)
                           select cinemaSuspects;
            }

            // If the television is not empty, gets all the suspects with the selected television
            if (!string.IsNullOrEmpty(fbud.Television))
            {
                suspects = from televisionSuspects in suspects
                           where (televisionSuspects.SuspectTelevision == fbud.Cinema)
                           select televisionSuspects;
            }

            // If the birthday is not empty, gets all the suspects with the selected birthday
            if (!string.IsNullOrEmpty(fbud.Birthday))
            {
                suspects = from birthdaySuspects in suspects
                           where (birthdaySuspects.SuspectBirthday == fbud.Birthday)
                           select birthdaySuspects;
            }

            //// TODO: filter with all the remaining fields

            DataFacebookUser fbudSuspect;
            List<DataFacebookUser> listFbudSuspect = new List<DataFacebookUser>();
            foreach (Suspect suspect in suspects)
            {
                fbudSuspect = new DataFacebookUser();

                fbudSuspect.IdFriend = suspect.SuspectFacebookId;
                fbudSuspect.FirstName = suspect.SuspectFirstName;
                fbudSuspect.LastName = suspect.SuspectLastName;
                fbudSuspect.Gender = suspect.SuspectGender;
                fbudSuspect.Hometown = suspect.SuspectHometown;
                fbudSuspect.Music = suspect.SuspectMusic;
                fbudSuspect.Television = suspect.SuspectTelevision;
                fbudSuspect.Cinema = suspect.SuspectCinema;
                fbudSuspect.Birthday = suspect.SuspectBirthday;
                fbudSuspect.PictureLink = suspect.SuspectPicLInk;
                
                listFbudSuspect.Add(fbudSuspect);
            }

            return listFbudSuspect;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetUserIdFacebookByLoginId(string userLoginId, InterpoolContainer context)
        {
            var query = from user in context.Users
                        where user.UserLoginId == userLoginId
                        select user.UserIdFacebook;
            //// TODO: Check if it has elements
            return query.First();
        }
    }
}