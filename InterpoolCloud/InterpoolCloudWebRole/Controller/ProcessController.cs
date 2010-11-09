//-----------------------------------------------------------------------
// <copyright file="ProcessController.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Data.Objects.DataClasses;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Datatypes;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Utilities;
    using System.ServiceModel;

    /// <summary>
    /// Class Description ProcessController
    /// </summary>
    public class ProcessController : IProcessController
    {
        /// <summary>
        /// Store for the property
        /// </summary>
        private InterpoolContainer container;

        /// <summary>
        /// Initializes a new instance of the ProcessController class.</summary>
        /// <param name="container"> Parameter description for container goes here</param>
        public ProcessController(InterpoolContainer container)
        {
            this.container = container;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public InterpoolContainer GetContainer()
        {
            return this.container;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataCity GetCurrentCity(string userIdFacebook)
        {
            NodePath node = this.GetCurrentNode(userIdFacebook);
            DataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, this.container);
            if (node != null)
            {
                DataCity dataCity = new DataCity();
                dataCity.NameCity = node.City.CityName;
                dataCity.NameFileCity = node.City.NameFile;
                dataCity.CurrentDate = game.CurrentTime;
                dataCity.DeadLine = game.DeadLine;
                dataCity.Left = node.City.Longitud;
                dataCity.Top = node.City.Latitud;
                dataCity.GameInfo = this.GetGameInfo(game, GameState.PL);
                return dataCity;
            }

            return null;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public List<DataCity> GetPossibleCities(string userIdFacebook)
        {
            NodePath node = this.GetCurrentNode(userIdFacebook);
            List<DataCity> result = new List<DataCity>();
            foreach (City c in node.PossibleCities)
            {
                DataCity dataCity = new DataCity();
                dataCity.NameCity = node.City.CityName;
                dataCity.NameFileCity = node.City.NameFile;
                result.Add(dataCity);
            }

            return result;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numClue"> Parameter description for numClue goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataFamous GetCurrentFamous(string userIdFacebook, int numClue)
        {
            NodePath node = this.GetCurrentNode(userIdFacebook);
            if (node != null)
            {
                DataFamous dataFamous = new DataFamous();
                dataFamous.NameFamous = node.Clue.ElementAt(numClue).Famous.FamousName;
                dataFamous.FileFamous = node.Clue.ElementAt(numClue).Famous.NameFileFamous;
                return dataFamous;
            }

            return null;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public NodePath GetCurrentNode(string userIdFacebook)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, this.container);
            foreach (NodePath node in game.NodePath)
            {
                if (node.NodePathCurrent)
                {
                    return node;
                }
            }

            return null;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public NodePath GetNextNode(string userIdFacebook)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, this.container);
            bool next = false;
            foreach (NodePath node in game.NodePath)
            {
                if (next)
                {
                    return node;
                }

                if (node.NodePathCurrent)
                {
                    next = true;
                }
            }

            return null;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        [FaultContract(typeof(FaultException))]
        public void StartGame(string userIdFacebook)
        {
            // this is only the structs that we should follow
            try
            {
                bool gameExists = this.container.Games.Where(game => game.User.UserIdFacebook == userIdFacebook).Count() != 0;

                if (gameExists)
                {
                    return;
                }
                User user;
                IQueryable<User> iUser = this.container.Users.Where(u => u.UserIdFacebook == userIdFacebook);
                if (iUser != null && iUser.Count() != 0)
                {
                    user = iUser.First();
                }
                else
                {
                    this.RegisterLog("StartGame: There is no user with that id", null, "error", "0");
                    throw new FaultException(new FaultReason("There is no user with that id"), new FaultCode("ESG1"));
                }
                //// 1 the trip is built to be followed by user
                // TODO borrar
                Game newGame = null;
                try
                {
                    newGame = this.BuiltTravel(user);
                }
                catch (Exception e)
                {
                    this.RegisterLog("BuiltTravel", e, "error", user.UserLoginId);
                    throw new FaultException(new FaultReason(e.Message), new FaultCode("ESG2"));
                }
                //// 2 Get suspects
                try
                {
                    this.GetSuspects(newGame);
                }
                catch (Exception e)
                {
                    this.RegisterLog("GetSuspects", e, "error", user.UserLoginId);
                    throw new FaultException(new FaultReason(e.Message), new FaultCode("ESG3"));
                }

                //// 3 Create clues
                try
                {
                    this.CreateClue(newGame);
                }
                catch (Exception e)
                {
                    this.RegisterLog("CreateClue", e, "error", user.UserLoginId);
                    throw new FaultException(new FaultReason(e.Message), new FaultCode("ESG4"));
                }

                //// set the date to monday
                DateTime currentTime = new DateTime(2010, 01, 01);
                newGame.CurrentTime = currentTime.AddDays(3);
                //// set the hour
                newGame.CurrentTime = newGame.CurrentTime.AddHours(8);
   
                try
                {
                    this.CalculateDeadLine(newGame);
                }
                catch (Exception e)
                {
                    this.RegisterLog("CalculateDeadLine", e, "error", user.UserLoginId);
                    throw new FaultException(new FaultReason(e.Message), new FaultCode("ESG5"));
                }
   
                // newGame.DeadLine = currentTime;
                this.container.AddToGames(newGame);
                this.container.SaveChanges();
            }
            catch (FaultException e)
            {
                throw new FaultException(new FaultReason(e.Message), new FaultCode("ESG6"));
            }
        }

        /// <summary>
        /// Get suspects 
        /// </summary>
        /// <param name="newGame"> Parameter description for newGame goes here</param>
        [FaultContract(typeof(FaultException))]
        public void GetSuspects(Game newGame)
        {
            //// In this operation we should go to find the possibles suspects, and asign the suspect

            IFacebookController facebookController = new FacebookController();
            IDataManager dm = new DataManager();
            OAuthFacebook auth = new OAuthFacebook() { Token = newGame.User.UserTokenFacebook };
            bool specialGame = dm.UserHasSubLevel(newGame.User.UserId, Constants.NumberSubLevels, this.container);
            Suspect nonFriendSuspect = null;
            if (specialGame)
            {
                this.GetSuspectsFromDatabase(newGame);
                //// If exists an user in the database who's not a friend of the current user
                if (newGame.Suspect != null)
                {
                    //// Stores the suspect chosen in a variable to be used later
                    nonFriendSuspect = newGame.Suspect;
                }

                int limitSuspects = Constants.MaxSuspects - newGame.PossibleSuspect.Count();
                if (limitSuspects > 0)
                {
                    facebookController.DownloadFacebookUserData(auth, newGame, limitSuspects, this.container);
                }
            }
            else
            {
                facebookController.DownloadFacebookUserData(auth, newGame, Constants.MaxSuspects, this.container);
            }

            //// If the suspect has not be chosen, it chooses someone
            if (newGame.Suspect == null)
            {
                int numberSuspect = new Random().Next(0, newGame.PossibleSuspect.Count - 1);
                List<Suspect> possibleSuspects = newGame.PossibleSuspect.ToList();
                Suspect suspect = possibleSuspects[numberSuspect];
                newGame.PossibleSuspect.Remove(suspect);
                newGame.Suspect = suspect;
            }

            List<string> list = new List<string>();

            list.Add("SuspectFirstName");
            list.Add("SuspectFacebookId");
            list.Add("SuspectLastName");
            list.Add("SuspectGender");
            list.Add("SuspectPicLInk");

            this.CreateHardCodeSuspects(newGame, list);

            if (specialGame && nonFriendSuspect != null)
            {
                //// If the suspect stays the same then nothing must be done
                if (newGame.Suspect.SuspectFacebookId != nonFriendSuspect.SuspectFacebookId)
                {
                    Suspect oldSuspect = newGame.Suspect;
                    newGame.PossibleSuspect.Add(oldSuspect);
                    newGame.PossibleSuspect.Remove(nonFriendSuspect);
                    newGame.Suspect = nonFriendSuspect;
                }
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public Game BuiltTravel(User user)
        {
            Game newGame = new Game();
            user.Game = newGame;

            newGame.FilterSuspect = new Filter();

            IDataManager dm = new DataManager();

            List<int> selectedCities = new List<int>();
            NodePath node;
            City next;
            Random random = new Random();

            int maxNumber = Int32.Parse(dm.GetParameter(Parameters.AmountCities, this.container));
            int nextCity = 0;
            bool find = false;
            ////TODO, maybe the amount of NodePath should be a param in the data base
            for (int i = 0; i < 4; i++)
            {
                node = new NodePath();
                node.Famous = new EntityCollection<Famous>();
                for (int j = 0; j < 3; j++)
                {
                    find = false;
                    do
                    {

                        nextCity = 1 > maxNumber ? 1 : random.Next(1, maxNumber);

                        IQueryable<City> iCity = dm.GetCities(this.container).Where(c => c.CityNumber == nextCity);
                        if (iCity != null && iCity.Count() != 0)
                        {
                            //// start to select cities

                            next = iCity.First();
                        }
                        else
                        {
                            this.RegisterLog("BuiltTravel: There is no next city", null, "error", "0");
                            throw new GameException("There is no next city", new Exception());
                        }
                        if (!selectedCities.Contains(next.CityNumber))
                        {
                            find = true;
                            if (j == 0)
                            {
                                node.City = next;
                                List<Famous> listFamous = next.Famous.ToList<Famous>();
                                foreach (Famous f in listFamous)
                                {
                                    node.Famous.Add(f);
                                }
                            }
                            else
                            {
                                node.PossibleCities.Add(next);
                            }

                            selectedCities.Add(next.CityNumber);
                        }
                    } 
                    while (!find);
                }

                //// the current node in the first time is 0
                node.NodePathCurrent = i == 0;
                node.NodePathOrder = i;

                newGame.NodePath.Add(node);
                ////conteiner.AddToNodePaths(node);
            }

            return newGame;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="fbud"> Parameter description for fbud goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataListFacebookUser FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            IDataManager dm = new DataManager();
            DataListFacebookUser dataListFacebookUser = new DataListFacebookUser();
            ////InterpoolContainer container = dm.GetContainer();
            dataListFacebookUser.ListFacebookUser = dm.FilterSuspects(userIdFacebook, fbud, this.container);
            dataListFacebookUser.CurrentDate = this.RestTime(dm.GetGameByUser(userIdFacebook, this.container), Constants.FilterSuspect);
            Game game = dm.GetGameByUser(userIdFacebook, container);
            
            this.SaveFilters(game, fbud);
            
            GameState state = game.CurrentTime < game.DeadLine ? GameState.PL : GameState.LOSE_TO;
            dataListFacebookUser.GameInfo = this.GetGameInfo(game, state);

            return dataListFacebookUser;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="nameNextCity"> Parameter description for nameNextCity goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataCity Travel(string userIdFacebook, string nameNextCity)
        {
            DataCity datacity = new DataCity();
            DataManager dm = new DataManager();
            NodePath node = this.GetCurrentNode(userIdFacebook);
            NodePath nextNode = this.GetNextNode(userIdFacebook);
            Game game = dm.GetGameByUser(userIdFacebook, this.container);
            if (!nextNode.City.CityName.Equals(nameNextCity))
            {
                datacity.NameCity = node.City.CityName;
                datacity.NameFileCity = node.City.NameFile;
                datacity.CurrentDate = this.RestTime(game, Constants.TravelWrong);
                datacity.DeadLine = game.DeadLine;
                datacity.CityNumber = -1;
                return datacity;
            }

            node.NodePathCurrent = false;
            nextNode.NodePathCurrent = true;
            this.container.SaveChanges();
            datacity.NameCity = nextNode.City.CityName;
            datacity.NameFileCity = nextNode.City.NameFile;
            datacity.CityNumber = nextNode.NodePathOrder + 1;
            
            datacity.CurrentDate = this.RestTime(dm.GetGameByUser(userIdFacebook, this.container), Constants.TravelGood);

            GameState state = game.CurrentTime < game.DeadLine ? GameState.PL : GameState.LOSE_TO;
            datacity.GameInfo = this.GetGameInfo(game, state);

            return datacity;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="userIdFacebookSuspect"> Parameter description for userIdFacebookSuspect goes here</param>
        [FaultContract(typeof(FaultException))]
        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, this.container);
            if (game.OrderOfArrest != null)
            {
                throw new FaultException(new FaultReason("error_existOneOrderOfArrest"), new FaultCode("ESG6"));
            }

            Suspect suspect = null;

            if (game.Suspect.SuspectFacebookId == userIdFacebookSuspect)
            {
                suspect = game.Suspect;
            }
            else
            {
                IEnumerable<Suspect> iSuspect = game.PossibleSuspect.Where(s => s.SuspectFacebookId == userIdFacebookSuspect);
                if (iSuspect != null && iSuspect.Count() != 0)
                {
                    suspect = iSuspect.First();
                }
                else
                {
                    this.RegisterLog("EmitOrderOfArresst: There is no Suspect with that id", null, "error", "0");
                    throw new FaultException(new FaultReason("There is no Suspect with that id"), new FaultCode("ESG6"));
                }
            }

            OrderOfArrest order = new OrderOfArrest();
            order.Suspect = suspect;
            this.container.AddToOrdersOfArrest(order);
            game.OrderOfArrest = order;

            this.container.SaveChanges();
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userId"> Parameter description for userId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public List<DataCity> GetCities(string userId)
        {
            ////TODO: order random
            IDataManager dm = new DataManager();
            NodePath node = this.GetNextNode(userId);
            if (node == null)
            {
                return null;
            }

            List<DataCity> cities = new List<DataCity>();
            DataCity datacity;
            foreach (City c in node.PossibleCities)
            {
                datacity = new DataCity();
                datacity.Left = c.Longitud;
                datacity.Top = c.Latitud;
                datacity.NameCity = c.CityName;
                datacity.NameFileCity = c.NameFile;
                cities.Add(datacity);
            }

            datacity = new DataCity();
            datacity.Left = node.City.Longitud;
            datacity.Top = node.City.Latitud;
            datacity.NameCity = node.City.CityName;
            datacity.NameFileCity = node.City.NameFile;
            cities.Add(datacity);

            cities = Functions.ShuffleList(cities);

            return cities;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userIdFacebook"> Parameter description for userIdFacebook goes here</param>
        /// <param name="numFamous"> Parameter description for numFamous goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataClue GetClueByFamous(string userIdFacebook, int numFamous)
        {
            IDataManager dm = new DataManager();
            User user;
            IQueryable<User> iUser = dm.GetUserByIdFacebook(this.container, userIdFacebook);
            if (iUser != null && iUser.Count() != 0)
            {
                user = iUser.First();
            }
            else
            {
                this.RegisterLog("GetClueByFamous: There is no User with that id", null, "error", "0");
                throw new FaultException(new FaultReason("There is no User with that id"), new FaultCode("ESG7"));
            }
            NodePath node = this.GetCurrentNode(userIdFacebook);
            DataClue clue;

           

            if (node != null)
            {
                clue = new DataClue();
                //// TODO make a Constant
                clue.CurrentDate = this.RestTime(dm.GetGameByUser(userIdFacebook, this.container), Constants.QuestionFamous);
                if (user.Level.LevelNumber > 5)
                {
                    clue.Clue = node.Clue.ElementAt(numFamous).ClueContent;
                }
                else if (user.Level.LevelNumber > 2)
                {
                    int index = 1 - numFamous;
                    index += 3;
                    index = index % 3;
                    clue.Clue = node.Clue.ElementAt(index).ClueContent;
                }
                else
                {
                    clue.Clue = node.Clue.ElementAt(2 - numFamous).ClueContent;
                }

                if (node.NodePathOrder == (Constants.NumberLastCity - 1))
                {
                    // last city
                    // TODO make a Constant
                    if (numFamous == 1)
                    {
                        Game game = dm.GetGameByUser(userIdFacebook, this.container);
                        bool arrest = this.Arrest(game, clue);
                        return clue;
                    }
                }
                else
                {
                    clue.States = GameState.PL;
                }

                
                return clue;
            }

            clue = new DataClue();
            clue.CurrentDate = this.RestTime(dm.GetGameByUser(userIdFacebook, this.container), Constants.QuestionFamous);
            clue.States = GameState.PL;
            clue.Clue = String.Empty;
            return clue;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="idLogin"> Parameter description for idLogin goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetLastUserIdFacebook(string idLogin)
        {
            IDataManager dm = new DataManager();
            //// This wont work for multiuser game
            return dm.GetLastUserIdFacebook(dm.GetContainer());
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="game"> The current game</param> 
        /// <param name="privatesProperties"> Parameter description for privatesProperties goes here</param>
        public void CreateHardCodeSuspects(Game game, List<string> privatesProperties)
        {
            List<Suspect> hardCodeSuspects = new List<Suspect>();

            Suspect bigSuspect = game.Suspect;

            Suspect hardCode;
            PropertyInfo info;
            
            List<long> idsHardCoded = new List<long>();
            List<HardCodedSuspect> sameGenders = this.container.HardCodedSuspects.Where(h => h.HardCodedSuspectGender.Equals(bigSuspect.SuspectGender)).ToList();
            List<HardCodedSuspect> hardCodedList = new List<HardCodedSuspect>();

            Random rand = new Random();

            //// Step 0: choose some hardcode suspect with the same gander

            //// Pre: supose that we have more than 2 hardcoded suspects per gender
            int amountHardCodedSuspects = Constants.AmountHardCodeSuspects;
            int amountSameGender = (int)Math.Min(sameGenders.Count, amountHardCodedSuspects);
            int a = amountSameGender / 2;
            amountSameGender = a > amountSameGender ? a : rand.Next(a, amountSameGender);

            int count = 0;
            int index = 0;
            HardCodedSuspect hard = null;
            do
            {
                index = 0 > sameGenders.Count - 1 ? 0 : rand.Next(0, sameGenders.Count - 1);
                hard = sameGenders.ElementAt(index);
                if (!idsHardCoded.Contains(hard.HardCodedSuspecId))
                {
                    idsHardCoded.Add(hard.HardCodedSuspecId);
                    hardCodedList.Add(hard);
                    count++;
                }
            }
            while (count < amountSameGender);

            int prob = rand.Next(0, 3);
            int x = -1;
            if (prob == 3)
            {
                x = 0 > hardCodedList.Count - 1 ? 0 : rand.Next(0, hardCodedList.Count - 1);   
            }
            //// Step 1: choose the rest of the hardcoded suspects

            List<HardCodedSuspect> restHarCoded = this.container.HardCodedSuspects.Where(p => !idsHardCoded.Contains(p.HardCodedSuspecId)).ToList();
            do
            {
                index = 0 > restHarCoded.Count - 1 ? 0 : rand.Next(0, restHarCoded.Count - 1);
                hard = restHarCoded.ElementAt(index);
                if (!idsHardCoded.Contains(hard.HardCodedSuspecId))
                {
                    idsHardCoded.Add(hard.HardCodedSuspecId);
                    hardCodedList.Add(hard);
                    count++;
                }
            } 
            while (count < amountHardCodedSuspects);

            //// Step 2: create the new hardcoded suspcts 

            string newValue;
            string propHard;
            for (int i = 0; i < Constants.AmountHardCodeSuspects; i++)
            {
                hardCode = new Suspect();
                foreach (string prop in privatesProperties)
                {
                    propHard = "HardCoded" + prop;
                    
                    hard = (HardCodedSuspect)hardCodedList.ElementAt(i);
                    info = hard.GetType().GetProperty(propHard);
                    if (info != null)
                    {
                        newValue = (string)info.GetValue(hard, null);
                    }
                    else
                    {
                        ////TODO only for null values
                        newValue = string.Empty;
                    }

                    info = hardCode.GetType().GetProperty(prop);
                    info.SetValue(hardCode, newValue, null);
                }
                hardCodeSuspects.Add(hardCode);
            }

            //// Step 3: set the suspect's property to new hard coded suspect

            var properties = typeof(Suspect).GetProperties();
            index = 0;
            count = 0;
            Suspect auxSuspect;
            string propS;
            string propValue;
            
            foreach (var property in properties)
            {
                string propType = property.PropertyType.Name;
                propS = property.Name;
                if ("String".Equals(propType) && !privatesProperties.Contains(propS))
                {
                    do
                    {
                        index = 0 > amountHardCodedSuspects ? 0 : rand.Next(0, amountHardCodedSuspects);
                        auxSuspect = hardCodeSuspects.ElementAt(index);

                        propS = property.Name;
                        info = auxSuspect.GetType().GetProperty(propS);
                        propValue = (string)info.GetValue(bigSuspect, null);

                        if (propValue != null)
                        {
                            info.SetValue(auxSuspect, propValue, null);
                            count++;
                        }
                    }
                    while (count < 3);
                }
            }

            //// Step 4: complete the info, 

            foreach (Suspect hardCodeS in hardCodeSuspects)
            {
                foreach (var property in properties)
                {
                    string propType = property.PropertyType.Name;
                    if ("String".Equals(propType))
                    {
                        string prop = property.Name;
                        info = hardCodeS.GetType().GetProperty(prop);
                        string value = (string)info.GetValue(hardCodeS, null);
                        if (!privatesProperties.Contains(prop) && null == value)
                        {
                            index = 0 > game.PossibleSuspect.Count - 1 ? 0 : rand.Next(0, game.PossibleSuspect.Count - 1);
                            Suspect realSuspect = game.PossibleSuspect.ToList().ElementAt(index);
                            newValue = (string)info.GetValue(realSuspect, null);
                                                      
                            info = hardCodeS.GetType().GetProperty(prop);
                            info.SetValue(hardCodeS, newValue, null);
                        }
                    }
                }
            }
           
            //// Step 5: persist
            if (prob == 3)
            {
                this.container.AddToSuspects(game.Suspect);
            }

            int ind = 0;
            foreach (Suspect s in hardCodeSuspects)
            {
                this.container.AddToSuspects(s);
                if (ind == x)
                {
                    game.PossibleSuspect.Add(bigSuspect);
                    bigSuspect = hardCodeSuspects[ind];
                    game.Suspect = bigSuspect;
                }
                else
                {
                    game.PossibleSuspect.Add(s);
                }
                ind++;
            }

            if (this.CheckConsistencySuspect(game))
            {
                this.RegisterLog("CreateHardCodeSuspects", null, "ok", game.User.UserLoginId);
            }
            else
            {
                this.RegisterLog("error_hardCodedSuspectConsistencia", null, "error", game.User.UserLoginId);
                throw new FaultException(new FaultReason("error_hardCodedSuspectConsistencia"), new FaultCode("ESG8"));
            }
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="newGame"> Parameter description for newGame goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public bool CheckConsistencySuspect(Game newGame)
        {
            var query = from s1 in newGame.PossibleSuspect
                        join s2 in newGame.PossibleSuspect
                        on s1.SuspectTelevision equals s2.SuspectTelevision
                        where s1.SuspectId != s2.SuspectId && s1.SuspectHometown == s2.SuspectHometown
                            && s1.SuspectCinema == s2.SuspectCinema && s1.SuspectBirthday == s2.SuspectBirthday
                            && s1.SuspectMusic == s2.SuspectMusic
                        select s1;
            if (query.Count() > 0)
            {
                return false;
            }
            
            return true;
        }

        //// TODO: delete if GetUserInfo works fine
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        public string GetUserIdFacebook(string userLoginId)
        {
            IDataManager dm = new DataManager();
            return dm.GetUserIdFacebookByLoginId(userLoginId, dm.GetContainer());
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="userLoginId"> Parameter description for userLoginId goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        [FaultContract(typeof(FaultException))]
        public DataUserInfo GetUserInfo(string userLoginId)
        {
            IDataManager dm = new DataManager();
            return dm.GetUserInfoByLoginId(userLoginId, dm.GetContainer());
        }

        [FaultContract(typeof(FaultException))]
        /* to consider: 
         * - all clues maybe have characteristic of the suspect 
         * - first clue is final
         * - second clue is dynamic
         * - third clue only have news of the famous*/

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="g"> Parameter description for g goes here</param>
        private void CreateClue(Game g)
        {
            DataManager dm = new DataManager();

            /* define the number of characteristics of the suspect by city */
            int[] amountCharacteristicsSuspects = new int[3];
            amountCharacteristicsSuspects[0] = 1; 
            amountCharacteristicsSuspects[1] = 2; 
            amountCharacteristicsSuspects[2] = 2;

            /* built the structure for the characteristics that I will put on each clue */
            bool[] characterSuspect = new bool[5];
            characterSuspect[0] = true;
            characterSuspect[1] = true;
            characterSuspect[2] = true;
            characterSuspect[3] = true;
            characterSuspect[4] = true;
            
            /* iterates over the NodePath of the game */
            int i;
            NodePath cnp;
            IEnumerable<NodePath> currentNodePath;
            int rnd;
            int characteristicsSuspect;
            string newFamous = String.Empty;
            Random r;
            Famous famous;
            for (i = 0; i < Constants.NumberLastCity - 1; i++)
            {
                /* get the Current NodePath  */
                IEnumerable<NodePath> iCnp = g.NodePath.Where(cp => cp.NodePathOrder == i);
                if (iCnp != null && iCnp.Count() != 0)
                {
                    cnp = iCnp.First();
                }
                else
                {
                    this.RegisterLog("CreateClue: There is no NodePath with this order", null, "error", "0");
                    throw new FaultException(new FaultReason("There is no NodePath with this order"), new FaultCode("ESG9"));
                }
                r = new Random();
                /* get the amount of caracteristic of the suspect by NodePath  */
                rnd = r.Next(0, 3);
                while (amountCharacteristicsSuspects[rnd] == -1)
                {
                    rnd = r.Next(0, 3);
                }

                characteristicsSuspect = amountCharacteristicsSuspects[rnd];
                amountCharacteristicsSuspects[rnd] = -1;
                /* get the next city by NodePath */
                City nextCity = this.NextCity(g, cnp);
                /* if nextCity is null, break the cicle */
                if (nextCity == null)
                {
                    break;
                }

                /* build the last clue */
                Clue c3 = new Clue();
                /* set the city */
                c3.City = cnp.City;

                /* if i have to put characteristics on the clue of the suspect */
                if (cnp.Famous != null && cnp.Famous.Count() != 0)
                {
                    famous = cnp.Famous.First();
                }
                else
                {
                    this.RegisterLog("CreateClue: There is no Famous in this node", null, "error", "0");
                    throw new FaultException(new FaultReason("There is no Famous in this node"), new FaultCode("ESG10"));
                }
                
                c3.Famous = famous;
                
                if (characteristicsSuspect != 0)
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        newFamous = famous.New.First().NewContent == String.Empty ? dm.GetParameter(Parameters.DefaultFamousClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueFamousContent, this.container) + " '" + famous.New.First().NewContent + "'.";
                        c3.ClueContent = this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect) + " " + newFamous;
                    }
                    else
                    {
                        c3.ClueContent = this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect);
                    }

                    characteristicsSuspect--;
                }

                /* build the second clue */
                Clue c2 = new Clue();

                /* get the dynamic cityProperty for the nextCity */
                var queryCity = nextCity.CityProperty.Where(qcp => qcp.Dyn == true);
                CityProperty cpd = null;
                if (queryCity.Count() != 0 && queryCity.First() != null)
                {
                    cpd = queryCity.First();
                }

                /* set de city */
                c2.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                famous = cnp.Famous.ElementAt(1);
                c2.Famous = famous;
                string dynProperty = String.Empty;
                if (cpd != null)
                {
                    dynProperty  = cpd.CityPropertyContent == string.Empty ? dm.GetParameter(Parameters.DefaultClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueContent, this.container) + " ' " + cpd.CityPropertyContent + " '.";
                }
                
                if (characteristicsSuspect != 0)
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        newFamous = famous.New.First().NewContent == String.Empty ? dm.GetParameter(Parameters.DefaultFamousClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueFamousContent, this.container) + " '" + famous.New.First().NewContent + "'.";
                        c2.ClueContent = dynProperty + " " + this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect) + " " + newFamous;
                    }
                    else
                    {
                        c2.ClueContent = dynProperty + " " + this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect);
                    }

                    characteristicsSuspect--;
                }
                else
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        newFamous = famous.New.First().NewContent == String.Empty ? dm.GetParameter(Parameters.DefaultFamousClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueFamousContent, this.container) + " '" + famous.New.First().NewContent + "'.";
                        c2.ClueContent = dynProperty + " " + newFamous;
                    }
                    else
                    {
                        c2.ClueContent = dynProperty;
                    }
                }

                /* build the second clue */
                Clue c1 = new Clue();

                /* get the static cityProperty for the nextCity */
                CityProperty cps;
                IEnumerable<CityProperty> iCps = nextCity.CityProperty.Where(qcs => qcs.Dyn == false);
                if (iCps != null && iCps.Count() != 0)
                {
                    cps = iCps.First();
                }
                else
                {
                    this.RegisterLog("CreateClue: There is no static CityProperty for next City", null, "error", "0");
                    throw new FaultException(new FaultReason("There is no dynamic CityProperty for next City"), new FaultCode("ESG10"));
                }
               

                /* set de city */
                c1.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                string staticProperty = cps == null ? string.Empty : cps.CityPropertyContent;
                famous = cnp.Famous.ElementAt(2);
                c1.Famous = famous;
                if (characteristicsSuspect != 0)
                {
                    if (famous.New.Count() != 0 && famous.New.First().NewContent != null)
                    {
                        newFamous = famous.New.First().NewContent == String.Empty ? dm.GetParameter(Parameters.DefaultFamousClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueFamousContent, this.container) + " '" + famous.New.First().NewContent + "'.";
                        c1.ClueContent = staticProperty + " " + this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect) + " " + newFamous;
                    }
                    else
                    {
                        c2.ClueContent = staticProperty + " " + this.GetRandomCharacteristicSuspect(g.Suspect, characterSuspect);
                    }

                    characteristicsSuspect--;
                }
                else
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        newFamous = famous.New.First().NewContent == String.Empty ? dm.GetParameter(Parameters.DefaultFamousClueContent, this.container) : dm.GetParameter(Parameters.PreprefixClueFamousContent, this.container) + " '" + famous.New.First().NewContent + "'.";
                        c1.ClueContent = staticProperty + " " + newFamous;
                    }
                    else
                    {
                        c1.ClueContent = staticProperty;
                    }
                }

                /* Add clue for the NodePath in order*/
                cnp.Clue.Add(c1);
                cnp.Clue.Add(c2);
                cnp.Clue.Add(c3);
            }

            currentNodePath = g.NodePath.Where(cp => cp.NodePathOrder == Constants.NumberLastCity - 1);
            if (currentNodePath != null && currentNodePath.Count() != 0)
            {
                cnp = currentNodePath.First();
            }
            else
            {
                this.RegisterLog("CreateClue: There is no NodePath with this order", null, "error", "0");
                throw new FaultException(new FaultReason("There is no NodePath with this order"), new FaultCode("ESG10"));
            }
            /* build the clues for the last city*/
            Clue lastClue1 = new Clue();
            /* set de city */
            lastClue1.City = cnp.City;
            famous = cnp.Famous.ElementAt(0);
            lastClue1.Famous = famous;
            /* this is in the class parameters  */
            lastClue1.ClueContent = dm.GetParameter(Parameters.LastClue1Esp, this.container);

            Clue lastClue2 = new Clue();
            /* set de city */
            lastClue2.City = cnp.City;
            famous = cnp.Famous.ElementAt(1);
            lastClue2.Famous = famous;
            /* this is in the class parameters  */
            lastClue2.ClueContent = dm.GetParameter(Parameters.LastClue2Esp, this.container);

            Clue lastClue3 = new Clue();
            /* set de city */
            lastClue3.City = cnp.City;
            famous = cnp.Famous.ElementAt(2);
            lastClue3.Famous = famous;
            /* this is in the class parameters  */
            lastClue3.ClueContent = dm.GetParameter(Parameters.LastClue3Esp, this.container);

            /* add clues to nodepath */
            cnp.Clue.Add(lastClue1);
            cnp.Clue.Add(lastClue2);
            cnp.Clue.Add(lastClue3);            
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="g"> Parameter description for g goes here</param>
        /// <param name="currentNodePath"> Parameter description for currentNodePath goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private City NextCity(Game g, NodePath currentNodePath)
        {
            /* get the order for the next NodePath */
            int orderNodePath = -1;
            orderNodePath = currentNodePath.NodePathOrder + 1;

            /* if the last nodePath of the game return null */
            if (Constants.NumberLastCity < orderNodePath)
            {
                return null;
            }

            /* get the next NodePath */
            IEnumerable<NodePath> nextNodePath = from nodePath in g.NodePath
                                                 where nodePath.NodePathOrder == orderNodePath
                                                 select nodePath;
            if (nextNodePath != null && nextNodePath.Count() != 0)
            {
                return nextNodePath.First().City;
            }
            else
            {
                this.RegisterLog("NextCity: There is no nextCity in this node", null, "error", "0");
                throw new FaultException(new FaultReason("There is no nextCity in this node"), new FaultCode("ESG10"));
            }
            
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="s"> Parameter description for s goes here</param>
        /// <param name="csuspect"> Parameter description for csuspect goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private string GetRandomCharacteristicSuspect(Suspect s, bool[] csuspect)
        {
            /* get the random index for the characteristic of the suspect */
            Random rnd = new Random();
            IDataManager dm = new DataManager();
            int indexRandom = rnd.Next(0, 5);
            while (!csuspect[indexRandom])
            {
                indexRandom = rnd.Next(0, 5);
            }

            /* set flase in the structure of characteristics of the suspects */
            csuspect[indexRandom] = false;

            /* according to the index, the characteristic choose */
            switch (indexRandom)
            {
                /* faltan definir las características 2, 3 y 4*/
                case 0:
                    return s.SuspectCinema == string.Empty ? dm.GetParameter(Parameters.PrefixNoCharacteristicSuspectCinema, this.container) : dm.GetParameter(Parameters.PrefixCharacteristicSuspectCinema, this.container) + s.SuspectCinema + ".";
                    
                case 1:
                    return s.SuspectMusic == string.Empty ? dm.GetParameter(Parameters.PrefixNoCharacteristicSuspectMusic, this.container) : dm.GetParameter(Parameters.PrefixCharacteristicSuspectMusic, this.container) + s.SuspectMusic + ".";

                case 2:
                    return s.SuspectBirthday == string.Empty ? dm.GetParameter(Parameters.PrefixNoCharacteristicSuspectBirthday, this.container) : dm.GetParameter(Parameters.PrefixCharacteristicSuspectBirthday, this.container) + s.SuspectBirthday.ToString() + ".";

                case 3:
                    return s.SuspectHometown == string.Empty ? dm.GetParameter(Parameters.PrefixNoCharacteristicSuspectHomeTown, this.container) : dm.GetParameter(Parameters.PrefixCharacteristicSuspectHomeTown, this.container) + s.SuspectHometown + ".";

                case 4:
                    return s.SuspectTelevision == string.Empty ? dm.GetParameter(Parameters.PrefixNoCharacteristicSuspectTV, this.container) : dm.GetParameter(Parameters.PrefixCharacteristicSuspectTV, this.container) + s.SuspectTelevision + ".";
                default:
                    return string.Empty;
            }
        }

        /**
         * summary This function is invoque by the controller when the user reaches the last city
         * 
         * */

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="game"> Parameter description for game goes here</param>
        /// <param name="clue"> Parameter description for clue goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private bool Arrest(Game game, DataClue clue)
        //// TODO, change to private
        {
            // the user make the order of arrest
            if (game.OrderOfArrest != null)
            {
                if (game.Suspect.SuspectFacebookId == game.OrderOfArrest.Suspect.SuspectFacebookId)
                {
                    //// the order is for the guillty, the user win
                    //// TODO level and score
                    ////User user = game.User;
                    ////Level level = user.LevelReference.Value;
                    if (game.User.SubLevel == Constants.NumberSubLevels)
                    {
                        if (game.User.Level.LevelNumber == Constants.MaxLevels)
                        {
                            // the user win, and the game is finish
                            this.RegisterLog("user_win", null, "ok", game.User.UserLoginId);
                        }
                        else
                        {
                            // i have to advance level
                            User user = game.User;
                            Level newLevel;
                            IQueryable<Level> inewLevel = this.container.Levels.Where(l => l.LevelNumber == (user.Level.LevelNumber + 1));
                            if (inewLevel != null && inewLevel.Count() != 0)
                            {
                                newLevel = inewLevel.First();
                            }
                            else
                            {
                                this.RegisterLog("Arrest: There is no Level with this number", null, "error", "0");
                                throw new FaultException(new FaultReason("There is no Level with this number"), new FaultCode("ESG10"));
                            }
                            user.SubLevel = 0;
                            user.Level = newLevel;
                            this.RegisterLog("user_advanceLevel", null, "ok", user.UserLoginId);
                        }
                    }
                    else
                    {
                        // advance the subLevel
                        game.User.SubLevel++;
                        this.RegisterLog("user_advanceSubLevel", null, "ok", game.User.UserLoginId);
                    }

                    clue.States = GameState.WIN;
                    clue.GameInfo = GetGameInfo(game, clue.States);
                    DeleteGame(game.User);
                    this.container.SaveChanges();
                    return true;
                }
                else
                {
                    // wrong order of arrest
                    clue.States = GameState.LOSE_EOAW;
                    this.RegisterLog("user_emit_wrong_order_of_arrest", null, "ok", game.User.UserLoginId);
                }
            }
            else
            {
                // no emit order of arrest
                clue.States = GameState.LOSE_NEOA;
                this.RegisterLog("user_no_emit_order_of_arrest", null, "ok", game.User.UserLoginId);
            }

            clue.GameInfo = this.GetGameInfo(game, clue.States);
            DeleteGame(game.User);
            this.container.SaveChanges();
            return false;
        }

        

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="user"> Parameter description for user goes here</param>
        public void DeleteGame(User user)
        {
            DataManager dm = new DataManager();
            Game game = user.Game;
            if (game == null) 
            {
                throw new GameException("No exist game to delete", new Exception());
            }

            ////user.Game = null;
            for (int i = Constants.NumberLastCity - 1; i >= 0; i--)
            {
                for (int j = Constants.FamousCities - 1; j >= 0; j--)
                {
                    game.NodePath.ElementAt(i).Famous.Remove(game.NodePath.ElementAt(i).Famous.ElementAt(j));
                }
                /*for (int j = Constants.FamousCities - 1; j >= 0; j--)
                {
                    Clue clue = game.NodePath.ElementAt(i).Clue.ElementAt(j);
                    game.NodePath.ElementAt(i).Clue.Remove(game.NodePath.ElementAt(i).Clue.ElementAt(j));
                    clue.NodePath = null;
                }*/
                for (int j = Constants.FamousCities - 1; j >= 0; j--)
                {
                    this.container.DeleteObject(game.NodePath.ElementAt(i).Clue.ElementAt(j));
                }
                for (int j = Constants.PosiblesCities - 1; j >= 0; j--)
                {
                    game.NodePath.ElementAt(i).PossibleCities.Remove(game.NodePath.ElementAt(i).PossibleCities.ElementAt(j));
                }
                
                this.container.DeleteObject(game.NodePath.ElementAt(i));
            }

            ////game.NodePath = null;
            ////this.container.DeleteObject(game.NodePath);
            OrderOfArrest order = game.OrderOfArrest;
            game.OrderOfArrest = null;
            if (order != null)
            {
                order.Suspect = null;
                this.container.DeleteObject(order);
            }

            Filter filter = game.FilterSuspect;
            game.FilterSuspect = null;
            if (filter != null)
            {
                this.container.DeleteObject(filter);
            }

            ////container.DeleteObject(game.Suspect);
            container.DeleteObject(game.Suspect);
            game.Suspect = null;
            
            /* foreach (Suspect suspect in game.PossibleSuspect)
             {
                 container.DeleteObject(suspect);
             }*/
            
            for (int j = Constants.MaxSuspects + Constants.AmountHardCodeSuspects - 2; j >= 0; j--)
            {
                ////game.PossibleSuspect.Remove(game.PossibleSuspect.ElementAt(j));
                this.container.DeleteObject(game.PossibleSuspect.ElementAt(j));
            }
            game.User = null;

            ////container.SaveChanges();
            this.container.DeleteObject(game);
            this.container.SaveChanges();
          ////  RegisterLog("deleteGame", null, "ok", game.User.UserLoginId);
        }

        public DataFacebookUser GetFilters(string userIdFacebook)
        {
            
            IDataManager dm = new DataManager();

            string propS;
            string propFilter;
            string newValue;
            PropertyInfo info;

            Game game = dm.GetGameByUser(userIdFacebook, container);

            Filter filter = game.FilterSuspect;
            var properties = typeof(Filter).GetProperties();

            DataFacebookUser fbud = new DataFacebookUser();

            foreach (var property in properties)
            {
                string propType = property.PropertyType.Name;
                propS = property.Name;
                if ("String".Equals(propType))
                {
                    propFilter = propS;
                    info = filter.GetType().GetProperty(propFilter);
                    if (info != null)
                    {
                        newValue = (string)info.GetValue(filter, null);
                        propFilter = propS.Replace("Filter", string.Empty);
                        info = fbud.GetType().GetProperty(propFilter);
                        if (info != null)
                        {
                            info.SetValue(fbud, newValue, null);
                        }
                    }
                }
            }

            return fbud;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="game"> Parameter description for game goes here</param>
        /// <param name="function"> Parameter description for function goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private DateTime RestTime(Game game, string function)
        {
            this.CalculateTime(game, function);
            return game.CurrentTime;
        }

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="game"> Parameter description for game goes here</param>
        /// <param name="function"> Parameter description for function goes here</param>
        /// <returns> </returns>
        private void CalculateTime(Game game, string function)
        {
            double hours = 0;
            DataManager dm = new DataManager();
            Random rnd = new Random();
            City currentcity = this.CurrentCity(game);
            City nextcity = this.NextCity(game);

            if (function.Equals(Constants.FilterSuspect))
            {
                hours = Double.Parse(dm.GetParameter(Parameters.HoursFilterSuspect, this.container));
            }
            else if (function.Equals(Constants.QuestionFamous))
            {
                hours = rnd.Next(Int32.Parse(dm.GetParameter(Parameters.MinHoursQuestionFamous, this.container)), Int32.Parse(dm.GetParameter(Parameters.MaxHoursQuestionFamous, this.container)));   
            }
            else if (function.Equals(Constants.TravelGood))
            {
                hours = nextcity == null ? 0 : this.TimeToTravel(currentcity, nextcity);
            }
            else if (function.Equals(Constants.TravelWrong))
            {
                hours = nextcity == null ? 0 : this.TimeToTravel(currentcity, nextcity);
                hours += rnd.Next(Int32.Parse(dm.GetParameter(Parameters.MinHoursQuestionFamous, this.container)), Int32.Parse(dm.GetParameter(Parameters.MaxHoursQuestionFamous, this.container)));
                hours += rnd.Next(Int32.Parse(dm.GetParameter(Parameters.MinHoursQuestionFamous, this.container)), Int32.Parse(dm.GetParameter(Parameters.MaxHoursQuestionFamous, this.container)));
                hours += nextcity == null ? 0 : this.TimeToTravel(currentcity, nextcity);                
            }

            game.CurrentTime = game.CurrentTime.AddHours(hours);
            if (this.CanSleep(game))
            {
                int dif = Constants.HourWakeUp - game.CurrentTime.Hour;
                game.CurrentTime = game.CurrentTime.AddHours(dif);
            }

            this.container.SaveChanges();
        }

        /// <summary>
        /// Function Can Sleep
        /// </summary>
        /// <param name="game">game of user</param>
        /// <returns>i sleep?.....</returns>
        private bool CanSleep(Game game)
        {
            return game.CurrentTime.Hour >= 0 && game.CurrentTime.Hour <= 8;
        }

        /// <summary>
        /// Current City function
        /// </summary>
        /// <param name="game">game of user</param>
        /// <returns>Current city of the game</returns>
        private City CurrentCity(Game game)
        {
            foreach (NodePath node in game.NodePath)
            {
                if (node.NodePathCurrent)
                {
                    return node.City;
                }
            }

            return null;
        }

        /// <summary>
        /// Next City function
        /// </summary>
        /// <param name="game">game of user</param>
        /// <returns>Next city of the game</returns>
        private City NextCity(Game game)
        {
            bool next = false;
            foreach (NodePath node in game.NodePath)
            {
                if (next)
                {
                    return node.City;   
                }

                if (node.NodePathCurrent)
                {
                    next = true;
                }
            }

            return null;
        }

        /// <summary>
        /// Time between two cities
        /// </summary>
        /// <param name="currentcity">Parameter description for function currentcity goes here</param>
        /// <param name="nextcity">Parameter description for function nextcity goes here</param>
        /// <returns>return hours to travel</returns>
        private int TimeToTravel(City currentcity, City nextcity)
        {
            DataManager dm = new DataManager();
            int long1 = currentcity.Longitud;
            int long2 = nextcity.Longitud;
            int lat1 = currentcity.Latitud;
            int lat2 = nextcity.Latitud;
            double distancia = Math.Sqrt(((long1 - long2) * (long1 - long2)) + ((lat1 - lat2) * (lat1 - lat2)));
            //// watch this

            int maxtotravel = Int32.Parse(dm.GetParameter(Parameters.MaxHoursTravel, this.container));
            int timetotravel = (int)(Math.Truncate(distancia) % maxtotravel);
            int mintotravel = Int32.Parse(dm.GetParameter(Parameters.MinHoursTravel, this.container));
            return timetotravel < mintotravel ? mintotravel : timetotravel;
         }

        /// <summary>
        /// register the Log
        /// </summary>
        /// <param name="operation"> Parameter description for operation currentcity goes here</param>
        /// <param name="e"> Parameter description for function e goes here</param>
        /// <param name="type"> Parameter description for type currentcity goes here</param>
        private void RegisterLog(string operation, Exception e, string type, string userLogin)
        {
            InterpoolContainer container = new InterpoolContainer();
            Log log = new Log();
            log.LogName = operation;
            log.LogStackTrace = e == null ? null : e.StackTrace;
            log.LogType = type;
            log.UserLogin = userLogin;
            log.Time = DateTime.Now;
            container.AddToLogs(log);
            container.SaveChanges();
            container.Dispose();
        }
        
        private void GetSuspectsFromDatabase(Game game)
        {
            IFacebookController facebookController = new FacebookController();
            List<String> friendIds = facebookController.GetFriendsId(game.User.UserIdFacebook);
            
            //// Gets the users who are not the user himself, and are in a higher level
            this.container.DetectChanges();
            List<User> users = this.container.Users.Where(u => u.UserId != game.User.UserId).ToList();
            users = Functions.ShuffleList(users);
            int i = 0;

            Suspect gameSuspect = null;
            foreach (User user in users)
            {
                if (i >= (Constants.MaxSuspects - 1) && gameSuspect != null)
                {
                    break;
                }
                Suspect suspect = this.NewSuspectFromUser(user);
                if (this.HaveEnoughFields(suspect, Constants.DataRequired))
                {
                    //// If the suspect hasn't be chosen and is not a friend of the current user
                    if (gameSuspect == null && !friendIds.Contains(suspect.SuspectFacebookId))
                    {
                        gameSuspect = suspect;
                    }
                    else if (i < (Constants.MaxSuspects - 1))
                    {
                        game.PossibleSuspect.Add(suspect);
                        i++;
                    }
                }
            }

            if (gameSuspect != null)
            {
                game.Suspect = gameSuspect;
            }
        }


        /// <summary>
        /// Calculate Daed Line
        /// </summary>
        /// <param name="newGame"> Parameter description for newGame goes here</param>
        private void CalculateDeadLine(Game newGame)
        {
            IDataManager dm = new DataManager();
            Level level = newGame.User.Level;

            //// In the best game the user interogate 1 famous * amount travels
            double time = Double.Parse(dm.GetParameter(Parameters.MaxHoursQuestionFamous, this.container)) * (newGame.NodePath.Count - 1);

            // add hours for level user
            time += (double)newGame.User.Level.TimeToAdd;

            //// In the best game the user dont travel wron
            City citySource = newGame.NodePath.ElementAt(0).City;
            for (int i = 1; i < newGame.NodePath.Count; i++)
            {
                time += this.TimeToTravel(citySource, newGame.NodePath.ElementAt(i).City);
                citySource = newGame.NodePath.ElementAt(i).City;
            }

            //// In te best game the user, filter one time
            time += Double.Parse(dm.GetParameter(Parameters.HoursFilterSuspect, this.container));

            double days = time / (24 - Constants.HoursToSleep);

            time += days * Constants.HoursToSleep;

            newGame.DeadLine = newGame.CurrentTime;
            newGame.DeadLine = newGame.DeadLine.AddHours(Math.Round(time));
        }

		private Suspect NewSuspectFromUser(User user)
        {
            Suspect suspect = new Suspect();
            
            suspect.SuspectBirthday = user.UserBirthday;
            suspect.SuspectCinema = user.UserCinema;
            suspect.SuspectFacebookId = user.UserIdFacebook;
            suspect.SuspectFirstName = user.UserFirstName;
            suspect.SuspectGender = user.UserGender;
            suspect.SuspectHometown = user.UserHometown;
            suspect.SuspectLastName = user.UserLastName;
            suspect.SuspectMusic = user.UserMusic;
            suspect.SuspectPicLInk = user.UserPictureLink;
            suspect.SuspectTelevision = user.UserTelevision;

            return suspect;
        }

        private DataGameInfo GetGameInfo(Game game, GameState state)
        {
            DataGameInfo info = new DataGameInfo();
            info.SuspectName = game.Suspect.SuspectFirstName + " " + game.Suspect.SuspectLastName;

            if (state == GameState.WIN)
            {
                ////info.ScoreWin = 
                TimeSpan timeSpan = game.DeadLine.Subtract(game.CurrentTime);
                info.DiffInDays = timeSpan.Days;
                info.DiffInMinutes = timeSpan.Minutes;
                info.DiffInseconds = timeSpan.Seconds;
                int leftTime = (int)(timeSpan.Ticks / TimeSpan.TicksPerMinute);
                info.ScoreWin =  leftTime;
                info.LinkBigSuspect = "www.facebook.com/#!/profile.php?id=" + game.Suspect.SuspectFacebookId;
                game.User.UserScore += (Int32)info.ScoreWin;
            }
            else 
            {
                info.DiffInDays = 0;
                info.DiffInMinutes = 0;
                info.DiffInseconds = 0;
                info.ScoreWin = 0;
                if (state == GameState.LOSE_TO)
                {
                    User user = game.User;
                    this.DeleteGame(user);
                }
            }
            
            info.newLevel = game.User.Level.LevelName;
            info.state = state;
            return info;
        }
        /// <summary>
        /// Description for Method.</summary>
        /// <param name="fbudOfSuspect"> Parameter description for fbudOfSuspect goes here</param>
        /// <param name="cantDataRequired"> Parameter description for cantDataRequired goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        private bool HaveEnoughFields(Suspect fbudOfSuspect, int cantDataRequired)
        {
            int cant = 0;
            if (fbudOfSuspect.SuspectBirthday != string.Empty)
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectCinema != string.Empty)
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectGender != string.Empty)
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectHometown != string.Empty)
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectMusic != string.Empty)
            {
                cant++;
            }

            if (fbudOfSuspect.SuspectTelevision != string.Empty)
            {
                cant++;
            }

            if (cant < cantDataRequired)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void SaveFilters(Game game, DataFacebookUser fbud)
        {
            var properties = typeof(DataFacebookUser).GetProperties();

            string propS;
            string propFilter;
            string newValue;
            PropertyInfo info;

            Filter filter = game.FilterSuspect;

            foreach (var property in properties)
            {
                string propType = property.PropertyType.Name;
                propS = property.Name;
                if ("String".Equals(propType))
                {
                    propFilter = propS;
                    info = fbud.GetType().GetProperty(propFilter);
                    if (info != null)
                    {
                        newValue = (string)info.GetValue(fbud, null);
                        propFilter = "Filter" + propS;
                        info = filter.GetType().GetProperty(propFilter);
                        if (info != null)
                        {
                            info.SetValue(filter, newValue, null);
                        }
                    }
                }
            }

            this.container.SaveChanges();

        }
    }
}