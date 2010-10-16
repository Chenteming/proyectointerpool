
namespace InterpoolCloudWebRole.Controller
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using System.Data.Objects.DataClasses;
    using InterpoolCloudWebRole.Data;
    using InterpoolCloudWebRole.Utilities;
    using InterpoolCloudWebRole.FacebookCommunication;
    using InterpoolCloudWebRole.Datatypes;

    public class ProcessController : IProcessController
    {
        private String output = "Inicio";
        private InterpoolContainer container;
        public ProcessController  (InterpoolContainer container){
            this.container = container;
        }

        public InterpoolContainer GetContainer (){
            return container;
        }
        public DataCity GetCurrentCity(string userIdFacebook)
        {
            
            NodePath node = GetCurrentNode(userIdFacebook);
            if (node != null)
            {
                DataCity dataCity = new DataCity();
                dataCity.name_city = node.City.CityName;
                dataCity.name_file_city = node.City.NameFile;
                return dataCity;
            }
            return null;
        }

        public List<DataCity> GetPossibleCities(string userIdFacebook)
        {
            NodePath node = GetCurrentNode(userIdFacebook);
            List<DataCity> result = new List<DataCity>();
            foreach ( City c in node.PossibleCities)
            {
                DataCity dataCity = new DataCity();
                dataCity.name_city = node.City.CityName;
                dataCity.name_file_city = node.City.NameFile;
                result.Add(dataCity);
            }
            return result;
        }

        public DataFamous GetCurrentFamous(string userIdFacebook, int numClue)
        {
            NodePath node = GetCurrentNode(userIdFacebook);
            if (node != null)
            {
                DataFamous dataFamous = new DataFamous();
                dataFamous.nameFamous = node.Clue.ElementAt(numClue).Famous.FamousName;
                dataFamous.fileFamous = node.Clue.ElementAt(numClue).Famous.NameFileFamous;
                return dataFamous;
            }
            return null;
        }

        public NodePath GetCurrentNode(string userIdFacebook)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook,container);
            foreach (NodePath node in game.NodePath)
            {
                if (node.NodePathCurrent)
                {
                    return node;
                }
            }
            return null;
        }

        public NodePath GetNextNode(string userIdFacebook)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, container);
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

        public void StartGame(string userIdFacebook)
        {
            // this is only the structs that we should follow
            try
            {
                bool existGame = container.Games.Where(game => game.User.UserIdFacebook == userIdFacebook).Count() != 0;

                
                if (existGame)
                    return;

                TimeSpan current = DateTime.Now.TimeOfDay;
            
                User user = container.Users.Where(u => u.UserIdFacebook == userIdFacebook).First();
                // 1 the trip is built to be followed by user
                Game newGame = BuiltTravel(user);

                // 2 Get suspects
                GetSuspects(newGame);

                // 3 Create clues
                CreateClue(newGame);

                container.AddToGames(newGame);
                output = "add to games";
                container.SaveChanges();
                output = "savechanges";
            }
            catch (Exception e)
            {
                Log log = new Log();
                log.LogName = output;
                log.LogStackTrace = e.StackTrace;
                
                
                //conteiner.AddToLogs(log);
                
                throw e;
            }
        }

        public void GetSuspects(Game newGame)
        {
            // In this operation we should go to find the possibles suspects, and asign the suspect

            IFacebookController facebookController = new FacebookController();
            IDataManager dm = new DataManager();
            oAuthFacebook oAuth = dm.GetLastUserToken(dm.GetContainer());
            facebookController.DownloadFacebookUserData(oAuth, newGame, container);
        }

        public Game BuiltTravel(User user)
        {

            Game newGame = new Game();
            user.Game = newGame;
            
            IDataManager dm = new DataManager();

            List<Int32> selectedCities = new List<Int32>();
            NodePath node;
            City next;
            Random random = new Random();

            int maxNumber = Int32.Parse(dm.GetParameter(Parameters.AMOUNT_CITIES, container));
            int nextCity = 0;
            bool find = false;
            //TODO, maybe the amount of NodePath should be a param in the data base
            for (int i = 0; i < 4; i++)
            {
                node = new NodePath();
                node.Famous = new EntityCollection<Famous>();
                for (int j = 0; j < 3; j++)
                {
                    find = false;
                    do
                    {
                        nextCity = random.Next(maxNumber);
                        next = dm.getCities(container).Where(c => c.CityNumber == nextCity).First();
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
                    } while (!find);
                }
                // the current node in the first time is 0
                node.NodePathCurrent = (i == 0);
                node.NodePathOrder = i;

                newGame.NodePath.Add(node);
                //conteiner.AddToNodePaths(node);
            }
            return newGame;
        }

        /* to consider: 
         * - all clues maybe have characteristic of the suspect 
         * - first clue is final
         * - second clue is dynamic
         * - third clue only have news of the famous*/
        private void CreateClue(Game g)
        {
            DataManager dm = new DataManager();

            /* define the number of characteristics of the suspect by city */
            int[] amountCharacteristicsSuspects = new int[3];
            amountCharacteristicsSuspects[0]=1; 
            amountCharacteristicsSuspects[1]=2; 
            amountCharacteristicsSuspects[2]=2;

            /* built the structure for the characteristics that I will put on each clue */
            bool[] cSuspect = new bool[5];
            cSuspect[0] = true;
            cSuspect[1] = true;
            cSuspect[2] = true;
            cSuspect[3] = true;
            cSuspect[4] = true;
            

            /* iterates over the NodePath of the game */
            int i;
            NodePath cnp;
            IEnumerable<NodePath> currentNodePath;
            int rnd;
            int characteristicsSuspect;
            Random r;
            Famous famous;
            for (i = 0; i < Constants.NUMBERLASTCITY - 1; i++)
            {
                /* get the Current NodePath  */
                currentNodePath = g.NodePath.Where(cp => cp.NodePathOrder == i);
                cnp = currentNodePath.First();
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
                City nextCity = NextCity(g, cnp);
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
                famous = cnp.Famous.First();
                c3.Famous = famous;
                if (characteristicsSuspect != 0)
                {

                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        c3.ClueContent = GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " + famous.New.First().NewContent;
                    }
                    else
                    {
                        c3.ClueContent = GetRandomCharacteristicSuspect(g.Suspect, cSuspect);
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
                string dynProperty = cpd == null ? "" : cpd.CityPropertyContent;
                if (characteristicsSuspect != 0)
                {


                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        c2.ClueContent = dynProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " +famous.New.First().NewContent;
                    }
                    else
                    {
                        c2.ClueContent = dynProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect);
                    }
                    characteristicsSuspect--;
                }
                else
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        c2.ClueContent = dynProperty + " " +famous.New.First().NewContent;
                    }
                    else
                    {
                        c2.ClueContent = dynProperty;
                    }
                }

                /* build the second clue */
                Clue c1 = new Clue();

                /* get the static cityProperty for the nextCity */
                CityProperty cps = nextCity.CityProperty.Where(qcs => qcs.Dyn == false).First();

                /* set de city */
                c1.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                string staticProperty = cps == null ? "" : cps.CityPropertyContent;
                famous = cnp.Famous.ElementAt(2);
                c1.Famous = famous;
                if (characteristicsSuspect != 0)
                {
                    if (famous.New.Count() != 0 && famous.New.First().NewContent != null)
                    {
                        c1.ClueContent = staticProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " +famous.New.First().NewContent;
                    }
                    else
                    {
                        c2.ClueContent = staticProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect);
                    }
                    characteristicsSuspect--;

                }
                else
                {
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        c1.ClueContent = staticProperty + " "+famous.New.First().NewContent;
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
            currentNodePath = g.NodePath.Where(cp => cp.NodePathOrder == Constants.NUMBERLASTCITY-1);
            cnp = currentNodePath.First();
            /* build the clues for the last city*/
            Clue lastClue1 = new Clue();
            /* set de city */
            lastClue1.City = cnp.City;
            famous = cnp.Famous.ElementAt(0);
            lastClue1.Famous = famous;
            /* this is in the class parameters  */
            lastClue1.ClueContent = dm.GetParameter(Parameters.LAST_CLUE1_ESP,container);


            Clue lastClue2 = new Clue();
            /* set de city */
            lastClue2.City = cnp.City;
            famous = cnp.Famous.ElementAt(1);
            lastClue2.Famous = famous;
            /* this is in the class parameters  */
            lastClue2.ClueContent = dm.GetParameter(Parameters.LAST_CLUE2_ESP, container);

            Clue lastClue3 = new Clue();
            /* set de city */
            lastClue3.City = cnp.City;
            famous = cnp.Famous.ElementAt(2);
            lastClue3.Famous = famous;
            /* this is in the class parameters  */
            lastClue3.ClueContent = dm.GetParameter(Parameters.LAST_CLUE3_ESP, container);

            /* add clues to nodepath */
            cnp.Clue.Add(lastClue1);
            cnp.Clue.Add(lastClue2);
            cnp.Clue.Add(lastClue3);
            
        }

        private City NextCity(Game g, NodePath currentNodePath)
        {
            /* get the order for the next NodePath */
            int orderNodePath = -1;
            orderNodePath = currentNodePath.NodePathOrder + 1;

            /* if the last nodePath of the game return null */
            if (Constants.NUMBERLASTCITY < orderNodePath)
            {
                return null;
            }

            /* get the next NodePath */
            IEnumerable<NodePath> nextNodePath = from nodePath in g.NodePath
                                                 where nodePath.NodePathOrder == orderNodePath
                                                 select nodePath;
            return nextNodePath.First().City;
        }

        private string GetRandomCharacteristicSuspect (Suspect s, bool[] csuspect){
            /* get the random index for the characteristic of the suspect */
            Random rnd = new Random();
            int indexRandom = rnd.Next(0,5);
            while (!csuspect[indexRandom]){
                indexRandom = rnd.Next(0,5);
            }
            /* set flase in the structure of characteristics of the suspects */
            csuspect[indexRandom] = false;

            /* according to the index, the characteristic choose */
            switch (indexRandom)
            {
                /* faltan definir las características 2, 3 y 4*/
                case 0:
                    return s.SuspectCinema == "" ? "Al sospechoso le gusta ...mmmm, no me acuerdo." : "Al sospechoso le gusta "+s.SuspectCinema + ".";
                    
                case 1:
                    return s.SuspectMusic == "" ? "Al ladrón le gusta escuchar ...mmmm, no me acuerdo en este momento." : "Al ladrón le gusta escuchar "+s.SuspectMusic + ".";

                case 2:
                    return s.SuspectBirthday == "" ? "Su cumpleaños es el ...mmmm, en alguna fecha, que me supongo sabrá su mamá." : "Su cumpleaños es el "+s.SuspectBirthday.ToString() + ".";

                case 3:
                    return s.SuspectHometown == "" ? "El ladrón nació en ...mmmm, una ciudad cuyo nombre no recuerdo." : "El ladrón nació en "+ s.SuspectHometown + ".";

                case 4:
                    return s.SuspectTelevision == "" ? "Al sospechoso le gusta mirar ...mmmm, no recuerdo que programa mira en este momento." : "Al sospechoso le gusta mirar "+s.SuspectTelevision + ".";
                default:
                    return "";
            }
                        
        }

        public List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            IDataManager dm = new DataManager();
            //InterpoolContainer container = dm.GetContainer();
            return dm.FilterSuspects(userIdFacebook, fbud, container);
        }

        public DataCity Travel(string userIdFacebook, string nameNextCity)
        {
            DataCity datacity = new DataCity();
            NodePath node = GetCurrentNode(userIdFacebook);
            NodePath nextNode = GetNextNode(userIdFacebook);
            if (!nextNode.City.CityName.Equals(nameNextCity))
            {
                //TODO: the user lose time
                datacity.name_city = node.City.CityName;
                datacity.name_file_city = node.City.NameFile;
                return datacity;
            }
            node.NodePathCurrent = false;
            nextNode.NodePathCurrent = true;
            container.SaveChanges();
            datacity.name_city = nextNode.City.CityName;
            datacity.name_file_city = nextNode.City.NameFile;
            return datacity;
        }

        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, container);
            if (game.OrderOfArrest != null)
            {
                throw new GameException("error_existOneOrderOfArrest");
            }
            Suspect suspect = null;

            if (game.Suspect.SuspectFacebookId == userIdFacebookSuspect)
            {
                suspect = game.Suspect;
            }
            else
            {
                //TODO, check if exist the suspect with that idFacebook
                suspect = game.PossibleSuspect.Where(s => s.SuspectFacebookId == userIdFacebookSuspect).First();
            }

            OrderOfArrest order = new OrderOfArrest();
            order.Suspect = suspect;
            container.AddToOrdersOfArrest(order);
            game.OrderOfArrest = order;

            container.SaveChanges();
            
        }

        /**
         * summary This function is invoque by the controller when the user reaches the last city
         * 
         * */
        private bool Arrest(Game game, DataClue clue)
        // TODO, change to private
        {
            
            // the user make the order of arrest
            if (game.OrderOfArrest != null)
            {
                if (game.Suspect.SuspectFacebookId == game.OrderOfArrest.Suspect.SuspectFacebookId)
                {
                    // the order is for the guillty, the user win
                    // TODO level and score
                    //User user = game.User;
                    //Level level = user.LevelReference.Value;
                    if (game.User.SubLevel == Constants.NUMBER_SUB_LEVELS)
                    {
                        if (game.User.Level.LevelNumber == Constants.MAX_LEVELS)
                        {
                            // the user win, and the game is finish

                        }
                        else
                        {
                            // i have to advance level
                            // TODO check if exists the next level
                            /*Level newLevel = container.Levels.Where(l => l.LevelNumber == (level.LevelNumber + 1)).First();
                            user.SubLevel = 0;
                            user.Level = newLevel; */
                        }
                    }
                    else
                    {
                        // advance the subLevel
                        game.User.SubLevel++;

                    }
                    // TODO delete
                    // deleteGame(user, container);
                    clue.state = DataClue.State.WIN;
                    container.SaveChanges();
                    return true;
                }
                else
                {
                    // wrong order of arrest
                    clue.state = DataClue.State.LOSE_EOAW;
                }
            }
            else
            {
                // no emit order of arrest
                clue.state = DataClue.State.LOSE_NEOA;
            }

            // user lose
            // TODO level and score
            return false;
        }

        //TODO private
        private void deleteGame(User user)
        {
            Game game = user.Game;

            //user.Game = null;
            IEnumerator<NodePath> itNodes = game.NodePath.GetEnumerator();
            while (itNodes.MoveNext())
            {
                NodePath node = itNodes.Current;
                node.Game = null;
               // container.DeleteObject(node.Clue);
               // node.Clue = null;
              /*  container.DeleteObject(node);
                node.City = null;
                node.PossibleCities = null;
                node.Famous = null;*/
            }
           
            //game.NodePath = null;
            container.DeleteObject(game.NodePath);
            OrderOfArrest order = game.OrderOfArrest;
            game.OrderOfArrest = null;
            if (order != null)
            {
                container.DeleteObject(order.Suspect);
                container.DeleteObject(order);
            }
            //container.DeleteObject(game.Suspect);
            game.Suspect = null;
           /* foreach (Suspect suspect in game.PossibleSuspect)
            {
                container.DeleteObject(suspect);
            }*/
            game.PossibleSuspect = null;
            game.User = null;
            game.NodePath = null;
            //container.SaveChanges();
            container.DeleteObject(game);
            container.SaveChanges();
        }

        public List<DataCity> GetCities(string userId)
        {
            //TODO: order random
            IDataManager dm = new DataManager();
            NodePath node = GetNextNode(userId);
            List<DataCity> cities = new List<DataCity>();
            DataCity datacity;
            foreach (City c in node.PossibleCities)
            {
                datacity = new DataCity();
                datacity.longitud = c.Longitud;
                datacity.latitud = c.Latitud;
                datacity.name_city = c.CityName;
                datacity.name_file_city = c.NameFile;
                cities.Add(datacity);
            }
            datacity = new DataCity();
            datacity.longitud = node.City.Longitud;
            datacity.latitud = node.City.Latitud;
            datacity.name_city = node.City.CityName;
            datacity.name_file_city = node.City.NameFile;
            cities.Add(datacity);
            return cities;

        }

        public DataClue GetClueByFamous(string userIdFacebook, int numFamous)
        {
            IDataManager dm = new DataManager();
            NodePath node = GetCurrentNode(userIdFacebook);
            DataClue clue;
            if (node != null)
            {
                clue = new DataClue();

                //TODO make a Constant
                clue.clue = node.Clue.ElementAt(2-numFamous).ClueContent;
                if (node.NodePathOrder == (Constants.NUMBERLASTCITY - 1))
                {
                    //last city
                    //TODO make a Constant
                    if (numFamous == 1)
                    {
                        Game game = dm.GetGameByUser(userIdFacebook, container);
                        bool arrest = Arrest(game, clue);
                     }
                }
                else
                {
                    clue.state = DataClue.State.PL;
                }
                
                
                return clue;
            }
            clue = new DataClue();
            clue.state = DataClue.State.PL;
            clue.clue = "";
            return clue;
        }

        public string GetLastUserIdFacebook(string idLogin)
        {
            IDataManager dm = new DataManager();
            // This wont work for multiuser game
            return dm.GetLastUserIdFacebook(dm.GetContainer());
        }

        public string GetUserIdFacebook(string userLoginId)
        {
            IDataManager dm = new DataManager();
            // This wont work for multiuser game
            return dm.GetUserIdFacebookByLoginId(userLoginId, dm.GetContainer());
        }
    }

}