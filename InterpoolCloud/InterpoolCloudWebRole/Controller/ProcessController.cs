using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;

using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.Utilities;
using InterpoolCloudWebRole.FacebookCommunication;
using InterpoolCloudWebRole.Datatypes;

namespace InterpoolCloudWebRole.Controller
{
    public class ProcessController : IProcessController
    {

        public string GetCurrentCity(string userIdFacebook)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            NodePath node = GetCurrentNode(userIdFacebook, conteiner);
            if (node != null)
            {
                return node.City.CityName;
            }
            return null;
        }

        public List<string> GetPossibleCities(string userIdFacebook)
        {
            return null;
        }

        public List<DataFamous> GetCurrentFamous(string userIdFacebook, int numClue)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            NodePath node = GetCurrentNode(userIdFacebook, conteiner);
            if (node != null)
            {
                List<Famous> f = node.Famous.ToList();
            }
            return null;
        }

        private NodePath GetCurrentNode(string userIdFacebook, InterpoolContainer conteiner)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, conteiner);
            foreach (NodePath node in game.NodePath)
            {
                if (node.NodePathCurrent)
                {
                    return node;
                }
            }
            return null;
        }

        private NodePath GetNextNode(string userIdFacebook, InterpoolContainer conteiner)
        {
            IDataManager dm = new DataManager();
            Game game = dm.GetGameByUser(userIdFacebook, conteiner);
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
            InterpoolContainer conteiner = new InterpoolContainer();
            try
            {

                User user = conteiner.Users.Where(u => u.UserIdFacebook == userIdFacebook).First();
                // 1 the trip is built to be followed by user
                Game newGame = BuiltTravel(user, conteiner);

                // 2 Get suspects
                GetSuspects(newGame,conteiner);

                // 3 Create clues
                CreateClue(newGame,conteiner);
                
                conteiner.AddToGames(newGame);
                conteiner.SaveChanges();
            }
            catch (Exception e)
            {
                Log log = new Log();
                log.LogName = "StartGame";
                log.LogStackTrace = e.StackTrace;
                //conteiner.AddToLogs(log);
                throw e;
            }
        }

        public void GetSuspects(Game newGame, InterpoolContainer container)
        {
            // In this operation we should go to find the possibles suspects, and asign the suspect

            IFacebookController facebookController = new FacebookController();
            IDataManager dm = new DataManager();
            oAuthFacebook oAuth = dm.GetLastUserToken(dm.GetContainer());
            facebookController.DownloadFacebookUserData(oAuth, newGame, container);
        }

        private Game BuiltTravel(User user, InterpoolContainer conteiner)
        {

            Game newGame = new Game();
            user.Game = newGame;
            
            IDataManager dm = new DataManager();

            List<City> selectedCities = new List<City>();
            NodePath node;
            City next;
            Random random = new Random();

            int maxNumber = Int32.Parse(dm.GetParameter(Parameters.AMOUNT_CITIES, conteiner));
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
                        next = dm.getCities(conteiner).Where(c => c.CityNumber == nextCity).First();
                        if (!selectedCities.Contains(next))
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
        private void CreateClue(Game g, InterpoolContainer conteiner)
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
                        c3.ClueContent = GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " ";// +famous.New.First().NewContent;
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
                        c2.ClueContent = dynProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " ";// +famous.New.First().NewContent;
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
                        c2.ClueContent = dynProperty + " ";// +famous.New.First().NewContent;
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
                        c1.ClueContent = staticProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " ";// +famous.New.First().NewContent;
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
                        c1.ClueContent = staticProperty + " ";// +famous.New.First().NewContent;
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
            lastClue1.ClueContent = dm.GetParameter(Parameters.LAST_CLUE1_ESP,conteiner);


            Clue lastClue2 = new Clue();
            /* set de city */
            lastClue2.City = cnp.City;
            famous = cnp.Famous.ElementAt(1);
            lastClue2.Famous = famous;
            /* this is in the class parameters  */
            lastClue2.ClueContent = dm.GetParameter(Parameters.LAST_CLUE2_ESP, conteiner);

            Clue lastClue3 = new Clue();
            /* set de city */
            lastClue3.City = cnp.City;
            famous = cnp.Famous.ElementAt(2);
            lastClue3.Famous = famous;
            /* this is in the class parameters  */
            lastClue3.ClueContent = dm.GetParameter(Parameters.LAST_CLUE3_ESP, conteiner);

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
                    return s.SuspectCinema == null ? "" : "Al sospechoso le gusta "+ s.SuspectCinema + ".";
                    
                case 1:
                    return s.SuspectMusic == null ? "" : "Al ladrón le gusta escuchar " + s.SuspectMusic + ".";

                case 2:
                    return s.SuspectBirthday == null ? "" : "Su cumpleaños es el " + s.SuspectBirthday.ToString() + ".";

                case 3:
                    return s.SuspectHometown == null ? "" : "El ladrón nació en " + s.SuspectHometown + ".";

                case 4:
                    return s.SuspectTelevision == null ? "" : "Al sospechoso le gusta mirar " + s.SuspectTelevision + ".";
                default:
                    return "";
            }
                        
        }

        public List<DataFacebookUser> FilterSuspects(string userIdFacebook, DataFacebookUser fbud)
        {
            IDataManager dm = new DataManager();
            InterpoolContainer container = dm.GetContainer();
            return dm.FilterSuspects(userIdFacebook, fbud, container);
        }

        public String Travel(string userIdFacebook)
        {
            InterpoolContainer container = new InterpoolContainer();
            NodePath node = GetCurrentNode(userIdFacebook, container);
            NodePath nextNode = GetNextNode(userIdFacebook, container);
            node.NodePathCurrent = false;
            nextNode.NodePathCurrent = true;
            container.SaveChanges();
            return nextNode.City.CityName;
        }

        public void EmitOrderOfArrest(string userIdFacebook, string userIdFacebookSuspect)
        {
            IDataManager dm = new DataManager();
            InterpoolContainer container = new InterpoolContainer();
            Game game = dm.GetGameByUser(userIdFacebook, container);
            if (game.OrderOfArrest != null)
            {
                throw new GameException("error_existOneOrderOfArrest");
            }

            //TODO, check if exist the suspect with that idFacebook
            Suspect suspect = game.PossibleSuspect.Where(s => s.SuspectFacebookId == userIdFacebookSuspect).First();

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
        private bool Arrest(Game game, InterpoolContainer container)
        {
            // the user make the order of arrest
            if (game.OrderOfArrest != null)
            {
                if (game.Suspect.SuspectFacebookId == game.OrderOfArrest.Suspect.SuspectFacebookId)
                {
                    // the order is for the guillty, the user win
                    // TODO level and score
                    User user = game.User;
                    Level level = user.Level;
                    if (user.SubLevel == Constants.NUMBER_SUB_LEVELS)
                    {
                        if (level.LevelNumber == Constants.MAX_LEVELS)
                        {
                            // the user win, and the game is finish

                        }
                        else
                        {
                            // i have to advance level
                            // TODO check if exists the next level
                            Level newLevel = container.Levels.Where(l => l.LevelNumber == (level.LevelNumber + 1)).First();
                            user.SubLevel = 0;
                            user.Level = newLevel;
                        }    
                    }
                    else
                    {
                        // advance the subLevel
                        user.SubLevel++;

                    }
                    deleteGame(user, container);
                    return true;
                }
            }

            // user lose
            // TODO level and score
            return false;
        }

        //TODO private
        public void deleteGame(User user, InterpoolContainer container)
        {
            Game game = user.Game;
            user.Game = null;
            foreach(NodePath node in game.NodePath)
            {
                node.City = null;
                node.PossibleCities = null;
                node.Famous = null;
                foreach(Clue clue in node.Clue)
                {
                    clue.City = null;
                    clue.Famous = null;
                    container.DeleteObject(clue);
                }
                node.Clue = null;
                container.DeleteObject(node);
            }
            game.NodePath = null;
            OrderOfArrest order = game.OrderOfArrest;
            game.OrderOfArrest = null;
            container.DeleteObject(order.Suspect);
            container.DeleteObject(order);
            container.DeleteObject(game.Suspect);
            game.Suspect = null;
            foreach (Suspect suspect in game.PossibleSuspect)
            {
                container.DeleteObject(suspect);
            }
            game.PossibleSuspect = null;
            container.DeleteObject(game);
            container.SaveChanges();
        }

        public List<DataCity> GetCities(string userId)
        {
            //TODO: order random
            IDataManager dm = new DataManager();
            InterpoolContainer container = new InterpoolContainer();
            NodePath node = GetCurrentNode(userId,container);
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

		public string GetClueByFamous(string userIdFacebook, int numFamous)
        {
            InterpoolContainer conteiner = new InterpoolContainer();
            NodePath node = GetCurrentNode(userIdFacebook, conteiner);
            if (node != null)
            {
                return node.Clue.ElementAt(numFamous - 1).ClueContent;
            }
            return "";
        }

        public string GetLastUserIdFacebook(string idLogin)
        {
            IDataManager dm = new DataManager();
            // This wont work for multiuser game
            return dm.GetLastUserIdFacebook(dm.GetContainer());
        }
    }

}