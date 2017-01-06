using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
    public class SearchFacetPrice
    {
        string account { get; set; }
        string password { get; set; }
        string domain { get; set; }
        List<string> logs { get; set; }

        string language { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }
        bool print { get; set; }

        string selectedValue { get; set; }
        public void searchFacetPrice()
        {

            /**
            * In this example, we make a simple search query, request a facet and get the search results and print the facet values and counter for price ranges.
            * We also implement a simple link logic so that if the user clicks on one of the facet values the page is reloaded with the results with this facet value selected.
            */

            //required parameters you should set for this example to work
            account = "csharp_unittest";
            password = "csharp_unittest";
            domain = ""; // your web-site domain (e.g.: www.abc.com)
            logs = new List<string>(); //optional, just used here in example to collect logs


            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain);

            try
            {
                language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women"; // a search query
                hitCount = 10; //a maximum number of search result to return in one page
                print = true;
                selectedValue = HttpContext.Current.Request.Form["bx_price"] == null ? Convert.ToString(HttpContext.Current.Request.Form["bx_price"]) : null;

                //create search request
                BxSearchRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //add a facert
                BxFacets facets = new BxFacets();
                //facets.addPriceRangeFacet(selectedValue);

                facets.addPriceRangeFacet(selectedValue);
                bxRequest.setFacets(facets);

                //set the fields to be returned for each item in the response
                bxRequest.setReturnFields(new List<string>() { facets.getPriceFieldName() });

                //add the request
                bxClient.addRequest(bxRequest);
               

                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();

                //get the facet responses
                facets = bxResponse.getFacets();

                //loop on the search response hit ids and print them
                foreach (var fieldValue in facets.getPriceRanges())
                {
                    string range = "<a href=\"?bx_price=" + facets.getPriceValueParameterValue(fieldValue.Value) + "\">" + facets.getPriceValueLabel(fieldValue.Value) + "</a> (" + facets.getPriceValueCount(fieldValue.Value) + ")";
                    if (string.IsNullOrEmpty(facets.isPriceValueSelected(fieldValue.Value)))
                    {
                        range += "<a href=\"?\">[X]</a>";
                    }
                    logs.Add(range);
                }


                //loop on the search response hit ids and print them
                foreach (var item in  bxResponse.getHitFieldValues((new List<string>(){facets.getPriceFieldName()}).ToArray())) {
		          logs.Add("<h3>"+item.Key+"</h3>");
                    foreach (var fieldValueMapItem  in item.Value) {
                        logs.Add("Price: " + string.Join(",", (fieldValueMapItem.Value).Value));
                    }
                }
                if (print){
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
