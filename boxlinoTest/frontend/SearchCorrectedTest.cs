using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using examples.frontend;
using System.Web;
using System.Reflection;
using System.IO;
using System.Web.SessionState;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for SearchCorrectedTest
    /// </summary>
    [TestClass]
    public class SearchCorrectedTest
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
        public void testFrontendSearchCorrected()
        {
            SearchCorrected _searchCorrected = new SearchCorrected();
            try
            {
                _searchCorrected.account = this.account;
                _searchCorrected.password = this.password;
                _searchCorrected.print = false;

                List<string> hitIds = new List<string>() { { "41" }, { "1940" }, { "1065" }, { "1151" }, { "1241" }, { "1321" }, { "1385" }, { "1401" }, { "1609" }, { "1801" } };
                _searchCorrected.searchCorrected();
                Assert.AreEqual(_searchCorrected.bxResponse.areResultsCorrected(), true);
                CollectionAssert.AreEqual(_searchCorrected.bxResponse.getHitIds().Values, hitIds);

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
