using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
using BoxalinoWeb.frontend;
using System.Linq;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchFacetCategoryTest
    /// </summary>
    [TestClass]
    public class SearchFacetCategoryTest
    {
        private string account = "boxalino_automated_tests";
        private string password = "boxalino_automated_tests";

        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new System.Web.HttpRequest("", "http://localhost:6989/", "");

            // Step 2: Setup the HTTP Response
            var httpResponce = new HttpResponse(new StringWriter());

            // Step 3: Setup the Http Context
            HttpContext httpContext = new HttpContext(httpRequest, httpResponce);
            var sessionContainer =
                new HttpSessionStateContainer(Guid.NewGuid().ToString("N"),
                                               new SessionStateItemCollection(),
                                               new HttpStaticObjectsCollection(),
                                               10,
                                               true,
                                               HttpCookieMode.AutoDetect,
                                               SessionStateMode.InProc,
                                               false);
            httpContext.Items["AspSession"] =
                typeof(HttpSessionState)
                .GetConstructor(
                                    BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void testFrontendSearchFacetCategory()
        {
            SearchFacetCategory _searchFacetCategory = new SearchFacetCategory();
            try
            {
                _searchFacetCategory.account = this.account;
                _searchFacetCategory.password = this.password;
                _searchFacetCategory.print = false;

                _searchFacetCategory.searchFacetCategory();

                CollectionAssert.AreEqual(_searchFacetCategory.bxResponse.getHitIds().Values, new List<string>() { { "41" }, { "1940" }, { "1065" }, { "1151" }, { "1241" }, { "1321" }, { "1385" }, { "1401" }, { "1609" }, { "1801" } });
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }

        }
    }
}
