using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.IO;
using System.Reflection;
using System.Web.SessionState;
using examples.frontend;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for RecommendationsSimilarTest
    /// </summary>
    [TestClass]
    public class RecommendationsSimilarTest
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
        public void testFrontendRecommendationsSimilar()
        {
            RecommendationsSimilar _recommendationsSimilar = new RecommendationsSimilar();
            try
            {
                _recommendationsSimilar.account = this.account;
                _recommendationsSimilar.password = this.password;
                _recommendationsSimilar.print = false;
                List<string> hitIds = Enumerable.Range(1, 10).Select(n => n.ToString()).ToList();
                _recommendationsSimilar.recommendationsSimilar();
                CollectionAssert.AreEqual(_recommendationsSimilar.bxResponse.getHitIds().Values.ToList<string>(), hitIds);

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
