using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.backend
{
    public class DataBasic
    {
        public void dataBasic()
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
            BxData bxData = new BxData(new BxClient(account, password, domain), languages, isDev, isDelta);

            try
            {
                string file = HttpContext.Current.Server.MapPath("~/SampleData/products.csv"); //a csv file with header row
                string itemIdColumn = "id"; //the column header row name of the csv with the unique id of each item

                //add a csv file as main product file
                string sourceKey = bxData.addMainCSVItemFile(file, itemIdColumn);

                //this part is only necessary to do when you push your data in full, as no specifications changes should not be published without a full data sync following next
                //even when you publish your data in full, you don't need to repush your data specifications if you know they didn't change, however, it is totally fine (and suggested) to push them everytime if you are not sure if something changed or not
                if (!isDelta)
                {
                    //declare the fields
                    bxData.addSourceTitleField(sourceKey, new Dictionary<string, string>() { { "en", "name_en" } });
                    bxData.addSourceDescriptionField(sourceKey, new Dictionary<string, string>() { { "en", "description_en" } });
                    bxData.addSourceListPriceField(sourceKey, "list_price");
                    bxData.addSourceDiscountedPriceField(sourceKey, "discounted_price");
                    bxData.addSourceLocalizedTextField(sourceKey, "short_description", new Dictionary<string, string>() { { "en", "short_description_en" } });
                    bxData.addSourceStringField(sourceKey, "sku", "sku");


                    logs.Add("publish the data specifications");
                    bxData.pushDataSpecifications();

                    logs.Add("publish the api owner changes"); //if the specifications have changed since the last time they were pushed
                    bxData.publishChanges();

                }
                logs.Add("push the data for data sync");
                bxData.pushData();
                if (print) {
                    HttpContext.Current.Response.Write(string.Join("<br/>", logs));
                }
            }
            catch (Exception ex)
            {
                //be careful not to print the error message on your publish web-site as sensitive information like credentials might be indicated for debug purposes

                if (print) {
                    HttpContext.Current.Response.Write(ex.Message);
                }
            }
        }
    }
}
