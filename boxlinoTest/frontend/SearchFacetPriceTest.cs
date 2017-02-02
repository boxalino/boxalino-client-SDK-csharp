using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;
using System.Web.SessionState;
using System.Reflection;
using examples.frontend;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for SearchFacetPriceTest
    /// </summary>
    [TestClass]
    public class SearchFacetPriceTest
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
        public void testFrontendSearchFacetPrice()
        {
            SearchFacetPrice _searchFacetPrice = new SearchFacetPrice();
            try
            {
                _searchFacetPrice.account = this.account;
                _searchFacetPrice.password = this.password;
                _searchFacetPrice.print = false;

                _searchFacetPrice.searchFacetPrice();
                string[] princeRange = new string[_searchFacetPrice.facets.getPriceRanges().Keys.Count];
                _searchFacetPrice.facets.getPriceRanges().Keys.CopyTo(princeRange, 0);
                Assert.AreEqual(princeRange[0], "22-84");
                foreach (var item in _searchFacetPrice.bxResponse.getHitFieldValues(new string[] { (_searchFacetPrice.facets.getPriceFieldName()) }))
                {

                    Assert.IsTrue(Convert.ToDouble(((List<string>)item.Value["discountedPrice"].Value)[0]) > 22.0);
                    Assert.IsTrue(Convert.ToDouble(((List<string>)item.Value["discountedPrice"].Value)[0]) <= 84.0);
                }
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
