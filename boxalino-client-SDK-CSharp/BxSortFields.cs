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
    public class BxSortFields
    {
        private Dictionary<string, bool> sorts = new Dictionary<string, bool>();
        /// <summary>
        /// Initializes a new instance of the <see cref="BxSortFields"/> class.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="reverse">if set to <c>true</c> [reverse].</param>
        public BxSortFields(string field = null, bool reverse = false)
        {
            if (field != null)
            {
                this.push(field, reverse);
            }
        }
        /**
         * @param $field name od field to sort by (i.e. discountedPrice / title)
         * @param $reverse true for ASC, false for DESC
         */
        /// <summary>
        /// Pushes the specified field.
        /// </summary>
        /// <param name="field">The field.</param>
        /// <param name="reverse">if set to <c>true</c> [reverse].</param>
        public void push(string field, bool reverse = false)
        {
            this.sorts[field] = reverse;
        }
        /// <summary>
        /// Gets the sort fields.
        /// </summary>
        /// <returns></returns>
        public Dictionary<string, bool> getSortFields()
        {
            return this.sorts;
        }

        /// <summary>
        /// Determines whether [is field reverse] [the specified field].
        /// </summary>
        /// <param name="field">The field.</param>
        /// <returns>
        ///   <c>true</c> if [is field reverse] [the specified field]; otherwise, <c>false</c>.
        /// </returns>
        public bool isFieldReverse(string field)
        {
            if (this.sorts != null && this.sorts[field] != null)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Gets the thrift sort fields.
        /// </summary>
        /// <returns></returns>
        public List<SortField> getThriftSortFields()
        {
            List<SortField> sortFields = new List<SortField>();
            foreach (var field in this.getSortFields())
            {
                SortField objSortFields = new SortField();
                objSortFields.FieldName = field.Key;
                objSortFields.Reverse = this.isFieldReverse(field.Key);
            }
            return sortFields;
        }
    }
}
