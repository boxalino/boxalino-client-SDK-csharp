using boxalino_client_SDK_CSharp;
using boxalino_client_SDK_CSharp.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

/// <summary>
/// 
/// </summary>
namespace BoxalinoWeb.frontend
{
    /// <summary>
    /// 
    /// </summary>
    public class SearchSubPhrases
    {
        #region declaration
        string account  { get; set; }
        string password { get; set; }
        string domain   { get; set; }
        string language { get; set; }
        List<string> logs   { get; set; }
        string queryText { get; set; }
        int hitCount { get; set; }        
        
        #endregion


        #region searchSubPhrase
        /// <summary>
        /// Searches the sub phrase.
        /// </summary>
        public void searchSubPhrase()
        {
            try
            {
                account  = "csharp_unittest";
                password = "csharp_unittest";
                domain   = "";// your web-site domain (e.g.: www.abc.com)
                language = "en";// a valid language code (e.g.: "en", "fr", "de", "it", ...)
                queryText = "women pack";// a search query
                hitCount = 10;//a maximum number of search result to return in one page
                logs = new List<string>();//optional, just used here in example to collect logs
                bool print = true;

                //Create the Boxalino Client SDK instance
                //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
                BxClient bxClient = new BxClient(account, password, domain);





                //create search request
                BxSearchRequest bxrequest = new BxSearchRequest(language, queryText, hitCount);
                //add the request
                bxClient.addRequest(bxrequest);
                //make the query to Boxalino server and get back the response for all requests
                BxChooseResponse bxResponse = bxClient.getResponse();



                //check if the system has generated sub phrases results

                if(bxResponse.areThereSubPhrases())
                {
                    logs.Add("No results found for all words in "+queryText+",but following partial matches were found:<br\\>");


                    foreach(var subPhrase in bxResponse.getSubPhrasesQueries())
                    {
                        logs.Add("Results for \"" + subPhrase.Value.QueryText + "\" (" + bxResponse.getSubPhraseTotalHitCount(subPhrase.Value.QueryText) + " hits):");
                        //loop on the search response hit ids and print them
                      
                        foreach(var id in bxResponse.getSubPhraseHitIds(subPhrase.Value.QueryText))
                        {
                            logs.Add(id.Key.ToString()+": returned id " +id.Value);
                            
                        }
                        	logs.Add("");
                    }
                }
                else
                {
                    
                    foreach(var item in bxResponse.getHitIds())
                    {
                        logs.Add(item.Key + "i: returned id " + item.Value);
                       
                    }
                }

                if(print)
                {
                    HttpContext.Current.Response.Write(string.Join("<br/>", logs));
                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write(ex.Message.ToString());
            }
        }
        #endregion
    }
}
