using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterpoolPrototypeWebRole.Data;
using InterpoolPrototypeWebRole.Util;

namespace InterpoolPrototypeWebRole.Controller
{
    public class ProcessController:IProcessController
    {
        public void StartGame(User user)
        {
            // this is only the structs that we should follow

            // 1 the trip is built to be followed by user
            BuiltTravel(user);

            // 2 Get suspects
            GetSuspects();

            // 3 Create clues
            
            //CreateClue();
        }

        public void GetSuspects()
        {
            throw new NotImplementedException();
        }

        public void BuiltTravel(User user)
        {
            Game newGame = new Game();
            user.Game = newGame;
            IDataManager dm = new DataManager();

            List<City> selectedCities = new List<City>();
            NodePath node;
            //TODO, maybe the amount of NodePath should be a param in the data base
            for (int i = 0; i < 4; i++)
            {
                node = new NodePath();
                bool find = false;
                do
                {
                    //get one city random
                    //if not city in selectedCities
                    //  find = true
                    //  node.City = city
                    //  node.Famous.Add(new List<Famous> (dm.getFamousByCity(city)))
                } while (!find);
                newGame.NodePath.Add(node);
            }
        }

        public List<Clue> CreateClue(City city, User user, Suspect suspect)
        {
            DataManager dm = new DataManager();
            InterpoolContainer ic = new InterpoolContainer();
            List<Clue> cpRes = new List<Clue>();

            City nextCity = NextCity(user,city);
            if (nextCity == null)
            {
                return null;
            }

            IQueryable<CityProperty> cp = dm.GetCityPropertyByCity(nextCity,ic);
            
            Level l = user.Level;
            Clue c1 = new Clue();
            Clue c2 = new Clue();
            Clue c3 = new Clue();
            
            IQueryable<Suspect> BigSuspect = dm.GetSuspectByGame(user.Game,ic);

            /*************** INICIA código totalmente hardcode para probar el funcionamiento de la función para un usario **************/

            if (l.LevelName.Equals(Parameters.LEVEL_ROOKIE))
            {
               
                
                c1.City = city;
                for (int i = 0; i < cp.ToArray().Length; i++)
                {
                    if (cp.ElementAt(i).Dyn == true)
                    {
                        c1.ClueContent = cp.ElementAt(i).CityPropertyContent;
                    }
                }
                c2.City = city;
                for (int i = 0; i < cp.ToArray().Length; i++)
                {
                    if (cp.ElementAt(i).Dyn == false)
                    {
                        c2.ClueContent = cp.ElementAt(i).CityPropertyContent;
                    }
                }
                c3.City = city;
                for (int i = 0; i < cp.ToArray().Length; i++)
                {
                    if (cp.ElementAt(i).City.CityName == Parameters.NONE)
                    {
                        c3.ClueContent = cp.ElementAt(i).CityPropertyContent + suspect.SuspectPreferenceMovies;
                    }
                }
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