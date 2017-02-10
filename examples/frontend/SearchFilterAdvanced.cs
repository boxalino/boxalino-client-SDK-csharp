using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
    public class SearchFilterAdvanced
    {

       public string account { get; set; }
        public string password { get; set; }
        string domain { get; set; }
        List<string> logs { get; set; }
        string language { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }
        public bool? print { get; set; }
        string filterField { get; set; }
        List<string> filterValues { get; set; }
        bool filterNegative { get; set; }

        string filterField2 { get; set; }

        List<string> filterValues2 { get; set; }

        bool filterNegative2 { get; set; }

        bool orFilters { get; set; }

        List<string> fieldNames { get; set; }

        public BxChooseResponse bxResponse = null;
        public void searchFilterAdvanced()
        {
            try
            {
                // required parameters you should set for this example to work
                string account = string.IsNullOrEmpty(this.account) ? "" : this.account; // your account name
                string password = string.IsNullOrEmpty(this.password) ? "" : this.password; // your account password
                domain = ""; // your web-site domain (e.g.: www.abc.com)
                logs = new List<string>(); //optional, just used here in example to collect logs
                bool print = this.print ?? true;
                //Create the Boxalino Client SDK instance
                //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
                BxClient bxClient = new BxClient(account, password, domain);

                language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women"; // a search query
                hitCount = 10; //a maximum number of search result to return in one page
                filterField = "id"; //the field to consider in the filter
                filterValues = new List<string>() { "41", "1941" }; //the field to consider any of the values should match (or not match)
                filterNegative = true; //false by default, should the filter match the values or not?
                filterField2 = "products_color"; //the field to consider in the filter
                filterValues2 = new List<string>() { "Yellow" }; //the field to consider any of the values should match (or not match)
                filterNegative2 = false; //false by default, should the filter match the values or not?
                orFilters = true; //the two filters are either or (only one of them needs to be correct
                fieldNames = new List<string>() { "products_color" }; //IMPORTANT: you need to put "products_" as a prefix to your field name except for standard fields: "title", "body", "discountedPrice", "standardPrice"

                //create search request
                BxSearchRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //set the fields to be returned for each item in the response
	            bxRequest.setReturnFields(fieldNames);

                //add a filter
            	bxRequest.addFilter(new BxFilter(filterField, filterValues, filterNegative));
	            bxRequest.addFilter(new BxFilter(filterField2, filterValues2, filterNegative2));
	            bxRequest.setOrFilters(orFilters);


                //add the request
	            bxClient.addRequest(bxRequest);

                //make the query to Boxalino server and get back the response for all requests
	            bxResponse = bxClient.getResponse();

                //loop on the search response hit ids and print them
                foreach (var item in bxResponse.getHitFieldValues(fieldNames.ToArray()))
                {
                    logs.Add("<h3>" + item.Key + "</h3>");
                    foreach (var val in item.Value)
                    {
                        logs.Add(val.Key + ": " + string.Join(",", (List<string>)(val.Value).Value));
                    }
                }

                if ((print))
                {
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.ToString());
            }
        }
    }
}
