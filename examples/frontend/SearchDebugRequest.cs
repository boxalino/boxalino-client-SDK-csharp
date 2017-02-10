﻿using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace examples.frontend
{
   public class SearchDebugRequest
    {

        public string account { get; set; }
        public string password { get; set; }
        public bool? print { get; set; }

        public BxClient bxClient = null;
        public void searchDebugRequest()
        {
            /**
          * In this example, we take a very simple CSV file with product data, generate the specifications, load them, publish them and push the data to Boxalino Data Intelligence
           */

        
            //path to the lib folder with the Boxalino Client SDK and C# Thrift Client files
            //required parameters you should set for this example to work
            string account = string.IsNullOrEmpty(this.account) ? "" : this.account; // your account name
            string password = string.IsNullOrEmpty(this.password) ? "" : this.password; // your account password
            string domain = ""; // your web-site domain (e.g.: www.abc.com)
            string[] languages = new string[] { "en" }; //declare the list of available languages
            bool isDev = false; //are the data to be pushed dev or prod data?
            bool isDelta = false; //are the data to be pushed full data (reset index) or delta (add/modify index)?
            List<string> logs = new List<string> { }; //optional, just used here in example to collect logs
            bool print = this.print ?? true;
            //Create the Boxalino Data SDK instance

            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
             bxClient = new BxClient(account, password, domain);

            try
            {
                string language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
                string queryText = "women"; // a search query
                int hitCount = 10; //a maximum number of search result to return in one page

                //create search request
                BxSearchRequest bxRequest = new BxSearchRequest(language, queryText, hitCount);

                //add the request
                bxClient.addRequest(bxRequest);

                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();

                //print the request object which is sent to our server (please provide it to Boxalino in all your support requests)
                if (print){                   
                    Print(bxClient.getThriftChoiceRequest());
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message);
            }
        }

        public void Print<T>(T x)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }

    }
}