//-----------------------------------------------------------------------
// <copyright file="DataManager.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Data
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Web;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Utilities;

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
        /// <param name="context"> Parameter description for context goes here</param>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public IQueryable<User> GetUserByIdFacebook(InterpoolContainer context, string userIdFacebook)
        {
            return from u in context.Users
                    where u.UserIdFacebook == userIdFacebook
                    select u;
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
            IQueryable<string> res = from u in context.Users
                    where u.UserId == userId
                    select u.UserIdFacebook;
            if (res != null && res.Count() != 0)
            {
                return res.First();
            }
            else
            {
                return string.Empty;
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="name"> Parameter description for name goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetParameter(string name, InterpoolContainer context)
        {
            IQueryable<string> res = from p in context.Parameters
                                      where p.ParameterName == name
                                      select p.ParameterValue;
            if (res != null && res.Count() != 0)
            {
                return res.First();
            }
            else
            {
                return string.Empty;
            }
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
                //// Updates the data of the user
                userDB.UserBirthday = user.UserBirthday;
                userDB.UserCinema = user.UserCinema;
                userDB.UserFirstName = user.UserFirstName;
                userDB.UserGender = user.UserGender;
                userDB.UserHometown = user.UserHometown;
                userDB.UserIdFacebook = user.UserIdFacebook;
                userDB.UserLastName = user.UserLastName;
                userDB.UserLoginId = user.UserLoginId;
                userDB.UserMusic = user.UserMusic;
                userDB.UserPictureLink = user.UserPictureLink;
                userDB.UserTelevision = user.UserTelevision;
                userDB.UserTokenFacebook = user.UserTokenFacebook;
                userDB.UserHometown = user.UserHometown;
                
                context.Detach(user);
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
            if (query != null && query.Count() != 0)
            {
                return query.First();
            }
            else
            {
                throw new Exception();
            }
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
            IQueryable<string> token = from u in context.Users
                            where u.UserId == userId
                            select u.UserTokenFacebook;
            if (token != null && token.Count() != 0)
            {
                return new OAuthFacebook() { Token = token.First() };
            }
            else
            {
                throw new Exception();
            }
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
            //// TODO: game passed as parameter
            var game = this.GetGameByUser(userIdFacebook, container);
            List<Suspect> list = game.PossibleSuspect.ToList();
            list.Add(game.Suspect);
            var suspects = list.AsEnumerable();
            //// var suspects = game.PossibleSuspect.AsEnumerable();
            
            // If the hometown is not empty, gets all the suspects with the selected hometown
            if (fbud.Hometown != null)
            {
                suspects = from hometownSuspects in suspects
                           where (hometownSuspects.SuspectHometown == fbud.Hometown)
                           select hometownSuspects;
            }

            // If the music is not empty, gets all the suspects with the selected music
            if (fbud.Music != null)
            {
                suspects = from musicSuspects in suspects
                           where (musicSuspects.SuspectMusic == fbud.Music)
                           select musicSuspects;
            }

            // If the gender is not empty, gets all the suspects with the selected gender
            if (fbud.Gender != null)
            {
                suspects = from genderSuspects in suspects
                           where (genderSuspects.SuspectGender == fbud.Gender)
                           select genderSuspects;
            }

            // If the cinema is not empty, gets all the suspects with the selected cinema
            if (fbud.Cinema != null)
            {
                suspects = from cinemaSuspects in suspects
                           where (cinemaSuspects.SuspectCinema == fbud.Cinema)
                           select cinemaSuspects;
            }

            // If the television is not empty, gets all the suspects with the selected television
            if (fbud.Television != null)
            {
                suspects = from televisionSuspects in suspects
                           where (televisionSuspects.SuspectTelevision == fbud.Television)
                           select televisionSuspects;
            }

            // If the birthday is not empty, gets all the suspects with the selected birthday
            if (fbud.Birthday != null)
            {
                suspects = from birthdaySuspects in suspects
                           where (birthdaySuspects.SuspectBirthday == fbud.Birthday)
                           select birthdaySuspects;
            }

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

            return Functions.ShuffleList<DataFacebookUser>(listFbudSuspect);
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetUserIdFacebookByLoginId(string userLoginId, InterpoolContainer context)
        {
            bool userExists = context.Users.Where(u => u.UserLoginId == userLoginId).Count() > 0;
            string userIdFacebook;
            if (userExists)
            {
                User user = context.Users.Where(u => u.UserLoginId == userLoginId).First();
                userIdFacebook = user.UserIdFacebook;
            }
            else
            {
                userIdFacebook = string.Empty;
            }

            return userIdFacebook;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <param name="subLevel"> Parameter description for subLevel goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public bool UserHasSubLevel(int userId, int subLevel, InterpoolContainer context)
        {
            var query = from users in context.Users
                        where users.UserId == userId
                        select users.SubLevel;
            int currentSubLevel = -1;
            if (query != null && query.Count() > 0)
            {
                currentSubLevel = (int)query.First();
            }

            return currentSubLevel == subLevel;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="token"> Parameter description for userLoginId goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public User GetUserByToken(string token, InterpoolContainer context)
        {
            var query = from user in context.Users
                        where user.UserTokenFacebook == token
                        select user;
            if (query.Count() > 0)
            {
                return query.First();
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <param name="context"> Parameter description for context goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public DataUserInfo GetUserInfoByLoginId(string userLoginId, InterpoolContainer context)
        {
            DataUserInfo userInfo = new DataUserInfo()
            {
                UserIdFacebook = string.Empty,
                FirstName = string.Empty,
                LastName = string.Empty
            };
            var usersQuery = context.Users.Where(u => u.UserLoginId == userLoginId);
            if (usersQuery.Count() > 0)
            {
                User user = usersQuery.First();
                userInfo.UserIdFacebook = user.UserIdFacebook;
                userInfo.FirstName = user.UserFirstName;
                userInfo.LastName = user.UserLastName;
                bool gameExists = context.Games.Where(game => game.User.UserIdFacebook == user.UserIdFacebook).Count() > 0;
                if (gameExists)
                {
                    userInfo.UserState = UserState.REGISTERED_PLAYING;
                }
                else
                {
                    bool loginRequired = this.UserIsLoginRequired(user.UserTokenFacebook);
                    if (loginRequired)
                    {
                        userInfo.UserState = UserState.REGISTERED_NO_PLAYING_LOGIN_REQUIRED;
                    }
                    else
                    {
                        userInfo.UserState = UserState.REGISTERED_NO_PLAYING;
                    }
                }
            }
            else
            {
                userInfo.UserState = UserState.NO_REGISTERED;
            }

            return userInfo;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="token"> Parameter description for token goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private bool UserIsLoginRequired(string token)
        {
            IFacebookController facebookController = new FacebookController();
            try
            {
                // If it doesn't throw an exception then no login is required
                facebookController.GetUserId(new OAuthFacebook() { Token = token });
                return false;
            }
            catch (Exception)
            {
                return true;
            }

            throw new NotImplementedException();
        }
    }
}