using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.backend
{
    public class DataCustomers
    {
        public void dataCustomers()
        {

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


                //add a csv file as main product file
                bxData.addMainCSVItemFile(productFile, itemIdColumn);

                //add a csv file as main customer file
                string customerSourceKey = bxData.addMainCSVCustomerFile(customerFile, customerIdColumn);

                //this part is only necessary to do when you push your data in full, as no specifications changes should not be published without a full data sync following next
                //even when you publish your data in full, you don't need to repush your data specifications if you know they didn't change, however, it is totally fine (and suggested) to push them everytime if you are not sure if something changed or not

                if (!isDelta)
                {

                    bxData.addSourceStringField(customerSourceKey, "country", "country");
                    bxData.addSourceStringField(customerSourceKey, "zip", "zip");

                    logs.Add("publish the data specifications");
                    bxData.pushDataSpecifications();

                    logs.Add("publish the api owner changes"); //if the specifications have changed since the last time they were pushed
                    bxData.publishChanges();

                }

                logs.Add("push the data for data sync");
                if (print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br/>", logs));
                }
                bxData.pushData();
            }
            catch (Exception ex)
            {
                //be careful not to print the error message on your publish web-site as sensitive information like credentials might be indicated for debug purposes
	
                if (print){
                    HttpContext.Current.Response.Write(ex.Message);
                }
            }
        }
    }
}
