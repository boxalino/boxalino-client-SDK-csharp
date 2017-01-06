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
    public class BxRecommendationRequest : BxRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BxRecommendationRequest"/> class.
        /// </summary>
        /// <param name="language">The language.</param>
        /// <param name="choiceIdSimilar">The choice identifier similar.</param>
        /// <param name="hitCount">The hit count.</param>
        public BxRecommendationRequest(string language,string choiceIdSimilar,int hitCount)
            : base(language, choiceIdSimilar, hitCount, 0)
        {

        }
    }
}
