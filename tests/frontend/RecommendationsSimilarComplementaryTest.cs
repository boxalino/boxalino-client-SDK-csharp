using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using examples.frontend;
using System.IO;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.Linq;

namespace tests.frontend
{
    /// <summary>
    /// Summary description for RecommendationsSimilarComplementaryTest
    /// </summary>
    [TestClass]
    public class RecommendationsSimilarComplementaryTest
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
        public void testFrontendRecommendationsSimilarComplementary()
        {
            RecommendationsSimilarComplementary _recommendationsSimilarComplementary = new RecommendationsSimilarComplementary();
            try
            {
                _recommendationsSimilarComplementary.account = this.account;
                _recommendationsSimilarComplementary.password = this.password;
                _recommendationsSimilarComplementary.print = false;
                string choiceIdSimilar = "similar";
                string choiceIdComplementary = "complementary";
                List<string> complementaryIds = Enumerable.Range(11, 10).Select(n => n.ToString()).ToList();
                List<string> similarIds = Enumerable.Range(1, 10).Select(n => n.ToString()).ToList();
                _recommendationsSimilarComplementary.recommendationsSimilarComplementary();
                CollectionAssert.AreEqual(_recommendationsSimilarComplementary.bxResponse.getHitIds(choiceIdSimilar).Values.ToList<string>(), similarIds);
                CollectionAssert.AreEqual(_recommendationsSimilarComplementary.bxResponse.getHitIds(choiceIdComplementary).Values.ToList<string>(), complementaryIds);
            }
            catch (Exception ex)    
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
