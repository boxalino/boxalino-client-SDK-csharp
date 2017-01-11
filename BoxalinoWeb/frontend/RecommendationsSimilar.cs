using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
    class RecommendationsSimilar
    {
        public void recommendationsSimilar()
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

            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain);
            try
            {
                string language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                string choiceId = "similar"; //the recommendation choice id (standard choice ids are: "similar" => similar products on product detail page, "complementary" => complementary products on product detail page, "basket" => cross-selling recommendations on basket page, "search"=>search results, "home" => home page personalized suggestions, "category" => category page suggestions, "navigation" => navigation product listing pages suggestions)
                string itemFieldId = "id"; // the field you want to use to define the id of the product (normally id, but could also be a group id if you have a difference between group id and sku) 
                string itemFieldIdValue = "1940"; //the product id the user is currently looking at
                int hitCount = 10; //a maximum number of recommended result to return in one page

                //create similar recommendations request
                BxRecommendationRequest bxRequest = new BxRecommendationRequest(language, choiceId, hitCount);

              //indicate the product the user is looking at now (reference of what the recommendations need to be similar to)
             	bxRequest.setProductContext(itemFieldId, itemFieldIdValue);


                //add the request
                bxClient.addRequest(bxRequest);

               
                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();

               
                foreach (var item in   bxResponse.getHitIds()) {
		         logs.Add(item.Key+": returned id "+ item.Value);
                }
                if (print){
                    HttpContext.Current.Response.Write(string.Join("<br>", logs));
                }
            }
            catch (Exception ex)
            {
                if (print)
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
