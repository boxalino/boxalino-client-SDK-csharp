using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boxalino_client_SDK_CSharp;
using System.Web;
using System.IO;

namespace examples.backend
{
    public class DataCategories
    {
        public void dataCategories()
        {
            /**
            * In this example, we take a very simple CSV file with product and categories data (and the link between them), generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
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

                string mainProductFile = HttpContext.Current.Server.MapPath("~/SampleData/products.csv"); //a csv file with header row
                string itemIdColumn = "id"; //the column header row name of the csv with the unique id of each item

                string categoryFile = HttpContext.Current.Server.MapPath("~/SampleData/categories.csv");  //a csv file with header row
                string categoryIdColumn = "category_id"; //column header row name of the csv with the unique category id
                string parentCategoryIdColumn = "parent_id"; //column header row name of the csv with the parent category id
                Dictionary<string, object> categoryLabelColumns = new Dictionary<string, object>() { { "en", "value_en" } }; //column header row names of the csv with the category label in each language

                string productToCategoriesFile = HttpContext.Current.Server.MapPath("~/SampleData/product_categories.csv");  //a csv file with header row

                //add a csv file as main product file
                string mainSourceKey = bxData.addMainCSVItemFile(mainProductFile, itemIdColumn);

                ////add a csv file with products ids to categories ids
                string productToCategoriesSourceKey = bxData.addCSVItemFile(productToCategoriesFile, itemIdColumn);

                ////add a csv file with categories
                bxData.addCategoryFile(categoryFile, categoryIdColumn, parentCategoryIdColumn, categoryLabelColumns);

                ////this part is only necessary to do when you push your data in full, as no specifications changes should not be published without a full data sync following next
                ////even when you publish your data in full, you don't need to repush your data specifications if you know they didn't change, however, it is totally fine (and suggested) to push them everytime if you are not sure if something changed or not
                if (!isDelta)
                {

                    //set the category field from the source mapping products to category ids (indicating which column of that file contains the category_id)
                    bxData.setCategoryField(productToCategoriesSourceKey, categoryIdColumn);

                    logs.Add("publish the data specifications");
                    bxData.pushDataSpecifications();

                    logs.Add("publish the api owner changes"); //if the specifications have changed since the last time they were pushed
                    bxData.publishChanges();
                }

                logs.Add("push the data for data sync");
                bxData.pushData();
                if (print)
                {
                   
                    HttpContext.Current.Response.Write(string.Join("<br/>", logs));
                }
            }
            catch (Exception e)
            {

                //be careful not to print the error message on your publish web-site as sensitive information like credentials might be indicated for debug purposes
                if (print)
                {
                    HttpContext.Current.Response.Write(e.Message);  
                }
            }

        }
    }
}
