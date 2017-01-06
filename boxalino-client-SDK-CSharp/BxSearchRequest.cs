using boxalino_client_SDK_CSharp.Exception;
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
    public class BxSearchRequest : BxRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BxSearchRequest"/> class.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="queryText">The query text.</param>
        /// <param name="max">The maximum.</param>
        /// <param name="choiceId">The choice identifier.</param>
        /// <exception cref="BoxalinoException">BxRequest created with null choiceId</exception>
        public BxSearchRequest(string language, string queryText, int max = 10, string choiceId = null)
            : base(language, ((choiceId == null) ? "search" : choiceId), max, 0)
        {

            if (choiceId == "")
            {
                throw new BoxalinoException("BxRequest created with null choiceId");
            }

            base.setQuerytext(queryText);
        }
      
    }
}
