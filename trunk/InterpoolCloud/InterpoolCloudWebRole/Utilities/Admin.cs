//-----------------------------------------------------------------------
// <copyright file="Admin.cs" company="Interpool">
//     Copyright Interpool. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------
namespace InterpoolCloudWebRole.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Text.RegularExpressions;
    using System.Web;
    using InterpoolCloudWebRole.BingSearchService;
    using InterpoolCloudWebRole.Data;

    /// <summary>
    /// Class statement Admin
    /// </summary>
    public static class Admin
    {
        /// <summary>
        /// Description for Method.</summary>
        public static void LoadFamousData()
        {
            string news = string.Empty;

            InterpoolContainer container = new InterpoolContainer();
            New newsF;

            foreach (Famous f in container.Famous)
            {
                ////Se trae la noticia
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
                ////Se trae la noticia
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

        ////FindCity()  "Este País" -- "Esta Ciudad"

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="ciudad"> Parameter description for ciudad goes here</param>
        /// <param name="country"> Parameter description for country goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region FindCity (string ciudad, string country)
        public static string FindCity(string ciudad, string country)
        {
            ////agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string queryOut = EscapearQuery(ciudad);

            ////realiza la busqueda en BING
            SearchResponse response = BingRequest(queryOut);

            string resultado = null;

            if (response.Errors == null)
            {
                int indice = 0;
                int maxNews = 0;
                if (response.News != null && response.News.Results != null)
                {
                    maxNews = response.News.Results.Length;
                }

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

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="famoso"> Parameter description for famoso goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region  FindFamous(string famoso)
        public static string FindFamous(string famoso)
        {
            ////agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string queryOut = EscapearQuery(famoso);

            ////realiza la busqueda en BING
            SearchResponse response = BingRequest(queryOut);

            string resultado = null;

            if (response.Errors == null)
            {
                int indice = 0;
                int maxNews = 0;
                if (response.News != null && response.News.Results != null)
                {
                    maxNews = response.News.Results.Length;
                }

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

        ////devuelve el string del query con comillas dobles escapedas

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="queryIn"> Parameter description for queryIn goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region EscapearQuery
        static string EscapearQuery(string queryIn)
        {
            var cadenaSplit = queryIn.Split(' ');
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

        ////se trae la noticia

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="query"> Parameter description for query goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region BingRequest
        static SearchResponse BingRequest(string query)
        {
            BingSearchService.BingPortTypeClient client = new BingSearchService.BingPortTypeClient();
            SearchRequest request = new SearchRequest()
            {
                AppId = Constants.AppId,
                Sources = new SourceType[] { SourceType.Web, SourceType.News },
                Adult = AdultOption.Moderate,
                AdultSpecified = true,
                Query = query,
                Market = Constants.Market,
            };
            request.Version = Constants.RequestVersion;
            request.News = new NewsRequest();
            request.News.Offset = Constants.NewsOffset;
            request.News.OffsetSpecified = true;
            request.News.Count = Constants.NewsCount;
            request.News.SortBy = NewsSortOption.Relevance;
            request.News.SortBySpecified = true;

            return client.Search(request);
        }
        #endregion BingRequest

        ////Devuelve caracteres hasta la primer ocurrencia de un punto (.) despues mas de 95 caracteres

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="entrada"> Parameter description for entrada goes here</param>
        /// <param name="query"> Parameter description for query goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region ParsearNoticia
        static string ParsearNoticia(string entrada, string query)
        {
            string resultado = null;
            Regex expRegNoticia;

            string patron1 = @"(.){95}[^\.]{0,105}";
            expRegNoticia = new Regex(patron1, RegexOptions.Multiline);

            Match matchNoticia = expRegNoticia.Match(entrada);
            if (matchNoticia.Length > 0)
            {
                return matchNoticia.ToString();
            }

            return resultado;
        }
        #endregion ParsearNoticia

        /// <summary>
        /// Description for Method.</summary>
        /// <param name="noticia"> Parameter description for noticia goes here</param>
        /// <param name="nuevoTxt"> Parameter description for nuevoTxt goes here</param>
        /// <param name="viejoTxt"> Parameter description for viejoTxt goes here</param>
        /// <returns>
        /// Return results are described through the returns tag.</returns>
        #region ReemplazarTexto
        static string ReemplazarTexto(string noticia, string nuevoTxt, string viejoTxt)
        {
            if (noticia != null)
            {
                Regex expRegQuitarPais = new Regex(viejoTxt, RegexOptions.IgnoreCase);
                string entradaSinPais = expRegQuitarPais.Replace(noticia, nuevoTxt);
                return entradaSinPais;
            }

            return string.Empty;
        }
        #endregion ReemplazarTexto
    }
}