
namespace InterpoolCloudWebRole.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    using InterpoolCloudWebRole.BingSearchService;
    using InterpoolCloudWebRole.Data;
    using System.Text.RegularExpressions;
    using System.IO;
    using System.Net;
    using System.Diagnostics;

    public static class Admin
    {
        //FindCity()  "Este País" -- "Esta Ciudad"
        #region FindCity (string ciudad, string country)
        public static string FindCity(string ciudad, string country)
        {
            //agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string QueryOut = EscapearQuery(ciudad);

            //realiza la busqueda en BING
            SearchResponse response = BingRequest(QueryOut);

            string resultado = null;

            if (response.Errors == null)
            {
                int indice = 0;
                int maxNews = 0;
                if (response.News != null && response.News.Results != null)
                    maxNews = response.News.Results.Length;

                while (resultado == null && indice < maxNews)
                {
                    NewsResult result = response.News.Results[indice];
                    resultado = ParsearNoticia(result.Snippet, ciudad);
                    indice++;
                }
            }

            if (country != null)
            {
                resultado = ReemplazarTexto(resultado, "Este País", country);
                return ReemplazarTexto(resultado, "Esta Ciudad", ciudad);
            }
            return resultado;
        }
        #endregion FindCity

        //"Yo"
        #region  FindFamous(string famoso)
        public static string FindFamous(string famoso)
        {
            //agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string QueryOut = EscapearQuery(famoso);

            //realiza la busqueda en BING
            SearchResponse response = BingRequest(QueryOut);

            string resultado = null;

            if (response.Errors == null)
            {
                int indice = 0;
                int maxNews = 0;
                if (response.News != null && response.News.Results != null)
                    maxNews = response.News.Results.Length;

                while (resultado == null && indice < maxNews)
                {
                    NewsResult result = response.News.Results[indice];
                    resultado = ParsearNoticia(result.Snippet, famoso);
                    indice++;
                }
            }


            return ReemplazarTexto(resultado, "Yo", famoso);

        }
        #endregion  FindFamous

        //devuelve el string del query con comillas dobles escapedas
        #region EscapearQuery
        static String EscapearQuery(string QueryIn)
        {
            var cadenaSplit = QueryIn.Split(' ');
            System.Text.StringBuilder result = new System.Text.StringBuilder();

            if (cadenaSplit != null)
            {
                result = result.Append("\"").Append(cadenaSplit[0]);

                for (int index = 1; index < cadenaSplit.Length; index++)
                {
                    result.Append(" ").Append(cadenaSplit[index]);
                }
                result.Append("\"");
            }
            return result.ToString();
        }
        #endregion EscapearQuery

        //se trae la noticia
        #region BingRequest
        static SearchResponse BingRequest(string Query)
        {
            BingSearchService.BingPortTypeClient client = new BingSearchService.BingPortTypeClient();
            SearchRequest request = new SearchRequest()

            {
                AppId = Constants.APPID,
                Sources = new SourceType[] { SourceType.Web, SourceType.News },
                Adult = AdultOption.Moderate,
                AdultSpecified = true,
                Query = Query,
                Market = Constants.MARKET,

            };
            request.Version = Constants.REQUEST_VERSION;

            request.News = new NewsRequest();
            request.News.Offset = Constants.NEWS_OFFSET;
            request.News.OffsetSpecified = true;
            request.News.Count = Constants.NEWS_COUNT;
            request.News.SortBy = NewsSortOption.Relevance;
            request.News.SortBySpecified = true;

            return client.Search(request);
        }
        #endregion BingRequest
        //Devuelve caracteres hasta la primer ocurrencia de un punto (.) despues mas de 95 caracteres
        #region ParsearNoticia
        static String ParsearNoticia(string entrada, string Query)
        {
            String resultado = null;
            Regex expRegNoticia;

            String patron1 = @"(.){95}[^\.]{0,105}";
            expRegNoticia = new Regex(patron1, RegexOptions.Multiline);

            Match matchNoticia = expRegNoticia.Match(entrada);
            if (matchNoticia.Length > 0)
            {
                return matchNoticia.ToString();
            }
            return resultado;
        }
        #endregion ParsearNoticia

        #region ReemplazarTexto
        static String ReemplazarTexto(string noticia, string nuevoTxt, string viejoTxt)
        {
            if (noticia != null)
            {
                Regex expRegQuitarPais = new Regex(viejoTxt, RegexOptions.IgnoreCase);
                string entradaSinPais = expRegQuitarPais.Replace(noticia, nuevoTxt);
                return entradaSinPais;
            }
            return "";
        }
        #endregion ReemplazarTexto

        public static void loadFamousData()
        {
            string news = "";

            InterpoolContainer container = new InterpoolContainer();
            New newsF;

            foreach (Famous f in container.Famous)
            {

                //Se trae la noticia
                news = FindFamous(f.FamousName);

                if (news != null)
                {
                    newsF = new New();
                    newsF.NewContent = news;
                    newsF.Famous = f;
                    container.AddToNews(newsF);
                }
            }
            container.SaveChanges();


            CityProperty newsCity;
            foreach (City c in container.Cities)
            {

                //Se trae la noticia
                news = FindCity(c.CityName, c.CityCountry);

                if (news != null)
                {
                    newsCity = new CityProperty();
                    newsCity.CityPropertyContent = news;
                    newsCity.City = c;
                    newsCity.Dyn = true;
                    container.AddToCityPropertySet(newsCity);
                }
            }
            container.SaveChanges();
        }
    }
}