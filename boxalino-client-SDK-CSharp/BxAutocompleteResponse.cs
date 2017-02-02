using boxalino_client_SDK_CSharp.Exception;
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
    public class BxAutocompleteResponse
    {
        private AutocompleteResponse response;
        private BxAutocompleteRequest bxAutocompleteRequest;
        /// <summary>
        /// Initializes a new instance of the <see cref="BxAutocompleteResponse"/> class.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="bxAutocompleteRequest">The bx autocomplete request.</param>
        public BxAutocompleteResponse(AutocompleteResponse response, BxAutocompleteRequest bxAutocompleteRequest = null)
        {
            this.response = response;
            this.bxAutocompleteRequest = bxAutocompleteRequest;
        }

        /// <summary>
        /// Gets the response.
        /// </summary>
        /// <returns></returns>
        public AutocompleteResponse getResponse()
        {
            return this.response;
        }
        /// <summary>
        /// Gets the prefix search hash.
        /// </summary>
        /// <returns></returns>
        public string getPrefixSearchHash()
        {
            if (this.getResponse().PrefixSearchResult.TotalHitCount > 0)
            {
                return Common.PHPMd5Hash(this.getResponse().PrefixSearchResult.QueryText).Substring(0, 10);
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Gets the textual suggestions.
        /// </summary>
        /// <returns></returns>
        public List<string> getTextualSuggestions()
        {
            List<string> suggestions = new List<string>();

            foreach (AutocompleteHit hit in this.getResponse().Hits)
            {
                suggestions.Add(hit.Suggestion);
            }
            return suggestions;
        }

        /// <summary>
        /// Gets the textual suggestion hit.
        /// </summary>
        /// <param name="suggestion">The suggestion.</param>
        /// <returns></returns>
        /// <exception cref="BoxalinoException">unexisting textual suggestion provided " + suggestion</exception>
        protected AutocompleteHit getTextualSuggestionHit(string suggestion)
        {
            foreach (AutocompleteHit hit in this.getResponse().Hits)
            {
                if (hit.Suggestion == suggestion)
                {
                    return hit;
                }
            }
            throw new BoxalinoException("unexisting textual suggestion provided " + suggestion);
        }

        /// <summary>
        /// Gets the textual suggestion total hit count.
        /// </summary>
        /// <param name="suggestion">The suggestion.</param>
        /// <returns></returns>
        public long getTextualSuggestionTotalHitCount(string suggestion)
        {
            AutocompleteHit hit = this.getTextualSuggestionHit(suggestion);
            return hit.SearchResult.TotalHitCount;
        }

        /// <summary>
        /// Gets the bx search response.
        /// </summary>
        /// <param name="textualSuggestion">The textual suggestion.</param>
        /// <returns></returns>
        public BxChooseResponse getBxSearchResponse(string textualSuggestion = null)
        {
            SearchResult searchResult = textualSuggestion == null ? this.getResponse().PrefixSearchResult : this.getTextualSuggestionHit(textualSuggestion).SearchResult;
            return new BxChooseResponse(searchResult,new List<BxSearchRequest>() { this.bxAutocompleteRequest.getBxSearchRequest() });
        }

        /// <summary>
        /// Gets the property hits.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public List<PropertyHit> getPropertyHits(string field)
        {
            
            foreach (PropertyResult propertyResult in this.getResponse().PropertyResults)
            {
                if (propertyResult.Name == field)
                {
                    return (List<PropertyHit>)propertyResult.Hits;
                }
            }
            return new List<PropertyHit>();
        }

        /// <summary>
        /// Gets the property hit.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="hitValue">The hit value.</param>
        /// <returns></returns>
        public PropertyHit getPropertyHit(string field, string hitValue)
        {
            foreach (PropertyHit hit in this.getPropertyHits(field))
            {
                if (hit.Value == hitValue)
                {
                    return hit;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the property hit values.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns></returns>
        public List<String> getPropertyHitValues(string field)
        {
            List<String> hitValues = new List<String>();
            foreach (PropertyHit hit in this.getPropertyHits(field))
            {
                hitValues.Add(hit.Value);
            }
            return hitValues;
        }

        /// <summary>
        /// Gets the property hit value label.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="hitValue">The hit value.</param>
        /// <returns></returns>
        public string getPropertyHitValueLabel(string field, string hitValue)
        {
            PropertyHit hit = this.getPropertyHit(field, hitValue);
            if (hit != null)
            {
                return hit.Label;
            }
            return null;
        }

        /// <summary>
        /// Gets the property hit value total hit count.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="hitValue">The hit value.</param>
        /// <returns></returns>
        public long getPropertyHitValueTotalHitCount(string field, string hitValue)
        {
            PropertyHit hit = this.getPropertyHit(field, hitValue);
            if (hit != null)
            {
                return hit.TotalHitCount;
            }
            return 0;
        }
    }
}
