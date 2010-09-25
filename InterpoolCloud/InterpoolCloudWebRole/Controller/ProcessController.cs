using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;

using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.Utilities;
using InterpoolCloudWebRole.FacebookCommunication;

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

        public List<string> GetCurrentFamous(string userIdFacebook)
        {
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
        
        public void StartGame(User user)
        {
            // this is only the structs that we should follow
            InterpoolContainer conteiner = new InterpoolContainer();
            try
            {
                user.Level = conteiner.Levels.Where(l => l.LevelId == 1).First();
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

            IQueryable<Suspect> IQbigSuspect =  from s in container.Suspects
                                                where s.SuspectId == 3
                                                select s;
            newGame.Suspect = IQbigSuspect.First();
            IQueryable<Suspect> IQSuspects = from s in container.Suspects
                                               where s.SuspectId != 3
                                               select s;
            
            foreach (Suspect s in IQSuspects.ToList<Suspect>())
            {
                newGame.PossibleSuspect.Add(s);
            }


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
            for (i = 0; i < Constants.NUMBERLASTCITY-1; i++)
            {
                /* get the Current NodePath  */
                IEnumerable<NodePath> currentNodePath = g.NodePath.Where(cp => cp.NodePathOrder == i);
                NodePath cnp = currentNodePath.First();
                Random r = new Random();

                /* get the amount of caracteristic of the suspect by NodePath  */
                int rnd = r.Next(0, 3);
                while (amountCharacteristicsSuspects[rnd] == -1)
                {
                    rnd = r.Next(0, 3);
                }
                int characteristicsSuspect = amountCharacteristicsSuspects[rnd];
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
                Famous famous = cnp.Famous.First();
                if (characteristicsSuspect != 0)
                {

                    if (famous.New.Count != 0 && famous.New.First()!=null)
                    {
                        c3.ClueContent = GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " +famous.New.First().NewContent;
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
                if (queryCity.Count() != 0 && queryCity.First()!=null)
                {
                    cpd = queryCity.First();
                }
                

                /* set de city */
                c2.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                famous = cnp.Famous.ElementAt(1);
                string dynProperty = cpd == null ? "" : cpd.CityPropertyContent;
                if (characteristicsSuspect != 0)
                {

                    
                    if (famous.New.Count() != 0 && famous.New.First() != null)
                    {
                        c2.ClueContent = dynProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " + famous.New.First().NewContent;
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
                        c2.ClueContent = dynProperty + " " + famous.New.First().NewContent;
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
                if (characteristicsSuspect != 0)
                {
                    if (famous.New.Count() != 0 && famous.New.First().NewContent != null)
                    {
                        c1.ClueContent = staticProperty + " " + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + " " + famous.New.First().NewContent;
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
                        c1.ClueContent = staticProperty + " " + famous.New.First().NewContent;
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

        private String GetRandomCharacteristicSuspect (Suspect s, bool[] csuspect){
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
                    return s.SuspectPreferenceMovies;
                    
                case 1:
                    return s.SuspectPreferenceMusic;

                case 2:
                    return "Fanta definir la característica";

                case 3:
                    return "Fanta definir la característica";

                case 4:
                    return "Fanta definir la característica";
                default:
                    return "";
            }
                        
        }
    }

}