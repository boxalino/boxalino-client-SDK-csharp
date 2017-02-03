using boxalino_client_SDK_CSharp;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
   public class RecommendationsBasket
    {
        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public  BxChooseResponse bxResponse = null;

        public void recommendationsBasket()
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
                string choiceId = "basket"; //the recommendation choice id (standard choice ids are: "similar" => similar products on product detail page, "complementary" => complementary products on product detail page, "basket" => cross-selling recommendations on basket page, "search"=>search results, "home" => home page personalized suggestions, "category" => category page suggestions, "navigation" => navigation product listing pages suggestions)
                string itemFieldId = "id"; // the field you want to use to define the id of the product (normally id, but could also be a group id if you have a difference between group id and sku)
              
                List<CustomBasketContent> itemFieldIdValuesPrices = new List<CustomBasketContent>(); //the product ids and their prices that the user currently has in his basket

                CustomBasketContent customBasketContent = new CustomBasketContent();
                customBasketContent.Id = "1940";
                customBasketContent.Price = "10.80";
                itemFieldIdValuesPrices.Add(customBasketContent);
                customBasketContent = new CustomBasketContent();
                customBasketContent.Id = "1234";
                customBasketContent.Price = "130.5";
                itemFieldIdValuesPrices.Add(customBasketContent);

                int hitCount = 10; //a maximum number of recommended result to return in one page


                //create similar recommendations request
                BxRecommendationRequest bxRequest = new BxRecommendationRequest(language, choiceId, hitCount);

                	//indicate the products the user currently has in his basket (reference of products for the recommendations)
	            bxRequest.setBasketProductWithPrices(itemFieldId, itemFieldIdValuesPrices); 

                //add the request
                bxClient.addRequest(bxRequest);

               

                //make the query to Boxalino server and get back the response for all requests
                  bxResponse = bxClient.getResponse();


             
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

       

        public  void Print<T>(T x)
        {
            string json = JsonConvert.SerializeObject(x, Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }
    }
}
