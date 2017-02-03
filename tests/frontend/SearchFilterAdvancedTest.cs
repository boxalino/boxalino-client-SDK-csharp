using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Reflection;
using examples.frontend;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for SearchFilterAdvancedTest
    /// </summary>
    [TestClass]
    public class SearchFilterAdvancedTest
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
        public void testFrontendSearchFilterAdvanced()
        {
            SearchFilterAdvanced _searchFilterAdvanced = new SearchFilterAdvanced();
            try
            {
                _searchFilterAdvanced.account = this.account;
                _searchFilterAdvanced.password = this.password;
                _searchFilterAdvanced.print = false;

                _searchFilterAdvanced.searchFilterAdvanced();


                Assert.AreEqual(_searchFilterAdvanced.bxResponse.getHitFieldValues((new List<string>() { { "products_color" } }).ToArray()).Count, 10);

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
