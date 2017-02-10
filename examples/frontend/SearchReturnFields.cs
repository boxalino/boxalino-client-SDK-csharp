using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
    public class SearchReturnFields
    {

        #region declaration
        public string account { get; set; }
        public string password { get; set; }
        string domain { get; set; }
        string language { get; set; }
        List<string> logs { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }
        public bool? print { get; set; }
        List<string> fieldNames { get; set; }

        public BxChooseResponse bxResponse = null;
        #endregion
        public void searchReturnFields()
        {

            fieldNames = new List<string>();
            try
            {
                string account = string.IsNullOrEmpty(this.account) ? "" : this.account; // your account name
                string password = string.IsNullOrEmpty(this.password) ? "" : this.password; // your account password
              
                domain = "";// your web-site domain (e.g.: www.abc.com)
                language = "en";// a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women";// a search query
                hitCount = 10;//a maximum number of search result to return in one page
                logs = new List<string>();//optional, just used here in example to collect logs
                fieldNames.Add("products_color"); //IMPORTANT: you need to put "products_" as a prefix to your field name except for standard fields: "title", "body", "discountedPrice", "standardPrice"
                bool print = this.print ?? true;
                //Create the Boxalino Client SDK instance
                //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
                BxClient bxClient = new BxClient(account, password, domain);



                //create search request
                BxSearchRequest bxrequest = new BxSearchRequest(language, queryText, hitCount);

                //set the fields to be returned for each item in the response
                bxrequest.setReturnFields(fieldNames);

                //add the request
                bxClient.addRequest(bxrequest);
                //make the query to Boxalino server and get back the response for all requests
                bxResponse = bxClient.getResponse();

                foreach (var obj in bxResponse.getHitFieldValues(fieldNames.ToArray()))
                {
                    string Id = obj.Key;
                    NestedDictionary<string, object> fieldValueMap = (NestedDictionary<string, object>)obj.Value;

                    string entity = "<h3>" + Id + "</h3>";
                  
                    foreach (var item in fieldValueMap)
                    {
                        entity += item.Key + ": " + string.Join(",", (List<string>)(item.Value).Value);
                    }
                    logs.Add(entity);
                }

                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
          
            catch (Exception ex)
            {

            }
        }
    }
}
