using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
    class SearchFacetCategory
    {

        public void searchFacetCategory()
        {
            /**
         * In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
          */

            //include the Boxalino Client SDK php files
            //include the Boxalino Client SDK php files
            //path to the lib folder with the Boxalino Client SDK and PHP Thrift Client files
            //required parameters you should set for this example to work
            string account = "csharp_unittest"; // your account name
            string password = "csharp_unittest"; // your account password
            string domain = ""; // your web-site domain (e.g.: www.abc.com)
            string[] languages = new string[] { "en" }; //declare the list of available languages
            bool isDev = false; //are the data to be pushed dev or prod data?
            bool isDelta = false; //are the data to be pushed full data (reset index) or delta (add/modify index)?
            List<string> logs = new List<string> { }; //optional, just used here in example to collect logs
            bool print = true;
            //Create the Boxalino Data SDK instance
            BxClient bxClient = new BxClient(account, password, domain);
            try
            {
                string language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                string queryText = "women"; // a search query
                int hitCount = 10; //a maximum number of search result to return in one page
                string selectedValue = HttpContext.Current.Request["bx_category_id"] != null ? HttpContext.Current.Request["bx_category_id"] : null;

                //create search request
                BxRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //add a facert
                BxFacets facets = new BxFacets();
                facets.addCategoryFacet(selectedValue);
                bxRequest.setFacets(facets);

                //add the request
                bxClient.addRequest(bxRequest);
               

                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();

                //get the facet responses
                facets = bxResponse.getFacets();

                //show the category breadcrumbs
                int level = 0;
                logs.Add("<a href=\"?\">home</a>");
                foreach (var item in facets.getParentCategories())
                {

                    logs.Add(">> <a href=\"?bx_category_id=" + item.Key + "\">" + item.Value + "</a>");
                    level++;
                }
                logs.Add(" ");
                //show the category facet values
                foreach (var value in facets.getCategories())
                {
                    logs.Add("<a href=\"?bx_category_id=" + facets.getCategoryValueId(value.Value) + "\">" + facets.getCategoryValueLabel(value.Value) + "</a> (" + facets.getCategoryValueCount(value.Value) + ")");

                }
                logs.Add(" ");

                //loop on the search response hit ids and print them
                foreach (var item in  bxResponse.getHitIds()) {
		            logs.Add(item.Key+": returned id "+item.Value+"");
                }

                if (print){
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
            catch (Exception ex)
            {
                if ((print))
                {
                    HttpContext.Current.Response.Write(ex.Message.ToString());
                }
            }
        }

        public void Print<T>(T x)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }

    }
}
