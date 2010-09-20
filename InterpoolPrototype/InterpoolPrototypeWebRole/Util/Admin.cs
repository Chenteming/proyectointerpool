using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterpoolPrototypeWebRole.BingSearchService;
using InterpoolPrototypeWebRole.Data;

namespace InterpoolPrototypeWebRole.Util
{
    public static class Admin
    {
        public static void loadFemousData()
        {
            string news = "";

            BingSearchService.LiveSearchPortTypeClient client = new InterpoolPrototypeWebRole.BingSearchService.LiveSearchPortTypeClient();

            SearchRequest request = new SearchRequest()

            {

                AppId = "A00C4105122186E4F9F0DFD82CDF594DD866BC1F",

                Sources = new SourceType[] { SourceType.Web, SourceType.News },

                Adult = AdultOption.Moderate,

                AdultSpecified = true,

                Market = "es-MX",
            };

            InterpoolContainer conteiner = new InterpoolContainer();
            New newsF;

            foreach (Famous f in conteiner.Famous)
            {
                request.Query = f.FamousName + " "+f.City.CityCountry;
                SearchResponse response = client.Search(request);
                if (response != null && response.Errors == null && response.News != null && response.News.Results != null)
                {

                    news = response.News.Results.FirstOrDefault().Snippet;
                    newsF = new New();
                    newsF.NewContent = news;
                    newsF.Famous = f;

                    conteiner.AddToNews(newsF);
                }   
            }
            conteiner.SaveChanges();
            
        }
    }
}