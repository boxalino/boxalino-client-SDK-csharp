using boxalino_client_SDK_CSharp.Services;
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
    public class BxAutocompleteRequest
    {
        protected string language;
        protected string queryText;
        protected string choiceId;
        protected int textualSuggestionsHitCount;
        protected BxSearchRequest bxSearchRequest;      
        protected string indexId = null;

        /// <summary>
        /// Initializes a new instance of the <see cref="BxAutocompleteRequest"/> class.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="queryText">The query text.</param>
        /// <param name="textualSuggestionsHitCount">The textual suggestions hit count.</param>
        /// <param name="productSuggestionHitCount">The product suggestion hit count.</param>
        /// <param name="autocompleteChoiceId">The autocomplete choice identifier.</param>
        /// <param name="searchChoiceId">The search choice identifier.</param>
        public BxAutocompleteRequest(string language, string queryText, int textualSuggestionsHitCount, int productSuggestionHitCount = 5, string autocompleteChoiceId = "autocomplete", string searchChoiceId = "search")
        {
            this.language = language;
            this.queryText = queryText;
            this.textualSuggestionsHitCount = textualSuggestionsHitCount;
            if (autocompleteChoiceId == null)
            {
                autocompleteChoiceId = "autocomplete";
            }
            this.choiceId = autocompleteChoiceId;
            this.bxSearchRequest = new BxSearchRequest(language, queryText, productSuggestionHitCount, searchChoiceId);
        }
        /// <summary>
        /// Gets the bx search request.
        /// </summary>
        /// <returns></returns>
        public BxSearchRequest getBxSearchRequest()
        {
            return this.bxSearchRequest;
        }

        /// <summary>
        /// Sets the bx search request.
        /// </summary>
        /// <param name="bxSearchRequest">The bx search request.</param>
        public void setBxSearchRequest(BxSearchRequest bxSearchRequest)
        {
            this.bxSearchRequest = bxSearchRequest;
        }
        public string getLanguage()
        {
            return this.language;
        }

        /// <summary>
        /// Sets the language.
        /// </summary>
        /// <param name="language">The language.</param>
        public void setLanguage(string language)
        {
            this.language = language;
        }

        /// <summary>
        /// Gets the querytext.
        /// </summary>
        /// <returns></returns>
        public string getQuerytext()
        {
            return this.queryText;
        }

        /// <summary>
        /// Sets the querytext.
        /// </summary>
        /// <param name="queryText">The query text.</param>
        public void setQuerytext(string queryText)
        {
            this.queryText = queryText;
        }

        /// <summary>
        /// Gets the choice identifier.
        /// </summary>
        /// <returns></returns>
        public string getChoiceId()
        {
            return this.choiceId;
        }

        /// <summary>
        /// Sets the choice identifier.
        /// </summary>
        /// <param name="choiceId">The choice identifier.</param>
        public void setChoiceId(string choiceId)
        {
            this.choiceId = choiceId;
        }

        /// <summary>
        /// Gets the textual suggestion hit count.
        /// </summary>
        /// <returns></returns>
        public int getTextualSuggestionHitCount()
        {
            return this.textualSuggestionsHitCount;
        }

        /// <summary>
        /// Sets the textual suggestion hit count.
        /// </summary>
        /// <param name="textualSuggestionsHitCount">The textual suggestions hit count.</param>
        public void setTextualSuggestionHitCount(int textualSuggestionsHitCount)
        {
            this.textualSuggestionsHitCount = textualSuggestionsHitCount;
        }
        /// <summary>
        /// Gets the index identifier.
        /// </summary>
        /// <returns></returns>
        public string getIndexId()
        {
            return this.indexId;
        }

        /// <summary>
        /// Sets the index identifier.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        public void setIndexId(string indexId)
        {
            this.indexId = indexId;
        }

        /// <summary>
        /// Sets the default index identifier.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        public void setDefaultIndexId(string indexId)
        {
            if (this.indexId == null)
            {
                this.setIndexId(indexId);
            }
            this.bxSearchRequest.setDefaultIndexId(indexId);
        }

        /// <summary>
        /// Gets the autocomplete query.
        /// </summary>
        /// <returns></returns>
        private AutocompleteQuery getAutocompleteQuery()
        {
            AutocompleteQuery autocompleteQuery = new AutocompleteQuery();
            autocompleteQuery.IndexId = this.getIndexId();
            autocompleteQuery.Language = this.language;
            autocompleteQuery.QueryText = this.queryText;
            autocompleteQuery.SuggestionsHitCount = this.textualSuggestionsHitCount;
            autocompleteQuery.Highlight = true;
            autocompleteQuery.HighlightPre = "<em>";
            autocompleteQuery.HighlightPost = "</em>";
            return autocompleteQuery;
        }

        private List<PropertyQuery> propertyQueries = new List<PropertyQuery>();
        /// <summary>
        /// Adds the property query.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="hitCount">The hit count.</param>
        /// <param name="evaluateTotal">if set to <c>true</c> [evaluate total].</param>
        public void addPropertyQuery(string field, int hitCount, bool evaluateTotal = false)
        {
            PropertyQuery propertyQuery = new PropertyQuery();
            propertyQuery.name = field;
            propertyQuery.hitCount = hitCount;
            propertyQuery.evaluateTotal = evaluateTotal;
            this.propertyQueries.Add(propertyQuery);
        }

        /// <summary>
        /// Resets the property queries.
        /// </summary>
        public void resetPropertyQueries()
        {
            this.propertyQueries = new List<PropertyQuery>();
        }

        /// <summary>
        /// Gets the autocomplete thrift request.
        /// </summary>
        /// <param name="profileid">The profileid.</param>
        /// <param name="thriftUserRecord">The thrift user record.</param>
        /// <returns></returns>
        public AutocompleteRequest getAutocompleteThriftRequest(string profileid, UserRecord thriftUserRecord)
        {
            AutocompleteRequest autocompleteRequest = new AutocompleteRequest();
            autocompleteRequest.UserRecord = thriftUserRecord;
            autocompleteRequest.ProfileId = profileid;
            autocompleteRequest.ChoiceId = this.choiceId;
            autocompleteRequest.SearchQuery = this.bxSearchRequest.getSimpleSearchQuery();
            autocompleteRequest.SearchChoiceId = this.bxSearchRequest.getChoiceId();
            autocompleteRequest.AutocompleteQuery = this.getAutocompleteQuery();

            if (this.propertyQueries.Count > 0)
            {
                autocompleteRequest.PropertyQueries = this.propertyQueries;
            }

            return autocompleteRequest;
        }
    }
}
