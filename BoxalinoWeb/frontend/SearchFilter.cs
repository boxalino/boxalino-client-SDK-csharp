using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoxalinoWeb.frontend
{
    public class SearchFilter
    {

        string account { get; set; }
        string password { get; set; }
        string domain { get; set; }
        List<string> logs { get; set; }
        string language { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }
        string print { get; set; }
        string filterField { get; set; }
        List<string> filterValues { get; set; }
        bool filterNegative { get; set; }
        public void searchFilter()
        {
            /**
            * In this example, we make a simple search query, add a filter and get the search results and print their ids
            * Filters are different than facets because they are not returned to the user and should not be related to a user interaction
            * Filters should be "system" filters (e.g.: filter on a category within a category page, filter on product which are visible and not out of stock, etc.)
            */


            //required parameters you should set for this example to work
            account = "csharp_unittest";
            password = "csharp_unittest";
            domain = ""; // your web-site domain (e.g.: www.abc.com)
            logs = new List<string>(); //optional, just used here in example to collect logs
            bool print = true;
            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain);
            try
            {
                language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women"; // a search query
                hitCount = 10; //a maximum number of search result to return in one page
                filterField = "id"; //the field to consider in the filter
                filterValues = new List<string>() { "41", "1940" }; //the field to consider any of the values should match (or not match)
                filterNegative = true; //false by default, should the filter match the values or not?


                //create search request
                BxSearchRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);
                //add a filter
                bxRequest.addFilter(new BxFilter(filterField, filterValues, filterNegative));
               

                //add the request
                bxClient.addRequest(bxRequest);

                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();

                //loop on the search response hit ids and print them
                foreach (var item in bxResponse.getHitIds())
                {
                    logs.Add(item.Key + ": returned id " + item.Value);
                }

                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.ToString());
            }
        }
        public void Print<T>(T x)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }
    }
}