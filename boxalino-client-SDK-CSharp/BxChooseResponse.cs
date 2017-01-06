using boxalino_client_SDK_CSharp.Exception;
using boxalino_client_SDK_CSharp.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace boxalino_client_SDK_CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class BxChooseResponse
    {
        private dynamic response;
        private dynamic bxRequests;
        /// <summary>
        /// Initializes a new instance of the <see cref="BxChooseResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="bxRequests">The bx requests.</param>
        public BxChooseResponse(ChoiceResponse response, List<BxRequest> bxRequests)
        {
            this.response = response;
            if (bxRequests == null)
            {
                bxRequests = new List<BxRequest>();
            }
            this.bxRequests = bxRequests;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BxChooseResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="bxRequests">The bx requests.</param>
        public BxChooseResponse(SearchResult response, List<BxSearchRequest> bxRequests)
        {
            this.response = response;
            if (bxRequests == null)
            {
                bxRequests = new List<BxSearchRequest>();
            }
            this.bxRequests = bxRequests;
        }


        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns></returns>
        public dynamic getResponse()
        {
            return this.response;
        }

        /// <summary>
        /// Gets the choice identifier response variant.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">no variant provided in choice response for variant id " + id</exception>
        protected Variant getChoiceIdResponseVariant(int id = 0)
        {
            dynamic response = this.getResponse();
            if (response.GetType().GetProperty("Variants") != null)
            {
                if (response.__isset.variants)
                {
                    return response.Variants[id - 1];
                }
            }
          
            if (response.GetType().Name == (new SearchResult().GetType().Name))
            {
                Variant variant = new Variant();
                variant.SearchResult = response;
                return variant;
            }
            throw new BoxalinoException("no variant provided in choice response for variant id " + id);
        }


        /// <summary>
        /// Gets the choice response variant.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public Variant getChoiceResponseVariant(string choice = null, int count = 0)
        {
            int k = 0;
            foreach (BxRequest bxRequest in this.bxRequests)
            {
                k++;
                if (choice == null || choice == bxRequest.getChoiceId())
                {
                    if (count > 0)
                    {
                        count--;
                        continue;
                    }
                    return this.getChoiceIdResponseVariant(k);
                }
            }
            return null;
        }
        /// <summary>
        /// Retrieves the hit field values.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="field">The field.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="hits">The hits.</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> retrieveHitFieldValues(Hit item, string field, List<Hit> fields, string[] hits)
        {
            List<Dictionary<string, object>> fieldValues = new List<Dictionary<string, object>>();
            foreach (var bxRequest in this.bxRequests)
            {
                fieldValues.AddRange(bxRequest.retrieveHitFieldValues(item, field, fields, hits));
            }
            return fieldValues;
        }
        /// <summary>
        /// Gets the request facets.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <returns></returns>
        protected BxFacets getRequestFacets(string choice = null)
        {
            if (choice == null)
            {
                if (this.bxRequests[0] != null)
                {
                    return this.bxRequests[0].getFacets();
                }
                return null;
            }
            foreach (var bxRequest in this.bxRequests)
            {
                if (bxRequest.getChoiceId() == choice)
                {
                    return bxRequest.getFacets();
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the facets.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public BxFacets getFacets(string choice = null, bool considerRelaxation = true, int count = 0, int maxDistance = 10)
        {
            Variant variant = this.getChoiceResponseVariant(choice, count);
            SearchResult searchResult = this.getVariantSearchResult(variant, considerRelaxation, maxDistance);
            BxFacets facets = this.getRequestFacets(choice);
            if (facets == null || searchResult == null)
            {
                return null;
            }
            facets.setFacetResponse(searchResult.FacetResponses);
            return facets;
        }

        /// <summary>
        /// Gets the search hit field values.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public NestedDictionary<string, object> getSearchHitFieldValues(SearchResult searchResult, string[] fields = null)
        {
            NestedDictionary<string, object> fieldValues = new NestedDictionary<string, object>();

            if (searchResult != null)
            {
                foreach (Hit item in searchResult.Hits)
                {
                    string[] finalFields = fields;
                    if (finalFields == null)
                    {
                        finalFields = Common.array_keys(item.Values);
                    }

                    foreach (string field in finalFields)
                    {
                        if (item.Values[field] != null)
                        {
                            if (!string.IsNullOrEmpty(item.Values[field].FirstOrDefault() ?? string.Empty))
                            {

                                fieldValues[item.Values["id"][0]][field].Value = item.Values[field].FirstOrDefault().ToString();

                            }
                        }
                        if (fieldValues[item.Values["id"][0]][field].Value == null)
                        {
                            fieldValues[item.Values["id"][0]][field].Value = this.retrieveHitFieldValues(item, field, searchResult.Hits, finalFields);
                        }


                    }

                }
            }
            return fieldValues;
        }


        /// <summary>
        /// Gets the hit field values.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public NestedDictionary<string, object> getHitFieldValues(string[] fields, string choice = null, bool considerRelaxation = true, int count = 0, int maxDistance = 10)
        {
            Variant variant = this.getChoiceResponseVariant(choice, count);
            return this.getSearchHitFieldValues(this.getVariantSearchResult(variant, considerRelaxation, maxDistance), fields);
        }

        /// <summary>
        /// Gets the first hit field value.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="returnOneValue">if set to <c>true</c> [return one value].</param>
        /// <param name="hitIndex">Index of the hit.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public object getFirstHitFieldValue(string field = null, bool returnOneValue = true, int hitIndex = 0, string choice = null, int count = 0, int maxDistance = 10)
        {
            string[] fieldNames = null;
            if (field != null)
            {
                fieldNames = new string[] { field };
            }
            count = 0;
            foreach (var fieldValueMap in this.getHitFieldValues(fieldNames, choice, true, count, maxDistance))
            {
                Dictionary<string, List<string>> temp_fieldValueMap = new Dictionary<string, List<string>>();
                if (count++ < hitIndex)
                {
                    continue;
                }
                foreach (var fieldValues in temp_fieldValueMap.Values)
                {
                    List<string> temp_fieldValues = new List<string>();
                    try
                    {
                        temp_fieldValues = fieldValues;
                    }
                    catch { }
                    if (temp_fieldValues.Count > 0)
                    {
                        if (returnOneValue)
                        {
                            return temp_fieldValues[0];
                        }
                        else
                        {
                            return temp_fieldValues;
                        }
                    }
                }
            }
            return null;

        }
        /// <summary>
        /// Gets the variant search result.
        /// </summary>
        /// <param name="variant">The variant.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public SearchResult getVariantSearchResult(Variant variant, bool considerRelaxation = true, int maxDistance = 10)
        {

            SearchResult searchResult = variant.SearchResult;
            if (considerRelaxation && variant.SearchResult.TotalHitCount == 0)
            {
                return this.getFirstPositiveSuggestionSearchResult(variant, maxDistance);
            }
            return searchResult;
        }
        /// <summary>
        /// Gets the first positive suggestion search result.
        /// </summary>
        /// <param name="variant">The variant.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        protected SearchResult getFirstPositiveSuggestionSearchResult(Variant variant, int maxDistance = 10)
        {
            if (variant.SearchRelaxation.SuggestionsResults == null)
            {
                return null;
            }
            foreach (SearchResult searchResult in variant.SearchRelaxation.SuggestionsResults)
            {
                if (searchResult.TotalHitCount > 0)
                {
                    if (searchResult.QueryText == "" || variant.SearchResult.QueryText == "")
                    {
                        continue;
                    }
                    int distance = Common.LevenshteinDistance(searchResult.QueryText, variant.SearchResult.QueryText);
                    if (distance <= maxDistance && distance != -1)
                    {
                        return searchResult;
                    }
                }
            }
            return null;
        }


        /// <summary>
        /// Ares the there sub phrases.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public bool areThereSubPhrases(string choice = null, int count = 0)
        {
            Variant variant = getChoiceResponseVariant(choice, count);
            return (variant.SearchRelaxation.SubphrasesResults) != null ? true : false && (variant.SearchRelaxation.SubphrasesResults).Count > 0;
        }


        /// <summary>
        /// Gets the sub phrases queries.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public Dictionary<string, SearchResult> getSubPhrasesQueries(string choice = null, int count = 0)
        {

            if (!this.areThereSubPhrases(choice, count))
            {
                return new Dictionary<string, SearchResult>();
            }

            Dictionary<string, SearchResult> queries = new Dictionary<string, SearchResult>();
            Variant variant = this.getChoiceResponseVariant(choice, count);
            foreach (var searchResult in variant.SearchRelaxation.SubphrasesResults.Select((value, index) => new { value, index }))
            {
                queries.Add(searchResult.index.ToString(), searchResult.value);
            }

            return queries;
        }

        /// <summary>
        /// Gets the sub phrase total hit count.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public long getSubPhraseTotalHitCount(string queryText, string choice = null, int count = 0)
        {

            SearchResult searchResult = this.getSubPhraseSearchResult(queryText, choice, count);
            if (searchResult != null)
            {
                return searchResult.TotalHitCount;
            }
            return 0;
        }

        /// <summary>
        /// Gets the sub phrase search result.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        protected SearchResult getSubPhraseSearchResult(string queryText, string choice = null, int count = 0)
        {
            if (!this.areThereSubPhrases(choice, count))
            {
                return null;
            }
            Variant variant = this.getChoiceResponseVariant(choice, count);
            foreach (var searchResult in variant.SearchRelaxation.SubphrasesResults)
            {
                if (searchResult.QueryText == queryText)
                {
                    return searchResult;
                }
            }

            return null;
        }




        /// <summary>
        /// Gets the search result hit ids.
        /// </summary>
        /// <param name="searchResult">The search result.</param>
        /// <param name="fieldId">The field identifier.</param>
        /// <returns></returns>
        public Dictionary<string, string> getSearchResultHitIds(SearchResult searchResult, string fieldId = "id")
        {
            Dictionary<string, string> ids = new Dictionary<string, string>();

            if (searchResult != null)
            {
                if (searchResult.Hits.Count > 0)
                {
                    foreach (var item in searchResult.Hits.Select((value, index) => new { value, index }))
                    {
                        ids.Add(item.index.ToString(), item.value.Values[fieldId][0]);
                    }
                }
                else if ((searchResult.HitsGroups).Count > 0)
                {
                    foreach (var hitGroup in searchResult.HitsGroups.Select((value, index) => new { value, index }))
                    {
                        ids.Add(hitGroup.index.ToString(), hitGroup.value.GroupValue);
                    }
                }
            }
            return ids;
        }
        /// <summary>
        /// Gets the sub phrase hit ids.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <param name="fieldId">The field identifier.</param>
        /// <returns></returns>
        public Dictionary<string, string> getSubPhraseHitIds(string queryText, string choice = null, int count = 0, string fieldId = "id")
        {
            SearchResult searchResult = this.getSubPhraseSearchResult(queryText, choice, count);
            if (searchResult != null)
            {
                return this.getSearchResultHitIds(searchResult, fieldId);
            }
            return new Dictionary<string, string>();
        }

        /// <summary>
        /// Gets the hit ids.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <param name="fieldId">The field identifier.</param>
        /// <returns></returns>
        public Dictionary<string, string> getHitIds(string choice = null, bool considerRelaxation = true, int count = 0, int maxDistance = 10, string fieldId = "id")
        {
            Variant variant = this.getChoiceResponseVariant(choice, count);
            return this.getSearchResultHitIds(this.getVariantSearchResult(variant, considerRelaxation, maxDistance), fieldId);
        }

        /// <summary>
        /// Gets the total hit count.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public long getTotalHitCount(string choice = null, bool considerRelaxation = true, int count = 0, int maxDistance = 10)
        {
            Variant variant = this.getChoiceResponseVariant(choice, count);
            SearchResult searchResult = this.getVariantSearchResult(variant, considerRelaxation, maxDistance);
            if (searchResult == null)
            {
                return 0;
            }
            return searchResult.TotalHitCount;
        }

        /// <summary>
        /// Gets the sub phrase hit field values.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        /// <param name="fields">The fields.</param>
        /// <param name="choice">The choice.</param>
        /// <param name="considerRelaxation">if set to <c>true</c> [consider relaxation].</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public NestedDictionary<string, object> getSubPhraseHitFieldValues(string queryText, string[] fields, string choice = null, bool considerRelaxation = true, int count = 0)
        {
            SearchResult searchResult = this.getSubPhraseSearchResult(queryText, choice, count);
            if (searchResult != null)
            {
                return this.getSearchHitFieldValues(searchResult, fields);
            }
            return new NestedDictionary<string, object>();
        }

        /// <summary>
        /// Ares the results corrected.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <param name="maxDistance">The maximum distance.</param>
        /// <returns></returns>
        public bool areResultsCorrected(string choice = null, int count = 0, int maxDistance = 10)
        {
            return this.getTotalHitCount(choice, false, count) == 0 && this.getTotalHitCount(choice, true, count, maxDistance) > 0 && this.areThereSubPhrases() == false;
        }

        /// <summary>
        /// Gets the corrected query.
        /// </summary>
        /// <param name="choice">The choice.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
        public string getCorrectedQuery(string choice = null, int count = 0)
        {
            Variant variant = this.getChoiceResponseVariant(choice, count);
            SearchResult searchResult = this.getVariantSearchResult(variant);
            if (searchResult != null)
            {
                return searchResult.QueryText;
            }
            return null;
        }

        /// <summary>
        /// To the json.
        /// </summary>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public string toJson(string[] fields)
        {
            List<Dictionary<string, object>> temp = new List<Dictionary<string, object>>();
            Dictionary<string, object> myObject = new Dictionary<string, object>();           
            foreach (var fieldValueMap in this.getHitFieldValues(fields))
            {
                Dictionary<string, object> hitFieldValues = new Dictionary<string, object>();
                foreach (var fieldValues in (NestedDictionary<string, object>)fieldValueMap.Value)
                {
                    hitFieldValues[fieldValues.Key] = new Dictionary<string, object>()
                {
                    {"values",fieldValues.Value.Value}
                };
                }

                temp.Add(new Dictionary<string, object>(){{"id", fieldValueMap.Key},{"fieldValues", hitFieldValues}});
            
            }
            myObject.Add("hits", temp);
            return JsonConvert.SerializeObject(myObject);
          

        }
    }
}
