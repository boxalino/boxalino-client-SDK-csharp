using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.backend
{
    class DataTransactions
    {

        public void dataTransactions()
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
                string productFile = HttpContext.Current.Server.MapPath("~/SampleData/products.csv"); //a csv file with header row
                string itemIdColumn = "id"; //the column header row name of the csv with the unique id of each item

                string customerFile = HttpContext.Current.Server.MapPath("~/SampleData/customers.csv"); //a csv file with header row
                string customerIdColumn = "customer_id"; //the column header row name of the csv with the unique id of each item


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

                //add a csv file as main product file
                bxData.addMainCSVItemFile(productFile, itemIdColumn);

                //add a csv file as main customer file
                bxData.addMainCSVCustomerFile(customerFile, customerIdColumn);

                //add a csv file as main customer file
                bxData.setCSVTransactionFile(transactionFile, orderIdColumn, transactionProductIdColumn, transactionCustomerIdColumn, orderDateIdColumn, totalOrderValueColumn, productListPriceColumn, productDiscountedPriceColumn, transactionProductIdField, transactionCustomerIdField);

                //this part is only necessary to do when you push your data in full, as no specifications changes should not be published without a full data sync following next
                //even when you publish your data in full, you don't need to repush your data specifications if you know they didn't change, however, it is totally fine (and suggested) to push them everytime if you are not sure if something changed or not
                if (!isDelta)
                {

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
