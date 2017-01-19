using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Reflection;
using BoxalinoWeb.frontend;
using boxalino_client_SDK_CSharp;
using System.Linq;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchFacetTest
    /// </summary>
    [TestClass]
    public class SearchFacetTest
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
                                    System.Reflection.BindingFlags.NonPublic | BindingFlags.Instance,
                                    null,
                                    CallingConventions.Standard,
                                    new[] { typeof(HttpSessionStateContainer) },
                                    null)
                .Invoke(new object[] { sessionContainer });

            // Step 4: Assign the Context
            HttpContext.Current = httpContext;
        }

        [TestMethod]
        public void testFrontendSearchFacet()
        {
            SearchFacet _searchFacet = new SearchFacet();
            try
            {
                _searchFacet.account = this.account;
                _searchFacet.password = this.password;
                _searchFacet.print = false;

                _searchFacet.searchFacet();
                NestedDictionary<string, object> productColor1 = new NestedDictionary<string, object>();
                productColor1["products_color"].Value = new List<string>() { { "Black"}, { "Gray" }, { "Yellow" } };
                NestedDictionary<string, object> productColor2 = new NestedDictionary<string, object>();
                productColor2["products_color"].Value = new List<string>() { { "Gray" }, { "Orange" }, { "Yellow" } };
                CollectionAssert.AreEqual((List<string>)(_searchFacet.bxResponse.getHitFieldValues(_searchFacet.facetField.ToArray())["41"]).Value, (List<string>)productColor1.Value);
                CollectionAssert.AreEqual((List<string>)(_searchFacet.bxResponse.getHitFieldValues(_searchFacet.facetField.ToArray())["1940"]).Value, (List<string>)productColor1.Value);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
