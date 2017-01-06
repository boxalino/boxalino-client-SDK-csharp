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
    /// <seealso cref="boxalino_client_SDK_CSharp.BxRequest" />
    public class BxParametrizedRequest : BxRequest
    {
        public List<string> bxReturnFields = new List<string>() { "id" };
        public string getItemFieldsCB = null;

        public string requestParametersPrefix = "";
        public string requestWeightedParametersPrefix = "bxrpw_";
        public string requestFiltersPrefix = "bxfi_";
        public string requestFacetsPrefix = "bxfa_";
        public string requestSortFieldPrefix = "bxsf_";

        public string requestReturnFieldsName = "bxrf";

        /// <summary>
        /// Initializes a new instance of the <see cref="BxParametrizedRequest"/> class.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="choiceId">The choice identifier.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="min">The minimum.</param>
        /// <param name="bxReturnFields">The bx return fields.</param>
        /// <param name="getItemFieldsCB">The get item fields cb.</param>
        public BxParametrizedRequest(string language, string choiceId, int max = 10, int min = 0, List<string> bxReturnFields = null, string getItemFieldsCB = null)
            : base(language, choiceId, max, min)
        {
            if (bxReturnFields != null)
            {
                this.bxReturnFields = bxReturnFields;
            }
            this.getItemFieldsCB = getItemFieldsCB;
        }

        /// <summary>
        /// Sets the request parameters prefix.
        /// </summary>
        /// <param name="requestParametersPrefix">The request parameters prefix.</param>
        public void setRequestParametersPrefix(string requestParametersPrefix)
        {
            this.requestParametersPrefix = requestParametersPrefix;
        }

        /// <summary>
        /// Gets the request parameters prefix.
        /// </summary>
        /// <returns></returns>
        public string getRequestParametersPrefix()
        {
            return this.requestParametersPrefix;
        }

        /// <summary>
        /// Sets the request weighted parameters prefix.
        /// </summary>
        /// <param name="requestWeightedParametersPrefix">The request weighted parameters prefix.</param>
        public void setRequestWeightedParametersPrefix(string requestWeightedParametersPrefix)
        {
            this.requestWeightedParametersPrefix = requestWeightedParametersPrefix;
        }

        /// <summary>
        /// Gets the request weighted parameters prefix.
        /// </summary>
        /// <returns></returns>
        public string getRequestWeightedParametersPrefix()
        {
            return this.requestWeightedParametersPrefix;
        }

        /// <summary>
        /// Sets the request filters prefix.
        /// </summary>
        /// <param name="requestFiltersPrefix">The request filters prefix.</param>
        public void setRequestFiltersPrefix(string requestFiltersPrefix)
        {
            this.requestFiltersPrefix = requestFiltersPrefix;
        }

        /// <summary>
        /// Gets the request filters prefix.
        /// </summary>
        /// <returns></returns>
        public string getRequestFiltersPrefix()
        {
            return this.requestFiltersPrefix;
        }

        /// <summary>
        /// Sets the request facets prefix.
        /// </summary>
        /// <param name="requestFacetsPrefix">The request facets prefix.</param>
        public void setRequestFacetsPrefix(string requestFacetsPrefix)
        {
            this.requestFacetsPrefix = requestFacetsPrefix;
        }

        /// <summary>
        /// Gets the request facets prefix.
        /// </summary>
        /// <returns></returns>
        public string getRequestFacetsPrefix()
        {
            return this.requestFacetsPrefix;
        }

        /// <summary>
        /// Sets the request sort field prefix.
        /// </summary>
        /// <param name="requestSortFieldPrefix">The request sort field prefix.</param>
        public void setRequestSortFieldPrefix(string requestSortFieldPrefix)
        {
            this.requestSortFieldPrefix = requestSortFieldPrefix;
        }

        /// <summary>
        /// Gets the request sort field prefix.
        /// </summary>
        /// <returns></returns>
        public string getRequestSortFieldPrefix()
        {
            return this.requestSortFieldPrefix;
        }

        /// <summary>
        /// Sets the name of the request return fields.
        /// </summary>
        /// <param name="requestReturnFieldsName">Name of the request return fields.</param>
        public void setRequestReturnFieldsName(string requestReturnFieldsName)
        {
            this.requestReturnFieldsName = requestReturnFieldsName;
        }

        /// <summary>
        /// Gets the name of the request return fields.
        /// </summary>
        /// <returns></returns>
        public string getRequestReturnFieldsName()
        {
            return this.requestReturnFieldsName;
        }

        /// <summary>
        /// Gets the prefixes.
        /// </summary>
        /// <returns></returns>
        public List<string> getPrefixes()
        {
            return new List<string>() { this.requestParametersPrefix, this.requestWeightedParametersPrefix, this.requestFiltersPrefix, this.requestFacetsPrefix, this.requestSortFieldPrefix };
        }

        /// <summary>
        /// Matcheses the prefix.
        /// </summary>
        /// <param name="_string">The string.</param>
        /// <param name="prefix">The prefix.</param>
        /// <param name="checkOtherPrefixes">if set to <c>true</c> [check other prefixes].</param>
        /// <returns></returns>
        public bool matchesPrefix(string _string, string prefix, bool checkOtherPrefixes = true)
        {
            if (checkOtherPrefixes)
            {
                foreach (var p in this.getPrefixes())
                {
                    if (p == prefix)
                    {
                        continue;
                    }
                    if (prefix.Length < p.Length && _string.IndexOf(p) == 0)
                    {
                        return false;
                    }
                }
            }
            return prefix == null || _string.IndexOf(prefix) == 0;
        }

        /// <summary>
        /// Gets the prefixed parameters.
        /// </summary>
        /// <param name="prefix">The prefix.</param>
        /// <param name="checkOtherPrefixes">if set to <c>true</c> [check other prefixes].</param>
        /// <returns></returns>
        public Dictionary<string, object> getPrefixedParameters(string prefix, bool checkOtherPrefixes = true)
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();
            foreach (var k in this.requestMap)
            {
                if (this.matchesPrefix(k.Key, prefix, checkOtherPrefixes))
                {
                    _params[k.Key.Substring(prefix.Length)] = k.Value;
                }
            }
            return _params;
        }

        /// <summary>
        /// Gets the request context parameters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, object> getRequestContextParameters()
        {
            Dictionary<string, object> _params = new Dictionary<string, object>();          
            foreach (dynamic item in getPrefixedParameters(requestWeightedParametersPrefix))
            {
                _params[item.name] = item.values;
            }
            foreach (dynamic item in getPrefixedParameters(requestParametersPrefix, false))
            {
                if (Common.strpos(item.name, requestWeightedParametersPrefix) != false)
                {
                    continue;
                }
                if (Common.strpos(item.name, requestFiltersPrefix) != false)
                {
                    continue;
                }
                if (Common.strpos(item.name, requestFacetsPrefix) != false)
                {
                    continue;
                }
                if (Common.strpos(item.name, requestSortFieldPrefix) != false)
                {
                    continue;
                }
                if (item.name == requestReturnFieldsName)
                {
                    continue;
                }
                _params[item.name] = item.values;
            }



            return _params;
        }

        /// <summary>
        /// Gets the weighted parameters.
        /// </summary>
        /// <returns></returns>
        public NestedDictionary<string, object> getWeightedParameters()
        {
            NestedDictionary<string, object> _params = new NestedDictionary<string, object>();

            foreach (var obj in this.getPrefixedParameters(this.requestWeightedParametersPrefix))
            {
                String[] pieces = obj.Key.Split(' ');
                string fieldValue = "";
                if (pieces.Length > 0)
                {
                    fieldValue = pieces[pieces.Length - 1];
                    pieces[pieces.Length - 1] = null;
                }
                string fieldName = String.Join(" ", pieces);
                if (_params[fieldName] != null)
                {
                    _params[fieldName] = new NestedDictionary<string, object>();
                }
                _params[fieldName] = new NestedDictionary<string, object>();


                _params[fieldName][fieldValue].Value = obj.Value;

            }
            return _params;
        }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BxFilter> getFilters()
        {
            Dictionary<string, BxFilter> filters = base.getFilters();
            foreach (var obj in this.getPrefixedParameters(this.requestFiltersPrefix))
            {
                bool negative = false;
                string value = "";
                if (Convert.ToString(obj.Value).IndexOf('!') == 0)
                {
                    negative = true;
                    value = Convert.ToString(obj.Value).Substring(1);
                }
                filters[""] = new BxFilter(obj.Key, new List<string>() { value }, negative);
            }
            return filters;
        }

        /// <summary>
        /// Gets the facets.
        /// </summary>
        /// <returns></returns>
        public BxFacets getFacets()
        {
            BxFacets facets = base.getFacets();
            if (facets == null)
            {
                facets = new BxFacets();
            }
            foreach (var obj in this.getPrefixedParameters(this.requestFacetsPrefix))
            {
                facets.addFacet(obj.Key, Convert.ToString(obj.Value));
            }
            return facets;
        }

        /// <summary>
        /// Gets the sort fields.
        /// </summary>
        /// <returns></returns>
        public BxSortFields getSortFields()
        {
            BxSortFields sortFields = base.getSortFields();
            if (sortFields == null)
            {
                sortFields = new BxSortFields();
            }
            foreach (var obj in this.getPrefixedParameters(this.requestSortFieldPrefix))
            {
                sortFields.push(obj.Key, Convert.ToBoolean(obj.Value));
            }
            return sortFields;
        }

        /// <summary>
        /// Gets the return fields.
        /// </summary>
        /// <returns></returns>
        public List<string> getReturnFields()
        {
            if (base.returnFields == null)
            {
                base.returnFields = new List<string>();
            }
             base.returnFields.AddRange(this.bxReturnFields);
            return base.returnFields.Distinct().ToList();
        }

        /// <summary>
        /// Gets all return fields.
        /// </summary>
        /// <returns></returns>
        public List<string> getAllReturnFields()
        {
            List<string> returnFields = this.getReturnFields();
            if (this.requestMap.FirstOrDefault(t => t.Key == this.requestReturnFieldsName).Value != null)
            {
                base.getReturnFields().Concat(this.bxReturnFields).Distinct().ToList();
                returnFields = returnFields.Concat(Convert.ToString(this.requestMap[this.requestReturnFieldsName]).Split(',')).ToList().Distinct().ToList();
            }
            return returnFields;
        }

        private Dictionary<string, object> callBackCache = null;
        /// <summary>
        /// Retrieves from call back.
        /// </summary>
        /// <param name="items">The items.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public Dictionary<string, object> retrieveFromCallBack(List<Hit> items, string[] fields)
        {
            if (this.callBackCache == null)
            {
                this.callBackCache = new Dictionary<string, object>();
                List<string> ids = new List<string>();
                foreach (var item in items)
                {
                    ids.AddRange(item.Values["id"]);
                }
               
            }
            return this.callBackCache;
        }

        /// <summary>
        /// Retrieves the hit field values.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="field">The field.</param>
        /// <param name="items">The items.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        public List<Dictionary<string, object>> retrieveHitFieldValues(Hit item, string field, List<Hit> items, string[] fields)
        {
            Dictionary<string, object> itemFields = this.retrieveFromCallBack(items, fields);
            List<Dictionary<string, object>> temp = new List<Dictionary<string, object>>();
            temp.Add(itemFields);
            if (temp[0]["id"] != null)
            {
                return (List<Dictionary<string, object>>)temp[0]["id"];
            }
            return base.retrieveHitFieldValues(item, field, items, fields);
        }
    }
}
