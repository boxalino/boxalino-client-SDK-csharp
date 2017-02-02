using boxalino_client_SDK_CSharp;
using boxalino_client_SDK_CSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
    public class SearchAutocompleteProperty
    {
        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public BxAutocompleteResponse bxAutocompleteResponse = null;

        public void searchAutocompleteProperty()
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
                string queryText = "a"; // a search query to be completed
                int textualSuggestionsHitCount = 10; //a maximum number of search textual suggestions to return in one page
                string property = "categories"; //the properties to do a property autocomplete request on, be careful, except the standard "categories" which always work, but return values in an encoded way with the path ( "ID/root/level1/level2"), no other properties are available for autocomplete request on by default, to make a property "searcheable" as property, you must set the field parameter "propertyIndex" to "true"
                int propertyTotalHitCount = 5; //the maximum number of property values to return
                bool propertyEvaluateCounters = true; //should the count of results for each property value be calculated? if you do not need to retrieve the total count for each property value, please leave the 3rd parameter empty or set it to false, your query will go faster

                //create search request
                BxAutocompleteRequest bxRequest = new BxAutocompleteRequest(language, queryText, textualSuggestionsHitCount);

                //indicate to the request a property index query is requested
                bxRequest.addPropertyQuery(property, propertyTotalHitCount, true);

                //set the request
                bxClient.setAutocompleteRequest(new List<BxAutocompleteRequest>() { bxRequest });
                //make the query to Boxalino server and get back the response for all requests               

                bxAutocompleteResponse = (BxAutocompleteResponse)bxClient.getAutocompleteResponse();

                //loop on the search response hit ids and print them
                logs.Add("property suggestions for \"" + queryText + "\":<br>");

                foreach (var hitValue in bxAutocompleteResponse.getPropertyHitValues(property))
                {
                    string label = bxAutocompleteResponse.getPropertyHitValueLabel(property, hitValue);
                    long totalHitCount = bxAutocompleteResponse.getPropertyHitValueTotalHitCount(property, hitValue);
                    string result = "<b>" + hitValue + ":</b><ul><li>label=" + label + "</li> <li>totalHitCount=" + totalHitCount + "</li></ul>";
                    logs.Add(result);
                }

                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }
      

        public void Print<T>(T x)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x,
                    Newtonsoft.Json.Formatting.None,
                    new Newtonsoft.Json.JsonSerializerSettings
                    {
                        NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore,
                        Formatting = Newtonsoft.Json.Formatting.Indented
                    }); ;
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }

    }
}
