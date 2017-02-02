using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.backend
{
   public class DataFullExport
    {
        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public void dataFullExport()
        {
            /**
          * In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
           */

            //include the Boxalino Client SDK php files
            //include the Boxalino Client SDK php files
            //path to the lib folder with the Boxalino Client SDK and PHP Thrift Client files
            //required parameters you should set for this example to work
            string account =string.IsNullOrEmpty(this.account)?"csharp_unittest": this.account; // your account name
            string password =string.IsNullOrEmpty(this.password)?"csharp_unittest":this.password; // your account password
            string domain = ""; // your web-site domain (e.g.: www.abc.com)
            string[] languages = new string[] { "en" }; //declare the list of available languages
            bool isDev = false; //are the data to be pushed dev or prod data?
            bool isDelta = false; //are the data to be pushed full data (reset index) or delta (add/modify index)?
            List<string> logs = new List<string> { }; //optional, just used here in example to collect logs
            bool print = this.print??true;

            //Create the Boxalino Data SDK instance
            BxData bxData = new BxData(new BxClient(account, password, domain), languages, isDev, isDelta);
            try
            {

                //Product export
                string file = HttpContext.Current.Server.MapPath("~/SampleData/products.csv"); //a csv file with header row
                string itemIdColumn = "id"; //the column header row name of the csv with the unique id of each item
                string colorFile = HttpContext.Current.Server.MapPath("~/SampleData/color.csv"); //a csv file with header row
                string colorIdColumn = "color_id"; //column header row name of the csv with the unique category id
                Dictionary<string, object> colorLabelColumns = new Dictionary<string, object>() { { "en", "value_en" } };  //column header row names of the csv with the category label in each language
                string productToColorsFile = HttpContext.Current.Server.MapPath("~/SampleData/product_color.csv"); //a csv file with header row


                //add a csv file as main product file
                string sourceKey = bxData.addMainCSVItemFile(file, itemIdColumn);
                bxData.addSourceStringField(sourceKey, "related_product_ids", "related_product_ids");
                bxData.addFieldParameter(sourceKey, "related_product_ids", "splitValues", ",");

                //declare the fields
                bxData.addSourceTitleField(sourceKey, new Dictionary<string, string>() { { "en", "name_en" } });
                bxData.addSourceDescriptionField(sourceKey, new Dictionary<string, string>() { { "en", "description_en" } });
                bxData.addSourceListPriceField(sourceKey, "list_price");
                bxData.addSourceDiscountedPriceField(sourceKey, "discounted_price");
                bxData.addSourceLocalizedTextField(sourceKey, "short_description", new Dictionary<string, string>() { { "en", "short_description_en" } });
                bxData.addSourceStringField(sourceKey, "sku", "sku");

                //add a csv file with products ids to Colors ids
                string productToColorsSourceKey = bxData.addCSVItemFile(productToColorsFile, itemIdColumn);

                //add a csv file with Colors
                string colorSourceKey = bxData.addResourceFile(colorFile, colorIdColumn, colorLabelColumns);
                bxData.addSourceLocalizedTextField(productToColorsSourceKey, "color", colorIdColumn, colorSourceKey);

                //Category export
                string categoryFile = HttpContext.Current.Server.MapPath("~/SampleData/categories.csv"); //a csv file with header row
                string categoryIdColumn = "category_id"; //column header row name of the csv with the unique category id
                string parentCategoryIdColumn = "parent_id"; //column header row name of the csv with the parent category id
                Dictionary<string, object> categoryLabelColumns = new Dictionary<string, object>() { { "en", "value_en" } }; //column header row names of the csv with the category label in each language
                string productToCategoriesFile = HttpContext.Current.Server.MapPath("~/SampleData/product_categories.csv"); //a csv file with header row

                //add a csv file with products ids to categories ids
                string productToCategoriesSourceKey = bxData.addCSVItemFile(productToCategoriesFile, itemIdColumn);

                //add a csv file with categories
                bxData.addCategoryFile(categoryFile, categoryIdColumn, parentCategoryIdColumn, categoryLabelColumns);
                bxData.setCategoryField(productToCategoriesSourceKey, categoryIdColumn);

                //Customer export
                string customerFile = HttpContext.Current.Server.MapPath("~/SampleData/customers.csv"); //a csv file with header row
                string customerIdColumn = "customer_id"; //the column header row name of the csv with the unique id of each item

                //add a csv file as main customer file
                string customerSourceKey = bxData.addMainCSVCustomerFile(customerFile, customerIdColumn);
                bxData.addSourceStringField(customerSourceKey, "country", "country");
                bxData.addSourceStringField(customerSourceKey, "zip", "zip");

                //Transaction export
                string transactionFile = HttpContext.Current.Server.MapPath("~/SampleData/transactions.csv"); //a csv file with header row, this file should contain one entry per product and per transaction (so the same transaction should appear several time if it contains more than 1 product
                string orderIdColumn = "order_id"; //the column header row name of the csv with the order (or transaction) id 
                string transactionProductIdColumn = "product_id"; //the column header row name of the csv with the product id
                string transactionCustomerIdColumn = "customer_id"; //the column header row name of the csv with the customer id
                string orderDateIdColumn = "order_date"; //the column header row name of the csv with the order date
                string totalOrderValueColumn = "total_order_value"; //the column header row name of the csv with the total order value
                string productListPriceColumn = "price"; //the column header row name of the csv with the product list price
                string productDiscountedPriceColumn = "discounted_price"; //the column header row name of the csv with the product price after discounts (real price paid)

                //optional fields, provided here with default values (so, no effect if not provided), matches the field to connect to the transaction product id and customer id columns (if the ids are not the same as the itemIdColumn of your products and customers files, then you can define another field)
                string transactionProductIdField = "bx_item_id"; //default value (can be left null) to define a specific field to map with the product id column
                string transactionCustomerIdField = "bx_customer_id"; //default value (can be left null) to define a specific field to map with the product id column


                //add a csv file as main customer file
                bxData.setCSVTransactionFile(transactionFile, orderIdColumn, transactionProductIdColumn, transactionCustomerIdColumn, orderDateIdColumn, totalOrderValueColumn, productListPriceColumn, productDiscountedPriceColumn, transactionProductIdField, transactionCustomerIdField);

                //prepare autocomplete index
                bxData.prepareCorpusIndex();
                List<string> fields = new List<string>() { "products_color" };
                bxData.prepareAutocompleteIndex(fields);

                logs.Add("publish the data specifications");
                bxData.pushDataSpecifications();

                logs.Add("publish the api owner changes"); //if the specifications have changed since the last time they were pushed
                bxData.publishChanges();

                logs.Add("push the data for data sync");
                bxData.pushData();

                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br/>", logs));
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
