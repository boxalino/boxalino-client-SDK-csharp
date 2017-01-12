using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BoxalinoWeb.frontend;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using System.IO;
using System.Reflection;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for RecommendationsBasketTest
    /// </summary>
    [TestClass]
    public class RecommendationsBasketTest
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
        public void testFrontendRecommendationsBasket()
        {
            RecommendationsBasket _recommendationsBasket = new RecommendationsBasket();
            try
            {
                _recommendationsBasket.account = this.account;
                _recommendationsBasket.password = this.password;
                _recommendationsBasket.print = false;
                List<string> hitIds = Enumerable.Range(1, 10).Select(n => n.ToString()).ToList();
                _recommendationsBasket.recommendationsBasket();
                CollectionAssert.AreEqual(_recommendationsBasket.bxResponse.getHitIds().Values.ToList<string>(), hitIds);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
