using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
    class RecommendationsSimilarComplementary
    {

        public void recommendationsSimilarComplementary()
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
                string choiceIdSimilar = "similar"; //the recommendation choice id (standard choice ids are: "similar" => similar products on product detail page, "complementary" => complementary products on product detail page, "basket" => cross-selling recommendations on basket page, "search"=>search results, "home" => home page personalized suggestions, "category" => category page suggestions, "navigation" => navigation product listing pages suggestions)
                string choiceIdComplementary = "complementary";
                string itemFieldId = "id"; // the field you want to use to define the id of the product (normally id, but could also be a group id if you have a difference between group id and sku)
                string itemFieldIdValue = "1940"; //the product id the user is currently looking at
                int hitCount = 10; //a maximum number of recommended result to return in one page


                //create similar recommendations request
                BxRecommendationRequest bxRequestSimilar = new BxRecommendationRequest(language, choiceIdSimilar, hitCount);
             
             
                //add the request
                bxClient.addRequest(bxRequestSimilar);


                //create complementary recommendations request
                BxRecommendationRequest bxRequestComplementary = new BxRecommendationRequest(language, choiceIdComplementary, hitCount);
            
            
                //add the request
                bxClient.addRequest(bxRequestComplementary);

             

                //make the query to Boxalino server and get back the response for all requests (make sure you have added all your requests before calling getResponse; i.e.: do not push the first request, then call getResponse, then add a new request, then call getResponse again it wil not work; N.B.: if you need to do to separate requests call, then you cannot reuse the same instance of BxClient, but need to create a new one)
                BxChooseResponse bxResponse = bxClient.getResponse();

                //loop on the recommended response hit ids and print them
                logs.Add("recommendations of similar items:");
                foreach (var item in bxResponse.getHitIds(choiceIdSimilar))
                {
                    logs.Add(item.Key + ": returned id " + item.Value);
                }
                logs.Add("");
                //retrieve the recommended responses object of the complementary request
                logs.Add("recommendations of complementary items:");
                //loop on the recommended response hit ids and print them
                foreach (var itemHitId in   bxResponse.getHitIds(choiceIdComplementary)) {
	              logs.Add(itemHitId.Key + ": returned id "+ itemHitId.Value);
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
