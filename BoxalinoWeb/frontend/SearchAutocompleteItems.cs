using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
   public class SearchAutocompleteItems
    {

        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public BxAutocompleteResponse bxAutocompleteResponse = null;

        public void searchAutocompleteItems()
        {
            /**
* In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
*/

            //include the Boxalino Client SDK php files
            //include the Boxalino Client SDK php files
            //path to the lib folder with the Boxalino Client SDK and PHP Thrift Client files
            //required parameters you should set for this example to work
            string account = string.IsNullOrEmpty(this.account) ? "boxalino_automated_tests" : this.account; // your account name
            string password = string.IsNullOrEmpty(this.password) ? "boxalino_automated_tests" : this.password; // your account password
            string domain = ""; // your web-site domain (e.g.: www.abc.com)
            string[] languages = new string[] { "en" }; //declare the list of available languages
            bool isDev = false; //are the data to be pushed dev or prod data?
            bool isDelta = false; //are the data to be pushed full data (reset index) or delta (add/modify index)?
            List<string> logs = new List<string> { }; //optional, just used here in example to collect logs
            bool print = this.print ?? true;
            //Create the Boxalino Data SDK instance

            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain);

            try
            {
                string language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                string queryText = "whit"; // a search query to be completed
                int textualSuggestionsHitCount = 10; //a maximum number of search textual suggestions to return in one page
                List<string> fieldNames = new List<string>() { "title" }; //return the title for each item returned (globally and per textual suggestion) - IMPORTANT: you need to put "products_" as a prefix to your field name except for standard fields: "title", "body", "discountedPrice", "standardPrice"

                //create search request
                BxAutocompleteRequest bxRequest = new BxAutocompleteRequest(language, queryText, textualSuggestionsHitCount);

                //set the fields to be returned for each item in the response
                bxRequest.getBxSearchRequest().setReturnFields(fieldNames);

                //set the request
                bxClient.setAutocompleteRequest(new List<BxAutocompleteRequest>() { bxRequest });

               // make the query to Boxalino server and get back the response for all requests
                bxAutocompleteResponse = bxClient.getAutocompleteResponse();

                //loop on the search response hit ids and print them
                logs.Add("textual suggestions for \"" + queryText + "\":<br>");
                foreach (var suggestion in bxAutocompleteResponse.getTextualSuggestions())
                {
                    logs.Add("<div style=\"border:1px solid; padding:10px; margin:10px\">");
                    logs.Add("<h3>" + suggestion + "</b></h3>");

                    logs.Add("item suggestions for suggestion \"" + suggestion + "\":<br>");
                    //loop on the search response hit ids and print them
                    foreach (var itemk in bxAutocompleteResponse.getBxSearchResponse(suggestion).getHitFieldValues(fieldNames.ToArray()))
                    {
                        logs.Add("<div>" + itemk.Key + "");
                        foreach (var fValueMap in itemk.Value)
                        {
                            logs.Add(" - " + fValueMap.Key + ":"+string.Join(",", fValueMap.Value.Value) + "");
                        }
                        logs.Add("</div>");
                    }
                    logs.Add("</div>");
                }
                logs.Add("global item suggestions for \"" + queryText + "\":<br>");
                //loop on the search response hit ids and print them
                foreach (var fvalueMap in bxAutocompleteResponse.getBxSearchResponse().getHitFieldValues(fieldNames.ToArray()))
                {
                    string item = fvalueMap.Key;
                    foreach (var itemInfieldValueMap in fvalueMap.Value)
                    {
                        item += " - " + itemInfieldValueMap.Key + ":"+string.Join(",", itemInfieldValueMap.Value.Value) + "<br>";
                    }
                    logs.Add(item);
                }
                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join(" ", logs));
                }

            }
            catch (Exception ex)
            {
                if (print){
                    HttpContext.Current.Response.Write(ex.Message);
                }
            }
        }
    }
}
