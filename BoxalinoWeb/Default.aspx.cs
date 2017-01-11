using Boxalino.backend;
using Boxalino.frontend;
using BoxalinoWeb.backend;
using BoxalinoWeb.frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BoxalinoWeb
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RecommendationsSimilar obj = new RecommendationsSimilar();
            obj.recommendationsSimilar();
        }


    }
}