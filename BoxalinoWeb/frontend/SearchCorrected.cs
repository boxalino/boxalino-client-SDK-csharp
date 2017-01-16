using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
   public class SearchCorrected
    {
        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public BxChooseResponse bxResponse = null;
        public void searchCorrected()
        {
            /**
        * In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
         */

            //include the Boxalino Client SDK php files
            //include the Boxalino Client SDK php files
            //path to the lib folder with the Boxalino Client SDK and PHP Thrift Client files
            //required parameters you should set for this example to work
            string account = string.IsNullOrEmpty(this.account) ? "csharp_unittest" : this.account; // your account name
            string password = string.IsNullOrEmpty(this.password) ? "csharp_unittest" : this.password; // your account password
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
                string queryText = "womem"; // a search query
                int hitCount = 10; //a maximum number of search result to return in one page

                //create search request
                BxSearchRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //add the request
                bxClient.addRequest(bxRequest);

             

                //make the query to Boxalino server and get back the response for all requests
                bxResponse = bxClient.getResponse();

                //if the query is corrected, then print the corrrect query text
                if (bxResponse.areResultsCorrected())
                {
                    logs.Add("Corrected query \"" + queryText + "\" into \"" + bxResponse.getCorrectedQuery() + "\"");
                }

                //loop on the search response hit ids and print them
                foreach (var item in bxResponse.getHitIds())
                {
                    logs.Add("" + item.Key + ": returned id " + item.Value + "");
                }

                if (bxResponse.getHitIds().Count == 0)
                {
                    logs.Add("There are no corrected results. This might be normal, but it also might mean that the first execution of the corpus preparation was not done and published yet. Please refer to the example backend_data_init and make sure you have done the following steps at least once: 1) publish your data 2) run the prepareCorpus case 3) publish your data again");
                }
                if (print){
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
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }

    }
}
