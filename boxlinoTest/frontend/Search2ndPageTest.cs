using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using examples.frontend;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for Search2ndPageTest
    /// </summary>
    [TestClass]
    public class Search2ndPageTest
    {
        private string account = "boxalino_automated_tests";
        private string password = "boxalino_automated_tests";

        [TestInitialize]
        public void TestSetup()
        {
            // We need to setup the Current HTTP Context as follows:            

            // Step 1: Setup the HTTP Request
            var httpRequest = new HttpRequest("", "http://localhost:6989/", "");

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
        public void testFrontendSearch2ndPage()
        {
            Search2ndPage _search2ndPage = new Search2ndPage();
            try
            {
                _search2ndPage.account = this.account;
                _search2ndPage.password = this.password;
                _search2ndPage.print = false;
                List<string> hitIds = new List<string>() { {"40"}, {"41"}, {"42"}, {"44"} };
                _search2ndPage.search2ndPage();
                CollectionAssert.AreEqual(_search2ndPage.bxResponse.getHitIds().Values, hitIds);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
