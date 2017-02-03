using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace examples.frontend
{
    public class SearchSortField
    {

        public string account { get; set; }
        public string password { get; set; }

        public bool? print { get; set; }

        public BxChooseResponse bxResponse = null;
        public void searchSortField()
        {

            string account = string.IsNullOrEmpty(this.account) ? "csharp_unittest" : this.account; // your account name
            string password = string.IsNullOrEmpty(this.password) ? "csharp_unittest" : this.password; // your account password

            string domain = ""; // your web-site domain (e.g.: www.abc.com)
            List<string> logs = new List<string>(); //optional, just used here in example to collect logs
            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain);

            try
            {
                string language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                string queryText = "women"; // a search query
                int hitCount = 10; //a maximum number of search result to return in one page
                string sortField = "title"; //sort the search results by this field - IMPORTANT: you need to put "products_" as a prefix to your field name except for standard fields: "title", "body", "discountedPrice", "standardPrice"
                bool sortDesc = true; //sort in an ascending / descending way
                //create search request
                BxRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //add a sort field in the provided direction
                bxRequest.addSortField(sortField, sortDesc);

                //set the fields to be returned for each item in the response
                bxRequest.setReturnFields(new List<string> { sortField });

                //add the request
                bxClient.addRequest(bxRequest);

                //make the query to Boxalino server and get back the response for all requests
                bxResponse = bxClient.getResponse();

                //loop on the search response hit ids and print them
                NestedDictionary<string, object> HitFieldValues = bxResponse.getHitFieldValues(new string[] { sortField });

                foreach (var obj in HitFieldValues)
                {
                    string Id = obj.Key;
                    NestedDictionary<string, object> fieldValueMap = (NestedDictionary<string, object>)obj.Value;

                    string product = "<h3>" + Id + "</h3>";
                    try
                    {
                        foreach (var item in fieldValueMap)
                        {
                            product += item.Key + ": " + string.Join(",", (List<string>)(item.Value).Value);
                        }                      
                    }
                    catch { }
                    logs.Add(product);
                }

                System.Web.HttpContext.Current.Response.Write(String.Join("<br>", logs));
            }
            catch (Exception ex)
            {


            }
        }
    }
}
