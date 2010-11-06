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
        /// Load Famous Data
        /// </summary>
        public static void LoadFamousData()
        {
            string news = String.Empty;

            InterpoolContainer container = new InterpoolContainer();
            New newsF;

            foreach (Famous f in container.Famous)
            {
                ////Se trae la noticia
                news = FindFamous(f.FamousName);

                if (news != null)
                {
                    if (container.News.Where(noticia => noticia.Famous.FamousId == f.FamousId).Count() > 0)

                        container.News.Where(noticia => noticia.Famous.FamousId == f.FamousId).FirstOrDefault().NewContent = news;
                    else
                    {
                        newsF = new New();
                        newsF.NewContent = news;
                        newsF.Famous = f;
                        container.AddToNews(newsF);
                    }
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
                    if (container.CityPropertySet.Where(cp => cp.City.CityId == c.CityId).Count() > 0)

                        container.CityPropertySet.Where(cp => cp.City.CityId == c.CityId).FirstOrDefault().CityPropertyContent = news;

                    else
                    {
                        newsCity = new CityProperty();
                        newsCity.CityPropertyContent = news;
                        newsCity.City = c;
                        newsCity.Dyn = true;
                        container.AddToCityPropertySet(newsCity);
                    }

                    
                }
            }

            container.SaveChanges();
        }

        ////FindCity()  "Este País" -- "Esta Ciudad"
        #region FindCity (String ciudad, String country)
        /// <summary>
        /// function: Find City
        /// </summary>
        /// <param name="city">Parameter description for city goes here</param>
        /// <param name="country">Parameter description for context country here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string FindCity(string city, string country)
        {
            ////agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string queryOut = EscapearQuery(city);

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
                    resultado = ParsearNoticia(result.Snippet, city);
                    indice++;
                }
            }

            if (country != null)
            {
                resultado = QuitarTildes(resultado);
                country = QuitarTildes(country);
                resultado = ReemplazarTexto(resultado, "Este País", country);
                city = QuitarTildes(city);
                return ReemplazarTexto(resultado, "Esta Ciudad", city);
            }

            return resultado;
        }
        #endregion FindCity

        ////"Yo"
        #region  FindFamous(String famoso)
        /// <summary>
        /// Find Famous
        /// </summary>
        /// <param name="famous">Parameter description for famous goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string FindFamous(string famous)
        {
            ////agrega comillas dobles escapeadas para que devuelva ocurrencias de toda la cadena
            string queryOut = EscapearQuery(famous);

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
                    resultado = ParsearNoticia(result.Snippet, famous);
                    indice++;
                }
            }

            resultado = QuitarTildes(resultado);
            famous = QuitarTildes(famous);
            return ReemplazarTexto(resultado, "Yo", famous);
        }
        #endregion  FindFamous

        ////devuelve el String del query con comillas dobles escapedas
        #region EscapearQuery
        /// <summary>
        /// Escapear Query
        /// </summary>
        /// <param name="queryIn">Parameter description for queryIn goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string EscapearQuery(string queryIn)
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
        #region BingRequest
        /// <summary>
        /// Bing Rquest
        /// </summary>
        /// <param name="query">Parameter description for queryIn goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static SearchResponse BingRequest(string query)
        {
            BingSearchService.BingPortTypeClient client = new BingSearchService.BingPortTypeClient();
            SearchRequest request = new SearchRequest()
            {
                AppId = Constants.AppId, Sources = new SourceType[] 
                { 
                    SourceType.Web, SourceType.News 
                }, Adult = AdultOption.Moderate, AdultSpecified = true, Query = query, Market = Constants.Market, 
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
        #region ParsearNoticia
        /// <summary>
        /// Parsear Noticia
        /// </summary>
        /// <param name="entry">Parameter description for entry goes here</param>
        /// <param name="query">Parameter description for query goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string ParsearNoticia(string entry, string query)
        {
            string resultado = null;
            Regex expRegNoticia;

            string patron1 = @"(.){95}[^\.]{0,105}";
            expRegNoticia = new Regex(patron1, RegexOptions.Multiline);

            Match matchNoticia = expRegNoticia.Match(entry);
            if (matchNoticia.Length > 0)
            {
                return matchNoticia.ToString();
            }

            return resultado;
        }
        #endregion ParsearNoticia

        #region ReemplazarTexto
        /// <summary>
        /// Reemplazo Texto
        /// </summary>
        /// <param name="news">Parameter description for news goes here</param>
        /// <param name="newText">Parameter description for newText goes here</param>
        /// <param name="oldText">Parameter description for oldText goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string ReemplazarTexto(string news, string newText, string oldText)
        {
            if (news != null)
            {
                Regex expRegReplace = new Regex(oldText, RegexOptions.IgnoreCase);
                return expRegReplace.Replace(news, newText);
            }

            return String.Empty;
        }
        #endregion ReemplazarTexto

        #region QuitarTildes
        /// <summary>
        /// Quitar Tildes
        /// </summary>
        /// <param name="entry">Parameter description for entry goes here</param>
        /// <returns>Return results are described through the returns tag.</returns>
        public static string QuitarTildes(string entry)
        {
            if (entry != null)
            {
                char[] arr = entry.ToCharArray();
                for (int i = 0; i < arr.Length; i++)
                {
                    switch (arr[i])
                    {
                        case 'á':
                            arr[i] = 'a';
                            break;
                        case 'é':
                            arr[i] = 'e';
                            break;
                        case 'í':
                            arr[i] = 'i';
                            break;
                        case 'ó':
                            arr[i] = 'o';
                            break;
                        case 'ú':
                            arr[i] = 'u';
                            break;
                    }
                }

                return new string(arr);
            }

            return String.Empty;
        }
        #endregion QuitarTildes
    }
}