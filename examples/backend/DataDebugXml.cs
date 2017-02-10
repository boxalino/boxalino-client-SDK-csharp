using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace examples.backend
{
    public class DataDebugXml
    {
        public void dataDebugXml()
        {
            /*In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
            */

          
            //path to the lib folder with the Boxalino Client SDK and C# Thrift Client files
            //required parameters you should set for this example to work
            string account = ""; // your account name
            string password = ""; // your account password
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

                //declare the fields
                bxData.addSourceTitleField(sourceKey, new Dictionary<string, string>() { { "en", "name_en" } });
                bxData.addSourceDescriptionField(sourceKey, new Dictionary<string, string>() { { "en", "description_en" } });
                bxData.addSourceListPriceField(sourceKey, "list_price");
                bxData.addSourceDiscountedPriceField(sourceKey, "discounted_price");
                bxData.addSourceLocalizedTextField(sourceKey, "short_description", new Dictionary<string, string> { { "en", "short_description_en" } });
                bxData.addSourceStringField(sourceKey, "sku", "sku");

                if (print)
                {
                    HttpContext.Current.Response.Write(HttpUtility.HtmlDecode(bxData.getXML().InnerXml));
                }
            }
            catch (Exception ex)
            {

                if (print)
                {
                    HttpContext.Current.Response.Write(ex.Message);
                }

            }

        }
    }
}