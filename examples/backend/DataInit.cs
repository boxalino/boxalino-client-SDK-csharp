using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.backend
{
    class DataInit
    {
        public void dataInit()
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
                /**
	* Publish choices
	*/
                //your choie configuration can be generated in 3 possible ways: dev (using dev data), prod (using prod data as on your live web-site), prod-test (using prod data but not affecting your live web-site)
                bool isTest = false;
                string temp_isDev = isDev == false ? "" : "True";
                logs.Add("force the publish of your choices configuration: it does it either for dev or prod (above " + temp_isDev + " parameter) and, if isDev is false, you can do it in prod or prod-test<br>");
                bxData.publishChoices(isTest);

                /**
	* Prepare corpus index
	*/
                logs.Add("force the preparation of a corpus index based on all the terms of the last data you sent ==> you need to have published your data before and you will need to publish them again that the corpus is sent to the index<br>");
                bxData.prepareCorpusIndex();

                /**
	* Prepare autocomplete index
	*/
                //NOT YET READY NOTICE: prepareAutocompleteIndex doesn't add the fields yet even if you pass them to the function like in this example here (TODO), for now, you need to go in the data intelligence admin and set the fields manually. You can contact support@boxalino.com to do that.
                //the autocomplete index is automatically filled with common searches done over time, but of course, before going live, you will not have any. While it is possible to load pre-existing search logs (contact support@boxalino.com to learn how, you can also define some fields which will be considered for the autocompletion anyway (e.g.: brand, product line, etc.).
                List<string> fields = new List<string>() { "products_color" };
                logs.Add("force the preparation of an autocompletion index based on all the terms of the last data you sent ==> you need to have published your data before and you will need to publish them again that the corpus is sent to the index<br>");
                bxData.prepareAutocompleteIndex(fields);

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
