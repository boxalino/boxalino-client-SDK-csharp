
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
    public class BxRequest
    {
        #region declaration
        private string _language;
        /// <summary>
        /// Gets or sets the language.
        /// </summary>
        /// <value>
        /// The language.
        /// </value>
        public string language
        {
            get
            {
                return this._language;
            }
            set
            {
                this._language = value;
            }
        }

        private string _groupBy;
        /// <summary>
        /// Gets or sets the group by.
        /// </summary>
        /// <value>
        /// The group by.
        /// </value>
        public string groupBy
        {
            get
            {
                return this._groupBy;
            }
            set
            {
                this._groupBy = value;
            }
        }

        private string _choiceId;
        /// <summary>
        /// Gets or sets the choice identifier.
        /// </summary>
        /// <value>
        /// The choice identifier.
        /// </value>
        public string choiceId
        {
            get
            {
                return this._choiceId;
            }
            set
            {
                this._choiceId = value;
            }
        }

        private float _min;
        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>
        /// The minimum.
        /// </value>
        public float min
        {
            get
            {
                return this._min;
            }
            set
            {
                this._min = value;
            }
        }

        private float _max;
        /// <summary>
        /// Gets or sets the maximum.
        /// </summary>
        /// <value>
        /// The maximum.
        /// </value>
        public float max
        {
            get
            {
                return this._max;
            }
            set
            {
                this._max = value;
            }
        }

        private bool _withRelaxation;
        /// <summary>
        /// Gets or sets a value indicating whether [with relaxation].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [with relaxation]; otherwise, <c>false</c>.
        /// </value>
        public bool withRelaxation
        {
            get
            {
                return this._withRelaxation;
            }
            set
            {
                this._withRelaxation = value;
            }
        }

        private string _indexId;
        /// <summary>
        /// Gets or sets the index identifier.
        /// </summary>
        /// <value>
        /// The index identifier.
        /// </value>
        public string indexId
        {
            get
            {
                return this._indexId;
            }
            set
            {
                this._indexId = value;
            }
        }

        private Dictionary<string, string> _requestMap;
        /// <summary>
        /// Gets or sets the request map.
        /// </summary>
        /// <value>
        /// The request map.
        /// </value>
        public Dictionary<string, string> requestMap
        {
            get
            {
                return this._requestMap;
            }
            set
            {
                this._requestMap = value;
            }
        }

        private List<string> _returnFields;
        /// <summary>
        /// Gets or sets the return fields.
        /// </summary>
        /// <value>
        /// The return fields.
        /// </value>
        public List<string> returnFields
        {
            get
            {
                return this._returnFields;
            }
            set
            {
                this._returnFields = value;
            }
        }

        private int _offset;
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        /// <value>
        /// The offset.
        /// </value>
        public int offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                this._offset = value;
            }
        }

        private string _queryText;
        /// <summary>
        /// Gets or sets the query text.
        /// </summary>
        /// <value>
        /// The query text.
        /// </value>
        public string queryText
        {
            get
            {
                return this._queryText;
            }
            set
            {
                this._queryText = value;
            }
        }

        private BxFacets _bxFacets;
        /// <summary>
        /// Gets or sets the bx facets.
        /// </summary>
        /// <value>
        /// The bx facets.
        /// </value>
        public BxFacets bxFacets
        {
            get
            {
                return this._bxFacets;
            }
            set
            {
                this._bxFacets = value;
            }
        }

        private BxSortFields _bxSortFields;

        /// <summary>
        /// Gets or sets the bx sort fields.
        /// </summary>
        /// <value>
        /// The bx sort fields.
        /// </value>
        public BxSortFields bxSortFields
        {
            get
            {
                return this._bxSortFields;
            }
            set
            {
                this._bxSortFields = value;
            }
        }

        private Dictionary<string, BxFilter> _bxFilters;
        /// <summary>
        /// Gets or sets the bx filters.
        /// </summary>
        /// <value>
        /// The bx filters.
        /// </value>
        public Dictionary<string, BxFilter> bxFilters
        {
            get
            {
                return this._bxFilters;
            }
            set
            {
                this._bxFilters = value;
            }
        }

        private bool _orFilters;
        /// <summary>
        /// Gets or sets a value indicating whether [or filters].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [or filters]; otherwise, <c>false</c>.
        /// </value>
        public bool orFilters
        {
            get
            {
                return this._orFilters;
            }
            set
            {
                this._orFilters = value;
            }
        }

        private List<ContextItem> _contextItems = new List<ContextItem>();
        /// <summary>
        /// Gets or sets the context items.
        /// </summary>
        /// <value>
        /// The context items.
        /// </value>
        public List<ContextItem> contextItems
        {
            get
            {
                return this._contextItems;
            }
            set
            {
                this._contextItems = value;
            }
        }

        #endregion

        #region BxRequest
        /// <summary>
        /// Initializes a new instance of the <see cref="BxRequest"/> class.
        /// </summary>
        public BxRequest(string _language, string _choiceId, float max = 10, float min = 0)
        {
            indexId = string.Empty;
            requestMap = new Dictionary<string,string>();
            returnFields = new List<string>();
            offset = 0;
            queryText = string.Empty;
            bxFacets = new BxFacets();
            bxSortFields = new BxSortFields();
            bxFilters = new Dictionary<string, BxFilter>();
            orFilters = false;

            try
            {
                if (choiceId == string.Empty)
                {
                    throw new BoxalinoException("BxRequest created with null choiceId");
                }
                this.language = _language;
                this.choiceId = _choiceId;
                this.max = max;
                this.min = min;
                if (this.max == 0)
                {
                    this.max = 1;
                }
                this.withRelaxation = choiceId == "search";

            }
            catch (BoxalinoException ex)
            {

            }
        }
        #endregion


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
        /// Gets the simple search query.
        /// </summary>
        /// <returns></returns>
        public SimpleSearchQuery getSimpleSearchQuery()
        {
            SimpleSearchQuery searchQuery = new SimpleSearchQuery();
            searchQuery.IndexId = this.getIndexId();
            searchQuery.Language = this.getLanguage();
            dynamic child = this;
            searchQuery.ReturnFields = child.getReturnFields();
            searchQuery.Offset = this.getOffset();
            searchQuery.HitCount = (int)this.getMax(); // converted into int
            searchQuery.QueryText = this.getQuerytext();
            searchQuery.GroupBy = this.groupBy;
            if (this.getFilters().Count > 0)
            {
                searchQuery.Filters = new List<Filter>();
                foreach (var filter in this.getFilters().Values)
                {
                    searchQuery.Filters.Add(filter.getThriftFilter());
                }
            }
            searchQuery.OrFilters = this.getOrFilters();
            if (child.getFacets() != null)
            {
                searchQuery.FacetRequests = child.getFacets().getThriftFacets();
            }
            if (child.getSortFields() != null)
            {
                searchQuery.SortFields = child.getSortFields().getThriftSortFields();
            }
            return searchQuery;
        }

        /// <summary>
        /// Sets the product context.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="contextItemId">The context item identifier.</param>
        /// <param name="role">The role.</param>
        public void setProductContext(string fieldName, string contextItemId, string role = "mainProduct")
        {
            ContextItem contextItem = new ContextItem();
            contextItem.IndexId = this.getIndexId();
            contextItem.FieldName = fieldName;
            contextItem.ContextItemId = contextItemId;
            contextItem.Role = role;
            this.contextItems.Add(contextItem);
        }

        /// <summary>
        /// Gets the sort fields.
        /// </summary>
        /// <returns></returns>
        public BxSortFields getSortFields()
        {
            return this.bxSortFields;
        }

        /// <summary>
        /// Sets the sort fields.
        /// </summary>
        /// <param name="bxSortFields">The bx sort fields.</param>
        public void setSortFields(BxSortFields bxSortFields)
        {
            this.bxSortFields = bxSortFields;
        }

        /// <summary>
        /// Gets the or filters.
        /// </summary>
        /// <returns></returns>
        public bool getOrFilters()
        {
            return this.orFilters;
        }

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, BxFilter> getFilters()
        {
            Dictionary<string, BxFilter> filters = this.bxFilters;
            if (this.getFacets() != null)
            {
                foreach (var filter in this.getFacets().getFilters())
                {
                    filters.Add(filter.Key, (BxFilter)filter.Value);
                }
            }
            return this.bxFilters;
        }

        /// <summary>
        /// Sets the filters.
        /// </summary>
        /// <param name="bxFilters">The bx filters.</param>
        public void setFilters(Dictionary<string, BxFilter> bxFilters)
        {
            this.bxFilters = bxFilters;
        }

        /// <summary>
        /// Adds the filter.
        /// </summary>
        /// <param name="bxFilter">The bx filter.</param>
        public void addFilter(BxFilter bxFilter)
        {
            this.bxFilters.Add(bxFilter.getFieldName(), bxFilter);
        }

        /// <summary>
        /// Sets the or filters.
        /// </summary>
        /// <param name="orFilters">if set to <c>true</c> [or filters].</param>
        public void setOrFilters(bool orFilters)
        {
            this.orFilters = orFilters;
        }
        /// <summary>
        /// Adds the sort field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="reverse">if set to <c>true</c> [reverse].</param>
        public void addSortField(string field, bool reverse = false)
        {
            if (this.bxSortFields == null)
            {
                this.bxSortFields = new BxSortFields();
            }
            this.bxSortFields.push(field, reverse);
        }

        /// <summary>
        /// Sets the return fields.
        /// </summary>
        /// <param name="returnFields">The return fields.</param>
        public void setReturnFields(List<string> returnFields)
        {
            this.returnFields = returnFields;
        }

        /// <summary>
        /// Gets the facets.
        /// </summary>
        /// <returns></returns>
        public BxFacets getFacets()
        {
            return this.bxFacets;
        }

        /// <summary>
        /// Sets the facets.
        /// </summary>
        /// <param name="bxFacets">The bx facets.</param>
        public void setFacets(BxFacets bxFacets)
        {
            this.bxFacets = bxFacets;
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
            int k = 0;
            foreach (var contextItem in contextItems)
            {
                if (contextItem.IndexId == null)
                {
                   
                    this.contextItems[k].IndexId = indexId;
                }
                k++;
            }
        }

        /// <summary>
        /// Sets the default index identifier.
        /// </summary>
        /// <param name="indexId">The index identifier.</param>
        public void setDefaultIndexId(string indexId)
        {
            if (!string.IsNullOrEmpty(indexId))
            {
                this.setIndexId(indexId);
            }
        }

        /// <summary>
        /// Gets the language.
        /// </summary>
        /// <returns></returns>
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
        /// Gets the group by.
        /// </summary>
        /// <returns></returns>
        public string getGroupBy()
        {
            return this.groupBy;
        }

        /// <summary>
        /// Sets the group by.
        /// </summary>
        /// <param name="groupBy">The group by.</param>
        public void setGroupBy(string groupBy)
        {
            this.groupBy = groupBy;
        }
        /// <summary>
        /// Gets the return fields.
        /// </summary>
        /// <returns></returns>
        public List<string> getReturnFields()
        {
            return this.returnFields;
        }
        /// <summary>
        /// Gets the offset.
        /// </summary>
        /// <returns></returns>
        public int getOffset()
        {
            return this.offset;
        }

        /// <summary>
        /// Gets the maximum.
        /// </summary>
        /// <returns></returns>
        public float getMax()
        {
            return this.max;
        }

        /// <summary>
        /// Sets the offset.
        /// </summary>
        /// <param name="offset">The offset.</param>
        public void setOffset(int offset)
        {
            this.offset = offset;
        }

        /// <summary>
        /// Sets the maximum.
        /// </summary>
        /// <param name="max">The maximum.</param>
        public void setMax(int max)
        {
            this.max = max;
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
        /// Sets the basket product with prices.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="basketContent">Content of the basket.</param>
        /// <param name="role">The role.</param>
        /// <param name="subRole">The sub role.</param>
        public void setBasketProductWithPrices(string fieldName, List<CustomBasketContent> basketContent, string role = "mainProduct", string subRole = "mainProduct")
        {
            if (basketContent != null && basketContent.Count > 0)
            {
              
                basketContent = basketContent.OrderByDescending(x => x.Price, new SemiNumericComparer()).ToList();
                List<CustomBasketContent> basketItem = Common.array_shift<CustomBasketContent>(basketContent); 
                ContextItem contextItem = new ContextItem();
                contextItem.IndexId = this.getIndexId();
                contextItem.FieldName = fieldName;
                contextItem.ContextItemId = basketItem[0].Id;
                contextItem.Role = role;
                this.contextItems.Add(contextItem);

                foreach (var basketItem1 in basketContent)
                {
                    ContextItem contextItem1 = new ContextItem();
                    contextItem1.IndexId = this.getIndexId();
                    contextItem1.FieldName = fieldName;
                    contextItem1.ContextItemId = basketItem1.Id;
                    contextItem1.Role = subRole;
                    this.contextItems.Add(contextItem1);
                }
            }
        }

        /// <summary>
        /// Gets the context items.
        /// </summary>
        /// <returns></returns>
        public List<ContextItem> getContextItems()
        {
            return this.contextItems;
        }
        /// <summary>
        /// Gets the minimum.
        /// </summary>
        /// <returns></returns>
        public float getMin()
        {
            return this.min;
        }

        /// <summary>
        /// Sets the minimum.
        /// </summary>
        /// <param name="min">The minimum.</param>
        public void setMin(int min)
        {
            this.min = min;
        }
        /// <summary>
        /// Gets the with relaxation.
        /// </summary>
        /// <returns></returns>
        public bool getWithRelaxation()
        {
            return this.withRelaxation;
        }

        /// <summary>
        /// Sets the with relaxation.
        /// </summary>
        /// <param name="withRelaxation">if set to <c>true</c> [with relaxation].</param>
        public void setWithRelaxation(bool withRelaxation)
        {
            this.withRelaxation = withRelaxation;
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
        /// Gets the request context parameters.
        /// </summary>
        /// <returns></returns>
        public IDictionary<string, List<string>> getRequestContextParameters()
        {
          
            var contextParameters = new Dictionary<string, List<string>>();
           

            return contextParameters;
        }


        /// <summary>
        /// Sets the default request map.
        /// </summary>
        /// <param name="requestMap">The request map.</param>
        public void setDefaultRequestMap(Dictionary<string, string> requestMap)
        {
            if (this.requestMap == null)
            {
                this.requestMap = requestMap;
            }
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
            return new List<Dictionary<string, object>>();
        }

    }
    /// <summary>
    /// 
    /// </summary>
    public class CustomBasketContent
    {

        public string Id { get; set; }
        public string Price { get; set; }

    }
}
