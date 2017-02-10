using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
    public class SearchRequestContextParameters
    {

        public string account { get; set; }
        public string password { get; set; }
        string domain { get; set; }
        List<string> logs { get; set; }
        string language { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }
        public bool? print { get; set; }

        public BxChooseResponse bxResponse = null;
        public void searchRequestContextParameters()
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
            try
            {
                language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women"; // a search query
                hitCount = 10; //a maximum number of search result to return in one page

                Dictionary<string, List<string>> requestParameters = new Dictionary<string, List<string>>();
                requestParameters.Add("geoIP-latitude", new List<string>() { "47.36" });
                requestParameters.Add("geoIP-longitude", new List<string>() { "6.1517993" });


                //create search request
                BxRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);
                //set the fields to be returned for each item in the response
                foreach (var item in requestParameters)
                {
                    bxClient.addRequestContextParameter(item.Key, item.Value);
                }
                //add the request
                bxClient.addRequest(bxRequest);

                //make the query to Boxalino server and get back the response for all requests
                bxResponse = bxClient.getResponse();

                //indicate the search made with the number of results found
                logs.Add("Results for query \"" + queryText + "\" (" + bxResponse.getTotalHitCount().ToString() + "):<br>");

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
    }
}
