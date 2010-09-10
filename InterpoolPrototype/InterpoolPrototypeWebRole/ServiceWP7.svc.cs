using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using InterpoolPrototypeWebRole.BingSearchService;
using InterpoolPrototypeWebRole.Data;

namespace InterpoolPrototypeWebRole
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceWP7" in code, svc and config file together.
    public class ServiceWP7 : IServiceWP7
    {
        private static int counterCities;

        private static string currentCity;

        public List<string> GetCities()
        {
            List<string> res = new List<string>();
            InterpoolContainer context = new InterpoolContainer();
            List<City> cities = new List<City>(context.Cities);
            foreach (City c in cities)
            {
                res.Add(String.Concat(c.CityName, " - ", c.CityCountry));
            }
            return res;
        }

        
        public string GetClueByFamous(string cadena)
        {
            string resultado = "";

            BingSearchService.LiveSearchPortTypeClient client = new InterpoolPrototypeWebRole.BingSearchService.LiveSearchPortTypeClient();

            SearchRequest request = new SearchRequest()

            {

                AppId = "A00C4105122186E4F9F0DFD82CDF594DD866BC1F",

                Sources = new SourceType[] { SourceType.Web, SourceType.News },

                Adult = AdultOption.Moderate,

                AdultSpecified = true,

                Query = cadena,      
         
                Market = "es-MX",
            };

            SearchResponse response = client.Search(request);

            if (response.Errors == null)
                resultado = response.News.Results.FirstOrDefault().Snippet;

            return resultado;

        }

        //Start game.
        public void StartGame()
        {
            counterCities = 0;
            currentCity = "Montevideo";
        }

        //Returns the current city
        public string GetCurrentCity()
        {
            return currentCity;
        }

        //Returns possible cities to travel
        public List<string> GetPossibleCities()
        {
            List<string> listCities = new List<string>();
            switch (counterCities)
            {
                case 0:
                    listCities.Add("Rio de Janeiro");
                    listCities.Add("Katmandu");
                    listCities.Add("Katmandu");
                    break;
                case 1:
                    listCities.Add("Roma");
                    listCities.Add("Madrid");
                    listCities.Add("Madrid");
                    break;
                case 2:
                    listCities.Add("Londres");
                    listCities.Add("Moscu");
                    listCities.Add("Moscu");
                    break;
            }
            listCities.Add("NoPuedeSeguirViajando");
            listCities.Add("NoPuedeSeguirViajando");
            listCities.Add("NoPuedeSeguirViajando");
            return listCities;
        }

        //Returns the current city famous
        public List<string> GetCurrentFamous(string city)
        {
            List<string> listFamous = new List<string>();
            switch (city)
            {
                case "Montevideo":
                    listFamous.Add("Sebastian Abreu");
                    listFamous.Add("Natalia Oreiro");
                    listFamous.Add("Paco Casal");
                    break;
                case "Río de Janeiro":
                    listFamous.Add("Roberto Carlos");
                    listFamous.Add("Xuxa");
                    listFamous.Add("Ronaldo");
                    break;
                case "Roma":
                    listFamous.Add("Giorgio Armani");
                    listFamous.Add("Andrea Bocelli");
                    listFamous.Add("Elisabetta Gregoracci");
                    break;
            }
            listFamous.Add("Queen Elizabeth");
            listFamous.Add("Bruce Dickinson");
            listFamous.Add("Steven Gerrard");
            return listFamous;
        }

        //Esta es la funcion de facebook que tiene Fede A y Vicente.
        public List<string> GetProbablySuspects()
        {
            // TODO: Get from the database the lists of names of the friends
            // of the current user
            List<string> friendsNames = new List<string>();
            InterpoolContainer context = new InterpoolContainer();
            List<Suspect> pSuspects = new List<Suspect>(context.Suspects);
            foreach (Suspect ps in pSuspects)
            {
                friendsNames.Add(ps.SuspectName);
            }
            return friendsNames;

        }

       //Travel from one city to another
        public void Travel(string City)
        {
            counterCities++;
            switch (counterCities)
            {
                case 1:
                    currentCity = "Río de Janeiro";
                    break;
                case 2:
                    currentCity = "Roma";
                    break;
                case 3:
                    currentCity = "Londres";
                    break;
            }
        }

    }
}
