using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using InterpoolCloudWebRole.BingSearchService;
using InterpoolCloudWebRole.Data;
using System.Text.RegularExpressions;
using System.IO;

namespace InterpoolCloudWebRole.Utilities
{
    public static class Admin
    {

        #region Buscar (String cadena)
        static string Buscar(String cadena)
        {
            var cadenaSplit = cadena.Split(' ');
            System.Text.StringBuilder builderAux = new System.Text.StringBuilder();
            System.Text.StringBuilder builderQuery = new System.Text.StringBuilder();
            if (cadenaSplit != null)
            {
                builderAux = builderAux.Append("\"").Append(cadenaSplit[0]);
                builderQuery = builderQuery.Append(cadenaSplit[0]);
                for (int index = 1; index < cadenaSplit.Length; index++)
                {
                    builderAux.Append(" ").Append(cadenaSplit[index]);
                    builderQuery = builderQuery.Append(" ").Append(cadenaSplit[index]);
                }
                builderAux.Append("\"");
            }

            System.Text.StringBuilder builder = new System.Text.StringBuilder();
            BingSearchService.BingPortTypeClient client = new BingSearchService.BingPortTypeClient();
            SearchRequest request = new SearchRequest()
            {
                AppId = "A00C4105122186E4F9F0DFD82CDF594DD866BC1F",
                Sources = new SourceType[] { SourceType.Web, SourceType.News },
                Adult = AdultOption.Moderate,
                AdultSpecified = true,
                Query = builderAux.ToString(),
                Market = "es-Mx",
            };
            request.Version = "2.0";

            request.News = new NewsRequest();
            request.News.Offset = 0;
            request.News.OffsetSpecified = true;
            request.News.Count = 10;
            request.News.SortBy = NewsSortOption.Relevance;
            request.News.SortBySpecified = true;

            SearchResponse response = client.Search(request);
            String resultado = null;

            if (response.Errors == null)
            {
                int indice = 0;
                int maxNews = 0;
                if (response.News != null && response.News.Results != null)
                     maxNews = response.News.Results.Length;
                
                while (resultado == null && indice < maxNews)
                {
                    NewsResult result = response.News.Results[indice];
                    builder.Length = 0;
                    resultado = leerPaginaWeb(result.Url, builderQuery.ToString());
                    indice++;
                }
            }
            return resultado;
        }
        #endregion Buscar

        #region leerPaginaWeb
        // Acceder a una página Web usando WebRequest y WebResponse
        static String leerPaginaWeb(string laUrl, string Query)
        {
            // Cear la solicitud de la URL.
            System.Net.WebRequest request2 = System.Net.WebRequest.Create(laUrl);
            
            // Arreglar si el request es un 404
            System.Net.WebResponse response = request2.GetResponse();

            // Abrir el stream de la respuesta recibida.
            StreamReader reader = new StreamReader(response.GetResponseStream(), System.Text.Encoding.Default);
            // Leer el contenido.
            String res = System.Web.HttpUtility.HtmlDecode(reader.ReadToEnd());

            // Cerrar los streams abiertos.
            reader.Close();
            response.Close();

            return ParsearTexto(res, Query);
        }
        #endregion leerPaginaWeb

        #region ParsearTexto
        static String ParsearTexto(string entrada, string Query)
        {
            String resultado = null;
            Regex expRegNoticia;
            String patron1 = @"(<p>(([^<]*</?([a-oq-zA-OQ-Z])+\s?/?>)*([\w-\.ñÑáéíóúÁÉÍÓÚ\s\n\r\t,])*)(";
            patron1 += Query;
            String patron2 = @")([^<]+<((/p>)|(([/a-oq-zA-OQ-Z\t\r\n])+)[^>]+>[\t\r\n\s]*)))";
            patron1 += patron2;

            expRegNoticia = new Regex(patron1, RegexOptions.Multiline);
            Match matchNoticia = expRegNoticia.Match(entrada);
            if (matchNoticia.Length > 0)
            {
                string pattern = @"(([\t])|(</?([^>])*\s?/?>))*";
                Regex expRegQuitar = new Regex(pattern);
                return expRegQuitar.Replace(matchNoticia.Result("$1"), "");
            }
            return resultado;
        }
        #endregion 

        public static void loadFamousData()
        {
            string news = "";

            InterpoolContainer container = new InterpoolContainer();
            New newsF;

            foreach (Famous f in container.Famous)
            {
                
                //Se trae la noticia
                news = Buscar(f.FamousName);
                if (news != null)
                {
                    newsF = new New();
                    newsF.NewContent = news;
                    newsF.Famous = f;

                    container.AddToNews(newsF);
                }
            }
            container.SaveChanges();
            
        }
    }
}