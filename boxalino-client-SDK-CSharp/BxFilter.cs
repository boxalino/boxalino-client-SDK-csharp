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
    /// <seealso cref="boxalino_client_SDK_CSharp.Services.Filter" />
    public class BxFilter:Filter
    {
        protected string fieldName;
        protected List<string> values;
        protected bool negative;

        protected string hierarchyId = string.Empty;
        protected List<string> hierarchy = new List<string>();
        protected string rangeFrom = string.Empty;
        protected string rangeTo = string.Empty;

        /// <summary>
        /// Initializes a new instance of the <see cref="BxFilter"/> class.
        /// </summary>
        /// <param name="fieldName">Name of the field.</param>
        /// <param name="values">The values.</param>
        /// <param name="negative">if set to <c>true</c> [negative].</param>
        public BxFilter(string fieldName, List<string> values, bool negative = false)
        {
            this.fieldName = fieldName;
            this.values = values;
            this.negative = negative;
        }

        /// <summary>
        /// Gets the name of the field.
        /// </summary>
        /// <returns></returns>
        public string getFieldName()
        {
            return this.fieldName;
        }

        /// <summary>
        /// Gets the values.
        /// </summary>
        /// <returns></returns>
        public List<string> getValues()
        {
            return this.values;
        }

        /// <summary>
        /// Determines whether this instance is negative.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance is negative; otherwise, <c>false</c>.
        /// </returns>
        public bool isNegative()
        {
            return this.negative;
        }

        /// <summary>
        /// Gets the hierarchy identifier.
        /// </summary>
        /// <returns></returns>
        public string getHierarchyId()
        {
            return this.hierarchyId;
        }

        /// <summary>
        /// Sets the hierarchy identifier.
        /// </summary>
        /// <param name="hierarchyId">The hierarchy identifier.</param>
        public void setHierarchyId(string hierarchyId)
        {
            this.hierarchyId = hierarchyId;
        }

        /// <summary>
        /// Gets the hierarchy.
        /// </summary>
        /// <returns></returns>
        public List<string> getHierarchy()
        {
            return this.hierarchy;
        }

        /// <summary>
        /// Sets the hierarchy.
        /// </summary>
        /// <param name="hierarchy">The hierarchy.</param>
        public void setHierarchy(List<string> hierarchy)
        {
            this.hierarchy = hierarchy;
        }

        /// <summary>
        /// Gets the range from.
        /// </summary>
        /// <returns></returns>
        public string getRangeFrom()
        {
            return this.rangeFrom;
        }

        /// <summary>
        /// Sets the range from.
        /// </summary>
        /// <param name="rangeFrom">The range from.</param>
        public void setRangeFrom(string rangeFrom)
        {
            this.rangeFrom = rangeFrom;
        }

        /// <summary>
        /// Gets the range to.
        /// </summary>
        /// <returns></returns>
        public string getRangeTo()
        {
            return this.rangeTo;
        }

        /// <summary>
        /// Sets the range to.
        /// </summary>
        /// <param name="rangeTo">The range to.</param>
        public void setRangeTo(string rangeTo)
        {
            this.rangeTo = rangeTo;
        }

        /// <summary>
        /// Gets the thrift filter.
        /// </summary>
        /// <returns></returns>
        public Filter getThriftFilter()
        {
            Filter filter = new Filter();
            filter.FieldName = this.fieldName;
            filter.Negative = this.negative;
            filter.StringValues = this.values;
            if (this.hierarchyId != null)
            {
                filter.HierarchyId = this.hierarchyId;
            }
            if (this.hierarchy != null)
            {
                filter.Hierarchy = this.hierarchy;
            }
            if (this.rangeFrom != null)
            {
                filter.RangeFrom = this.rangeFrom;
            }
            if (this.rangeTo != null)
            {
                filter.RangeTo = this.rangeTo;
            }
            return filter;
        }
    }
}
