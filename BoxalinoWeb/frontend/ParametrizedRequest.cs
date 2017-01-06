using boxalino_client_SDK_CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace BoxalinoWeb.frontend
{
    public class ParametrizedRequest
    {
        string account { get; set; }
        string password { get; set; }
        string domain { get; set; }
        List<string> logs { get; set; }
        string language { get; set; }
        string choiceId { get; set; }
        int hitCount { get; set; }
        bool isDev { get; set; }

        string requestWeightedParametersPrefix { get; set; }
        string requestFiltersPrefix { get; set; }

        string requestFacetsPrefix { get; set; }

        string requestSortFieldPrefix { get; set; }

        string requestReturnFieldsName { get; set; }

        List<string> bxReturnFields { get; set; }

        string getItemFieldsCB { get; set; }

        bool print=true;
        public void parametrizedRequest()
        {

            //required parameters you should set for this example to work
            account = "csharp_unittest";
            password = "csharp_unittest";
            domain = ""; // your web-site domain (e.g.: www.abc.com)
            logs = new List<string>(); //optional, just used here in example to collect logs
            isDev = true;


            //Create the Boxalino Client SDK instance
            //N.B.: you should not create several instances of BxClient on the same page, make sure to save it in a static variable and to re-use it.
            BxClient bxClient = new BxClient(account, password, domain,isDev);
            bxClient.setRequestMap(new Dictionary<string, string>());
            try
            {
                language = "en"; // a valid language code (e.g.: "en", "fr", "de", "it", ...)
	            choiceId = "productfinder"; //the recommendation choice id (standard choice ids are: "similar" => similar products on product detail page, "complementary" => complementary products on product detail page, "basket" => cross-selling recommendations on basket page, "search"=>search results, "home" => home page personalized suggestions, "category" => category page suggestions, "navigation" => navigation product listing pages suggestions)
	            hitCount = 10; //a maximum number of recommended result to return in one page
	            requestWeightedParametersPrefix = "bxrpw_";
	            requestFiltersPrefix = "bxfi_";
	            requestFacetsPrefix = "bxfa_";
	            requestSortFieldPrefix = "bxsf_";
	            requestReturnFieldsName = "bxrf";

                bxReturnFields = new List<string>();
                bxReturnFields.Add("id");  //the list of fields which should be returned directly by Boxalino, the others will be retrieved through a call-back function
	            getItemFieldsCB = "getItemFieldsCB";

                //create the request and set the parameter prefix values
                BxParametrizedRequest bxRequest = new BxParametrizedRequest(language, choiceId, hitCount, 0, bxReturnFields, getItemFieldsCB); 
                bxRequest.setRequestWeightedParametersPrefix(requestWeightedParametersPrefix);
                bxRequest.setRequestFiltersPrefix(requestFiltersPrefix);
                bxRequest.setRequestFacetsPrefix(requestFacetsPrefix);
                bxRequest.setRequestSortFieldPrefix(requestSortFieldPrefix);

                bxRequest.setRequestReturnFieldsName(requestReturnFieldsName);

                //add the request
	            bxClient.addRequest(bxRequest);

             

                //make the query to Boxalino server and get back the response for all requests
             	BxChooseResponse bxResponse = bxClient.getResponse();

                logs.Add("<h3>weighted parameters</h3>");
                foreach (var  item  in    bxRequest.getWeightedParameters()) {
                    foreach (var  fieldItems in  item.Value) {
			          logs.Add( item.Key+": "+fieldItems.Key+": "+fieldItems.Value);
                    }
                }
                logs.Add("..");
                logs.Add("<h3>filters</h3>");
                foreach (var bxFilter in  bxRequest.getFilters()) {
	            	logs.Add(((BxFilter)bxFilter.Value).getFieldName()+ ": "+ string.Join(",", bxFilter.Value.getValues())+ " :"+ bxFilter.Value.isNegative());
                }
                logs.Add("..");

                logs.Add("<h3>facets</h3>");
	            BxFacets  bxFacets = bxRequest.getFacets();
                foreach (var fieldName in bxFacets.getFieldNames()) {
		            logs.Add(fieldName+": "+ string.Join(",", bxFacets.getSelectedValues(fieldName)));
                }
                logs.Add("..");

                logs.Add("<h3>sort fields</h3>");
	            BxSortFields bxSortFields = bxRequest.getSortFields();
                foreach (var fieldName in bxSortFields.getSortFields() ) {
		            logs.Add( fieldName+": "+ bxSortFields.isFieldReverse(fieldName.Value.ToString()));
                }

                logs.Add("..");                	
	            logs.Add("<h3>results</h3>");
                logs.Add(bxResponse.toJson(bxRequest.getAllReturnFields().ToArray()));

                if (print){
                    HttpContext.Current.Response.Write( string.Join("<br>",logs));
                }
            }
            catch(Exception ex)
            {
                //be careful not to print the error message on your publish web-site as sensitive information like credentials might be indicated for debug purposes

                if (print){
                    HttpContext.Current.Response.Write(ex.Message.ToString());
                }
            }
        }

        NestedDictionary<string, object> fgetItemFieldsCB(string[]  ids, string[] fieldNames)
        {
           
            NestedDictionary<string, object> values = new NestedDictionary<string, object>();
            foreach (var id in  ids) {
                values[id] = new NestedDictionary<string, object>();
                foreach (var fieldName in fieldNames) {
                    values[id][fieldName] = new NestedDictionary<string, object>();
                    values[id][fieldName].Value = fieldName + "-value";                   
                }
            }
            return values;
        }

        public void Print<T>(T x)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(x, Newtonsoft.Json.Formatting.Indented);
            HttpContext.Current.Response.Write("<pre>" + json + "</pre>");
        }
    }
}
