using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;

using InterpoolCloudWebRole.Data;
using InterpoolCloudWebRole.Utilities;

namespace InterpoolCloudWebRole.Controller
{
    public class ProcessController : IProcessController
    {
        public void StartGame(User user)
        {
            // this is only the structs that we should follow
            InterpoolContainer conteiner = new InterpoolContainer();
            try
            {
                // 1 the trip is built to be followed by user
                Game newGame = BuiltTravel(user, conteiner);

                // 2 Get suspects
                GetSuspects(newGame);

                // 3 Create clues
                CreateClue(newGame);
                
                conteiner.AddToGames(newGame);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void GetSuspects(Game newGame)
        {
            // in this operation we should to find the possibles suspects , and asign the suspect
            // newGame.Suspect =
            // newGame.PossibleSuspect
            throw new NotImplementedException();
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
                find = false;
                do
                {
                    nextCity = random.Next(maxNumber);
                    next = dm.getCities(conteiner).Where(c => c.CityNumber == nextCity).First();
                    if (!selectedCities.Contains(next)) 
                    {
                        find = true;
                        node.City = next;
                        foreach (Famous f in dm.GetFamousByCity(next, conteiner))
                        {
                            node.Famous.Add(f);
                        } 
                    }
                } while (!find);
                newGame.NodePath.Add(node);
                conteiner.AddToNodePaths(node);
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
            List<Int32> amountCharacteristicsSuspects = new List<int>(3);
            amountCharacteristicsSuspects[0]=1; amountCharacteristicsSuspects[1]=2; amountCharacteristicsSuspects[2]=3;

            /* built the structure for the characteristics that I will put on each clue */
            List<Boolean> cSuspect = new List<Boolean>(5);
            cSuspect[0]=false;cSuspect[1]=false;cSuspect[2]=false;cSuspect[3]=false;cSuspect[4]=false;
            
            /* get the context for query of DataManager*/
            InterpoolContainer ic = new InterpoolContainer();

            /* iterates over the NodePath of the game */
            int i;
            for (i = 0; i < Constants.NUMBERLASTCITY-1; i++)
            {
                /* get the Current NodePath  */
                IEnumerable<NodePath> currentNodePath = g.NodePath.Where(cp => cp.NodePathOrder == i);
                NodePath cnp = currentNodePath.First();
                Random r = new Random();

                /* get the amount of caracteristic of the suspect by NodePath  */
                int rnd = r.Next(0, 2);
                while (amountCharacteristicsSuspects[rnd] != 0)
                {
                    rnd = r.Next(0, 2);
                }
                int characteristicsSuspect = amountCharacteristicsSuspects[rnd];
                amountCharacteristicsSuspects[rnd] = 0;
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
                if (characteristicsSuspect != 0)
                {
                    c3.ClueContent = GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + cnp.Famous.First().New;
                    characteristicsSuspect--;
                }
                
                /* build the second clue */
                Clue c2 = new Clue();

                /* get the dynamic cityProperty for the nextCity */
                CityProperty cpd = nextCity.CityProperty.Where(qcp => qcp.Dyn == true).First();

                /* set de city */
                c2.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                if (characteristicsSuspect != 0)
                {
                    c2.ClueContent = cpd.CityPropertyContent + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + cnp.Famous.ElementAt(1).New;
                    characteristicsSuspect--;
                }
                else
                {
                    c2.ClueContent = cpd.CityPropertyContent;
                }
                
                /* build the second clue */
                Clue c1 = new Clue();

                /* get the static cityProperty for the nextCity */
                CityProperty cps = nextCity.CityProperty.Where(qcs => qcs.Dyn == false).First();

                /* set de city */
                c1.City = cnp.City;
                /* if i have to put characteristics on the clue of the suspect */
                if (characteristicsSuspect != 0)
                {
                    c1.ClueContent = cps.CityPropertyContent + GetRandomCharacteristicSuspect(g.Suspect, cSuspect) + cnp.Famous.ElementAt(2).New;
                    characteristicsSuspect--;
                }
                else
                {
                    c1.ClueContent = cps.CityPropertyContent;
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

        private String GetRandomCharacteristicSuspect (Suspect s, List<Boolean> csuspect){
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
                    return s.SuspectPreferenceMovies;
                default:
                    return "";
            }
                        
        }
    }

}