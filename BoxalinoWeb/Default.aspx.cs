using Boxalino.backend;
using Boxalino.frontend;
using examples.backend;
using examples.frontend;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace examples
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            //DataBasic obj = new DataBasic();
            //obj.dataBasic();

            //DataCategories obj = new DataCategories();
            //obj.dataBasic();

            //DataCustomers obj = new DataCustomers();
            //obj.dataBasic();

            //DataDebugXml obj = new DataDebugXml();
            //obj.dataBasic();

            //DataFullExport obj = new DataFullExport();
            //obj.dataBasic();

            //DataInit obj = new DataInit();
            //obj.dataInit();

            //DataResource obj = new DataResource();
            //obj.dataResource();

            //DataSplitFieldValues obj = new DataSplitFieldValues();
            //obj.dataSplitFieldValues();

            //DataTransactions obj = new DataTransactions();
            //obj.dataTransactions();

            //ParametrizedRequest obj = new ParametrizedRequest();
            //obj.parametrizedRequest();

            //SearchDebugRequest obj = new SearchDebugRequest();
            //obj.searchDebugRequest();

            SearchFacetCategory obj = new SearchFacetCategory();
            obj.searchFacetCategory();
        }


    }
}