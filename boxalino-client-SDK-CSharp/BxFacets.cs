using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using boxalino_client_SDK_CSharp.Services;
using boxalino_client_SDK_CSharp;

namespace boxalino_client_SDK_CSharp
{
    /// <summary>
    /// 
    /// </summary>
    public class BxFacets
    {

        protected List<FacetResponse> facetResponse = new List<FacetResponse>();

        protected string parameterPrefix = "";

        protected string priceFieldName = "discountedPrice";

        public Dictionary<string, FacetValue> selectedValues = new Dictionary<string, FacetValue>();

        public NestedDictionary<string, object> facets = new NestedDictionary<string, object>();

        /// <summary>
        /// Gets the thrift facets.
        /// </summary>
        /// <returns></returns>
        public List<FacetRequest> getThriftFacets()
        {

            List<FacetRequest> thriftFacets = new List<FacetRequest>();
            foreach (var item in facets)
            {


                string type = item.Value["type"].Value.ToString();
                int order = Convert.ToInt32(item.Value["order"].Value);

                FacetRequest facetRequest = new FacetRequest();
                facetRequest.FieldName = item.Key.ToString();
                facetRequest.Numerical = type == "ranged" ? true : type == "numerical" ? true : false;
                facetRequest.Range = type == "ranged" ? true : false;
                facetRequest.boundsOnly = Convert.ToBoolean(item.Value["boundsOnly"].Value);
                facetRequest.SelectedValues = this.facetSelectedValue(item.Value.ToString(), type);
                facetRequest.SortOrder = (FacetSortOrder)((order == 1) ? 1 : 2);
                thriftFacets.Add(facetRequest);
            }

            return thriftFacets;
        }

        private Dictionary<string, Filter> filters = new Dictionary<string, Filter>();

        /// <summary>
        /// Gets the filters.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Filter> getFilters()
        {
            return this.filters;
        }

        /// <summary>
        /// Facets the selected value.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="option">The option.</param>
        /// <returns></returns>
        private List<FacetValue> facetSelectedValue(string fieldName, string option)
        {
            List<FacetValue> selectedFacets = new List<FacetValue>();
            if (this.facets != null)
            {
                foreach (var value in facets)
                {
                    if (value.Key == fieldName)
                    {
                        FacetValue selectedFacet = new FacetValue();
                        if (option == "ranged")
                        {
                            string[] rangedValue = value.Key.Split('-');
                            if (rangedValue[0] != "*")
                            {
                                selectedFacet.RangeFromInclusive = rangedValue[0];
                            }
                            if (rangedValue[1] != "*")
                            {
                                selectedFacet.RangeToExclusive = rangedValue[1];
                            }
                        }
                        else
                        {
                            selectedFacet.StringValue = value.Key;
                        }
                        selectedFacets.Add(selectedFacet);
                    }
                }
                return selectedFacets;
            }
            return null;
        }

        /// <summary>
        /// Adds the price range facet.
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="order">The order.</param>
        /// <param name="label">The label.</param>
        /// <param name="fieldName">Name of the field.</param>
        public void addPriceRangeFacet(string selectedValue = null, int order = 2, string label = "Price", string fieldName = "discountedPrice")
        {
            this.priceFieldName = fieldName;
            this.addRangedFacet(fieldName, selectedValue, label, order, true);
        }

        /// <summary>
        /// Adds the ranged facet.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="label">The label.</param>
        /// <param name="order">The order.</param>
        /// <param name="boundsOnly">if set to <c>true</c> [bounds only].</param>
        public void addRangedFacet(string fieldName, string selectedValue = null, string label = null, int order = 2, bool boundsOnly = false)
        {
            this.addFacet(fieldName, selectedValue, "ranged", label, order, boundsOnly);
        }

        /// <summary>
        /// Adds the facet.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="type">The type.</param>
        /// <param name="label">The label.</param>
        /// <param name="order">The order.</param>
        /// <param name="boundsOnly">if set to <c>true</c> [bounds only].</param>
        public void addFacet(string fieldName, string selectedValue = null, string type = "string", string label = null, int order = 2, bool boundsOnly = false)
        {
            selectedValues = new Dictionary<string, FacetValue>();
            if (selectedValue != null)
            {
                selectedValues.Add(selectedValue, new FacetValue());
            }
            NestedDictionary<string, object> fvalue = new NestedDictionary<string, object>();
            this.facets[fieldName]["label"].Value = label;
            this.facets[fieldName]["type"].Value = type;
            this.facets[fieldName]["order"].Value = order;
            this.facets[fieldName]["selectedValues"].Value = selectedValues;
            this.facets[fieldName]["boundsOnly"].Value = boundsOnly;
            

        }

        /// <summary>
        /// Gets the selected values.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public Dictionary<string, FacetValue> getSelectedValues(string fieldName)
        {
            selectedValues = new Dictionary<string, FacetValue>();
            try
            {
                foreach (var key in this.getFacetValues(fieldName))
                {
                    if (!string.IsNullOrEmpty(this.isFacetValueSelected(fieldName, key.Value)))
                    {
                        selectedValues.Add(key.Key, new FacetValue());
                    }
                }
            }
            catch (boxalino_client_SDK_CSharp.Exception.BoxalinoException ex)
            {

            }
            return selectedValues;
        }

        /// <summary>
        /// Gets the facet values.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public Dictionary<string, FacetValue> getFacetValues(string fieldName)
        {
            return this.getFacetKeysValues(fieldName);
        }

        /// <summary>
        /// Gets the facet keys values.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        protected Dictionary<string, FacetValue> getFacetKeysValues(string fieldName)
        {
            if (fieldName == "")
            {
                return new Dictionary<string, FacetValue>();
            }
            Dictionary<string, FacetValue> facetValues = new Dictionary<string, FacetValue>();

            FacetResponse facetResponse = this.getFacetResponse(fieldName);

            string type = this.getFacetType(fieldName);
            switch (type)
            {
                case "hierarchical":
                    Dictionary<string, Dictionary<string, FacetValue>> tree = this.buildTree(facetResponse);
                    tree = this.getSelectedTreeNode(tree);
                    Dictionary<string, Dictionary<string, FacetValue>> node = this.getFirstNodeWithSeveralChildren(tree);
                    if (node != null)
                    {
                        foreach (var fvalue in node.FirstOrDefault(x => x.Key == "children").Value)
                        {
                            facetValues[(fvalue.Key == "node") ? fvalue.Value.StringValue : ""] = node["node"].Values.FirstOrDefault();
                        }
                    }
                    break;
                case "ranged":
                    foreach (var facetValue in facetResponse.Values)
                    {

                        facetValues[facetValue.RangeFromInclusive + "-" + facetValue.RangeToExclusive] = facetValue;

                    }
                    break;
                default:
                    foreach (var facetValue in facetResponse.Values)
                    {
                        facetValues[facetValue.StringValue] = facetValue;
                    }
                    break;
            }
            return facetValues;
        }

        /// <summary>
        /// Gets the facet response.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        /// <exception cref="boxalino_client_SDK_CSharp.Exception.BoxalinoException">trying to get facet response on unexisting fieldname " + fieldName</exception>
        protected FacetResponse getFacetResponse(string fieldName)
        {
            if (this.facetResponse != null)
            {
                foreach (var facetResponse in this.facetResponse)
                {
                    if (facetResponse.FieldName == fieldName)
                    {
                        return facetResponse;
                    }
                }
            }
            throw new boxalino_client_SDK_CSharp.Exception.BoxalinoException("trying to get facet response on unexisting fieldname " + fieldName);
        }

        /// <summary>
        /// Gets the type of the facet.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        protected string getFacetType(string fieldName)
        {
            string type = "string";
            if (this.facets[fieldName] != null)
            {
                type = this.facets[fieldName]["type"].Value.ToString();
            }
            return type;
        }

        /// <summary>
        /// Builds the tree.
        /// </summary>
        /// <param name="response">The response.</param>
        /// <param name="parents">The parents.</param>
        /// <param name="parentLevel">The parent level.</param>
        /// <returns></returns>
        protected Dictionary<string, Dictionary<string, FacetValue>> buildTree(FacetResponse response, Dictionary<string, string> parents = null, int parentLevel = 0)
        {
            if (parents == null) { parents = new Dictionary<string, string>(); }
            if (parents.Count == 0)
            {
                foreach (var node in response.Values)
                {
                    if ((node.Hierarchy).Count() == 1)
                    {
                        parents = node.Hierarchy.ToDictionary(x => x, x => x);
                    }
                }
            }
            Dictionary<string, Dictionary<string, FacetValue>> children = new Dictionary<string, Dictionary<string, FacetValue>>();
            foreach (var node in response.Values)
            {
                if ((node.Hierarchy).Count == parentLevel + 2)
                {
                    bool allTrue = true;
                    foreach (var item in parents)
                    {
                        if (!string.IsNullOrEmpty(node.Hierarchy[Convert.ToInt32(item.Key)]) || node.Hierarchy[Convert.ToInt32(item.Key)] != item.Value)
                        {
                            allTrue = false;
                        }
                    }
                    if (allTrue)
                    {


                        children = (this.buildTree(response, node.Hierarchy.ToDictionary(x => x, x => x), parentLevel + 1));
                    }
                }
            }
            foreach (var node in response.Values)
            {
                if (node.Hierarchy.Count() == parentLevel + 1)
                {
                    bool allTrue = true;
                    foreach (var item in node.Hierarchy.Select((Key, Value) => new { Key, Value }))
                    {
                        if (parents[item.Key] != item.Value.ToString())
                        {
                            allTrue = false;
                        }
                    }
                    if (allTrue)
                    {
                        return new Dictionary<string, Dictionary<string, FacetValue>>() {
                                           { "node",     new Dictionary<string, FacetValue>() { {"node",node} } }
                                         , {"children",children.FirstOrDefault(t => t.Key == "children").Value }
                        };
                    }
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the selected tree node.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, FacetValue>> getSelectedTreeNode(Dictionary<string, Dictionary<string, FacetValue>> tree)
        {
            if ((this.facets["category_id"]) != null)
            {
                return tree;
            }
            if (tree["node"] == null)
            {
                return null;
            }
            List<string> parts = tree.FirstOrDefault(t => t.Key == "node").Value.FirstOrDefault(t => t.Key == "node").Value.StringValue.Split('/').ToList();
            if (parts[0].ToString() == this.facets["category_id"]["selectedValues"].FirstOrDefault().ToString())
            {
                return tree;
            }
            foreach (var node in tree["children"])
            {
                Dictionary<string, Dictionary<string, FacetValue>> result = this.getSelectedTreeNode(new Dictionary<string, Dictionary<string, FacetValue>>() { { "node", new Dictionary<string, FacetValue>() { { node.Key, node.Value } } } });
                if (result != null)
                {
                    return result;
                }
            }
            return null;
        }

        /// <summary>
        /// Gets the first node with several children.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <returns></returns>
        protected Dictionary<string, Dictionary<string, FacetValue>> getFirstNodeWithSeveralChildren(Dictionary<string, Dictionary<string, FacetValue>> tree)
        {
            if (tree["children"].Count == 0)
            {
                return null;
            }
            if (tree["children"].Count > 1)
            {
                return tree;
            }

            tree = new Dictionary<string, Dictionary<string, FacetValue>>() { { "children", new Dictionary<string, FacetValue>() { { "node", tree.FirstOrDefault(t => t.Key == "children").Value.ElementAt(0).Value } } } };
            return this.getFirstNodeWithSeveralChildren(tree);
        }

        /// <summary>
        /// Determines whether [is facet value selected] [the specified field name].
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string isFacetValueSelected(string fieldName, FacetValue facetValue)
        {
            var list = this.getFacetValueArray(fieldName, facetValue);
            string label = list[0];
            string parameterValue = list[1];
            string hitCount = list[2];
            string selected = list[3];
            return selected;
        }

        /// <summary>
        /// Gets the facet value array.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        /// <exception cref="boxalino_client_SDK_CSharp.Exception.BoxalinoException">Requesting an invalid facet values for fieldname: " + fieldName + ", requested value: " + facetValue + ", available values . " + string.Join(",", (keyValues).Keys)</exception>
        protected List<string> getFacetValueArray(string fieldName, FacetValue facetValue)
        {
            Dictionary<string, FacetValue> keyValues = this.getFacetKeysValues(fieldName);
            if (keyValues.FirstOrDefault().Value == null)
            {
                throw new boxalino_client_SDK_CSharp.Exception.BoxalinoException("Requesting an invalid facet values for fieldname: " + fieldName + ", requested value: " + facetValue + ", available values . " + string.Join(",", (keyValues).Keys));
            }
            string type = this.getFacetType(fieldName);
            FacetValue fv = keyValues.FirstOrDefault(t => t.Value == facetValue).Value != null ? keyValues.FirstOrDefault(t => t.Value == facetValue).Value : null;
            switch (type)
            {
                case "hierarchical":
                    List<string> parts = fv.StringValue.Split('/').ToList();
                    return new List<string>() { parts[parts.Count - 1], parts[0], fv.HitCount.ToString(), fv.Selected.ToString() };
                case "ranged":
                    double from = Math.Round(Convert.ToDouble(fv.RangeFromInclusive), 2);
                    double to = Math.Round(Convert.ToDouble(fv.RangeToExclusive), 2);
                    string valueLabel = from + " - " + to;
                    string paramValue = fv.StringValue;
                    paramValue = from + "-" + to;
                    return new List<string>() { valueLabel, paramValue, fv.HitCount.ToString(), fv.Selected.ToString() };

                default:
                    fv = keyValues.FirstOrDefault(t => t.Value == facetValue).Value;
                    return new List<string>() { fv.StringValue, fv.StringValue, fv.HitCount.ToString(), fv.Selected.ToString() };
            }
        }


        /// <summary>
        /// Sets the facet response.
        /// </summary>
        /// <param name="facetResponse">The facet response.</param>
        public void setFacetResponse(List<FacetResponse> facetResponse)
        {
            this.facetResponse = facetResponse;
        }

        /// <summary>
        /// Gets the name of the category field.
        /// </summary>
        /// <returns></returns>
        public string getCategoryFieldName()
        {
            return "categories";
        }

        /// <summary>
        /// Adds the category facet.
        /// </summary>
        /// <param name="selectedValue">The selected value.</param>
        /// <param name="order">The order.</param>
        public void addCategoryFacet(string selectedValue = null, int order = 2)
        {
            if ((selectedValue) != null)
            {
                this.addFacet("category_id", selectedValue, "hierarchical", "1");
            }
            this.addFacet(this.getCategoryFieldName(), null, "hierarchical", order.ToString());
        }

        /// <summary>
        /// Sets the parameter prefix.
        /// </summary>
        /// <param name="parameterPrefix">The parameter prefix.</param>
        public void setParameterPrefix(string parameterPrefix)
        {
            this.parameterPrefix = parameterPrefix;
        }

        /// <summary>
        /// Determines whether the specified field name is categories.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        protected int isCategories(string fieldName)
        {
            return fieldName.IndexOf("categories");
        }

        /// <summary>
        /// Gets the name of the facet parameter.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public string getFacetParameterName(string fieldName)
        {
            string parameterName = fieldName;
            if (this.isCategories(fieldName) > 0)
            {
                parameterName = "category_id";
            }
            return this.parameterPrefix + parameterName;
        }


        /// <summary>
        /// Gets the field names.
        /// </summary>
        /// <returns></returns>
        public List<string> getFieldNames()
        {
            return (this.facets).Keys.ToList();
        }

        /// <summary>
        /// Gets the name of the facet by field.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        protected NestedDictionary<string,object> getFacetByFieldName(string fieldName)
        {
            foreach (var item in this.facets)
            {
                if (fieldName == item.Key)
                {
                    return item.Value;
                }
            }
            return null;
        }

        /// <summary>
        /// Determines whether the specified field name is selected.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="ignoreCategories">if set to <c>true</c> [ignore categories].</param>
        /// <returns>
        ///   <c>true</c> if the specified field name is selected; otherwise, <c>false</c>.
        /// </returns>
        public bool isSelected(string fieldName, bool ignoreCategories = false)
        {
            if (fieldName == "")
            {
                return false;
            }
            if (this.isCategories(fieldName) > 0)
            {
                if (ignoreCategories)
                {
                    return false;
                }
            }
            if (this.getSelectedValues(fieldName).Count > 0)
            {
                return true;
            }
            NestedDictionary<string, object> facet = this.getFacetByFieldName(fieldName);
            if (facet != null)
            {
                if (facet["type"].Value.ToString() == "hierarchical")
                {
                    FacetResponse facetResponse = this.getFacetResponse(fieldName);
                    Dictionary<string, Dictionary<string, FacetValue>> tree = this.buildTree(facetResponse);
                    tree = this.getSelectedTreeNode(tree);
                    return tree.FirstOrDefault(t => t.Key == "node").Value.FirstOrDefault(t => t.Key == "node").Value.Hierarchy.Count > 1;
                }
                return this.facets[fieldName]["selectedValues"] != null && this.facets[fieldName]["selectedValues"].ToList().Count > 0;
            }
            return false;
        }

        /// <summary>
        /// Gets the tree parent.
        /// </summary>
        /// <param name="tree">The tree.</param>
        /// <param name="treeEnd">The tree end.</param>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, FacetValue>> getTreeParent(Dictionary<string, Dictionary<string, FacetValue>> tree, Dictionary<string, Dictionary<string, FacetValue>> treeEnd)
        {
            foreach (var child in tree["children"])
            {
                if ((child.Key == "node" ? child.Value.StringValue : "") == (treeEnd.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue))
                {
                    return tree;
                }
                Dictionary<string, Dictionary<string, FacetValue>> parent = this.getTreeParent(new Dictionary<string, Dictionary<string, FacetValue>>() {

                                          {"children",new Dictionary<string, FacetValue>() { { "children", child.Value } } }
                        }, treeEnd);
                if (parent != null)
                {
                    return parent;
                }
            }
            return null;
        }


        /// <summary>
        /// Gets the parent categories.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, Dictionary<string, FacetValue>> getParentCategories()
        {
            string fieldName = "categories";
            FacetResponse facetResponse = this.getFacetResponse(fieldName);
            Dictionary<string, Dictionary<string, FacetValue>> tree = this.buildTree(facetResponse);
            Dictionary<string, Dictionary<string, FacetValue>> treeEnd = this.getSelectedTreeNode(tree);
            if (treeEnd == null)
            {
                return new Dictionary<string, Dictionary<string, FacetValue>>();
            }
            if (treeEnd.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue == tree.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue)
            {
                return new Dictionary<string, Dictionary<string, FacetValue>>();
            }
            Dictionary<string, Dictionary<string, FacetValue>> parent = new Dictionary<string, Dictionary<string, FacetValue>>();
            parent = treeEnd;
            Dictionary<string, string> parents = null;
            while (parent.Count > 0)
            {
                List<string> parts = parent.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue.Split('/').ToList();
                parents = new Dictionary<string, string>() { { "0", parts[0] }, { "1", parts[(parts).Count - 1] } };
                parent = this.getTreeParent(tree, parent);
            }
            parents = parents.Keys.OrderBy(x => x, StringComparer.Ordinal).ToDictionary(x => x, x => x);
            Dictionary<string, Dictionary<string, FacetValue>> final = new Dictionary<string, Dictionary<string, FacetValue>>();
            foreach (var v in parents)
            {
                final.FirstOrDefault(x => x.Key == v.Key).Value.FirstOrDefault(x => x.Key == v.Key).Value.StringValue = v.Value;
            }
            return final;
        }

        /// <summary>
        /// Gets the parent categories hit count.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public long getParentCategoriesHitCount(int id)
        {
            string fieldName = "categories";
            FacetResponse facetResponse = this.getFacetResponse(fieldName);
            Dictionary<string, Dictionary<string, FacetValue>> tree = this.buildTree(facetResponse);
            Dictionary<string, Dictionary<string, FacetValue>> treeEnd = this.getSelectedTreeNode(tree);
            if (treeEnd == null)
            {
                return tree.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.HitCount;
            }
            if (treeEnd.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue == tree.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue)
            {
                return tree.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.HitCount;
            }
            Dictionary<string, Dictionary<string, FacetValue>> parent = treeEnd;
            while (parent.Count > 0)
            {
                if (Convert.ToInt32(parent.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.HierarchyId) == id)
                {
                    return Convert.ToInt32(parent.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.HierarchyId);
                }
                parent = this.getTreeParent(tree, parent);
            }
            return 0;
        }

        /// <summary>
        /// Gets the selected value label.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="index">The index.</param>
        /// <returns></returns>
        public string getSelectedValueLabel(string fieldName, int index = 0)
        {
            if (fieldName == "")
            {
                return "";
            }
            Dictionary<string,FacetValue> svs = this.getSelectedValues(fieldName);
            if (Convert.ToString(svs.Keys.ElementAt(index)) != null)
            {
                return this.getFacetValueLabel(fieldName, (FacetValue)svs[Convert.ToString(svs.Keys.ElementAt(index))]);
            }
            NestedDictionary<string,object> facet = this.getFacetByFieldName(fieldName);
            if (facet != null)
            {
                if (facet["type"].Value.ToString() == "hierarchical")
                {
                    FacetResponse facetResponse = this.getFacetResponse(fieldName);
                    Dictionary<string, Dictionary<string, FacetValue>> tree = this.buildTree(facetResponse);
                    tree = this.getSelectedTreeNode(tree);
                    List<string> parts = new List<string>() { tree.FirstOrDefault(x => x.Key == "node").Value.FirstOrDefault(x => x.Key == "node").Value.StringValue };
                    return parts[(parts).Count - 1];
                }
                if (facet["type"].Value.ToString() == "ranged")
                {
                    if (this.facets[fieldName]["selectedValues"] != null)
                    {
                        return this.facets[fieldName]["selectedValues"].FirstOrDefault().ToString();
                    }
                }
                if (facet["selectedValues"] !=null)
                {
                    return facet["selectedValues"].FirstOrDefault().ToString();
                }
                return "";
            }
            return "";
        }

        /// <summary>
        /// Gets the facet value label.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getFacetValueLabel(string fieldName, FacetValue facetValue)
        {
            var item = this.getFacetValueArray(fieldName, facetValue);
            string label = item[0];
            string parameterValue = item[1];
            string hitCount = item[2];
            string selected = item[3];

            return label;
        }

        /// <summary>
        /// Gets the name of the price field.
        /// </summary>
        /// <returns></returns>
        public string getPriceFieldName()
        {
            return this.priceFieldName;
        }

        /// <summary>
        /// Gets the categories key labels.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, FacetValue> getCategoriesKeyLabels()
        {
            Dictionary<string, FacetValue> categoryValueArray = new Dictionary<string, FacetValue>();
            foreach (var v in this.getCategories())
            {
                string label = this.getCategoryValueLabel(v.Value);
                categoryValueArray[label] = v.Value;
            }
            return categoryValueArray;
        }

        /// <summary>
        /// Gets the categories.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, FacetValue> getCategories()
        {
            return this.getFacetValues(this.getCategoryFieldName());
        }

        /// <summary>
        /// Gets the category value label.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getCategoryValueLabel(FacetValue facetValue)
        {
            return this.getFacetValueLabel(this.getCategoryFieldName(), facetValue);
        }


        /// <summary>
        /// Gets the price ranges.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, FacetValue> getPriceRanges()
        {
            return this.getFacetValues(this.getPriceFieldName());
        }

        /// <summary>
        /// Gets the facet label.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <returns></returns>
        public string getFacetLabel(string fieldName)
        {
            if (this.facets[fieldName] != null)
            {
                return this.facets[fieldName]["label"].Value.ToString();
            }
            return fieldName;
        }

        /// <summary>
        /// Gets the price value label.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getPriceValueLabel(FacetValue facetValue)
        {
            return this.getFacetValueLabel(this.getPriceFieldName(), facetValue);
        }

        /// <summary>
        /// Gets the facet value count.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getFacetValueCount(string fieldName, FacetValue facetValue)
        {
            var item = this.getFacetValueArray(fieldName, facetValue);
            string label = item[0];
            string parameterValue = item[1];
            string hitCount = item[2];
            string selected = item[3];
            return hitCount;
        }
        /// <summary>
        /// Gets the category value count.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getCategoryValueCount(FacetValue facetValue)
        {
            return this.getFacetValueCount(this.getCategoryFieldName(), facetValue);
        }

        /// <summary>
        /// Gets the price value count.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getPriceValueCount(FacetValue facetValue)
        {
            return this.getFacetValueCount(this.getPriceFieldName(), facetValue);
        }

        /// <summary>
        /// Gets the facet value parameter value.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getFacetValueParameterValue(string fieldName, FacetValue facetValue)
        {
            var item = this.getFacetValueArray(fieldName, facetValue);
            string label = item[0];
            string parameterValue = item[1];
            string hitCount = item[2];
            string selected = item[3];
            return parameterValue;
        }
        /// <summary>
        /// Gets the category value identifier.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getCategoryValueId(FacetValue facetValue)
        {
            return this.getFacetValueParameterValue(this.getCategoryFieldName(), facetValue);
        }

        /// <summary>
        /// Gets the price value parameter value.
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string getPriceValueParameterValue(FacetValue facetValue)
        {
            return this.getFacetValueParameterValue(this.getPriceFieldName(), facetValue);
        }


        /// <summary>
        /// Determines whether [is price value selected] [the specified facet value].
        /// </summary>
        /// <param name="facetValue">The facet value.</param>
        /// <returns></returns>
        public string isPriceValueSelected(FacetValue facetValue)
        {
            return this.isFacetValueSelected(this.getPriceFieldName(), facetValue);
        }


        /// <summary>
        /// Gets the parent identifier.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public string getParentId(string fieldName, string id)
        {
            List<string> hierarchy = new List<string>();
            foreach (var response in this.facetResponse)
            {
                if (response.FieldName == fieldName)
                {
                    foreach (var item in response.Values)
                    {
                        if (item.HierarchyId == id)
                        {
                            hierarchy = item.Hierarchy;
                            if (hierarchy.Count < 4)
                            {
                                return "1";
                            }
                        }
                    }
                    foreach (var item in response.Values)
                    {
                        if (item.Hierarchy.Count == hierarchy.Count - 1)
                        {
                            if (item.Hierarchy[hierarchy.Count - 2] == hierarchy[hierarchy.Count - 2])
                            {
                                return item.HierarchyId;
                            }
                        }
                    }
                }
            }
            return null;
        }
    }

}
