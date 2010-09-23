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

                //CreateClue();
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

        private List<Clue> CreateClue(City city, User user, Suspect suspect)
        {
            DataManager dm = new DataManager();
            InterpoolContainer ic = new InterpoolContainer();
            List<Clue> cpRes = new List<Clue>();

            City nextCity = NextCity(user, city);
            if (nextCity == null)
            {
                return null;
            }

            IQueryable<CityProperty> cp = dm.GetCityPropertyByCity(nextCity, ic);

            Level l = user.Level;
            Clue c1 = new Clue();
            Clue c2 = new Clue();
            Clue c3 = new Clue();

            IQueryable<Suspect> BigSuspect = dm.GetSuspectByGame(user.Game, ic);

            /*************** INICIA código totalmente hardcode para probar el funcionamiento de la función para un usario **************/

            if (l.LevelName.Equals(Parameters.LEVEL_ROOKIE))
            {
                c1.City = nextCity;
                c2.City = nextCity;
                c3.City = null;

                IQueryable<CityProperty> cp1 = from ccpp1 in cp
                                               where ccpp1.Dyn == false
                                               select ccpp1;
                c1.ClueContent = cp1.First().CityPropertyContent;

                IQueryable<CityProperty> cp2 = from ccpp2 in cp
                                               where ccpp2.Dyn == true
                                               select ccpp2;
                c2.ClueContent = cp2.First().CityPropertyContent;

                IQueryable<CityProperty> cp3 = from ccpp3 in cp
                                               where ccpp3.City.CityName == Parameters.NONE
                                               select ccpp3;

                //IQueryable<New> newsFamous = from nnff in city.Famous.
                //                             where nnff.New in
                c3.ClueContent = cp3.First().CityPropertyContent; //+ " " + ;

                cpRes.Add(c1);
                cpRes.Add(c2);
                cpRes.Add(c3);
                return cpRes;
            }
            return null;

            /***************************** FIN de código hardcode *********************************/
        }

        public City NextCity(User user, City city)
        {
            IEnumerable<NodePath> currentNodePath = from nodePath in user.Game.NodePath
                                                    where nodePath.City == city
                                                    select nodePath;

            
            int orderNodePath = -1;
            orderNodePath = currentNodePath.ElementAt(0).NodePathOrder + 1;

            if (Parameters.NUMBERLASTCITY < orderNodePath)
            {
                return null;
            }

            IEnumerable<NodePath> nextNodePath = from nodePath in user.Game.NodePath
                                                 where nodePath.NodePathOrder == orderNodePath
                                                 select nodePath;
            return nextNodePath.ElementAt(0).City;
        }
    }

}