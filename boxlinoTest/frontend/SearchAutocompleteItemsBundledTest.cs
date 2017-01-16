using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.SessionState;
using System.Web;
using System.Reflection;
using System.IO;
using BoxalinoWeb.frontend;
using System.Linq;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchAutocompleteItemsBundledTest
    /// </summary>
    [TestClass]
    public class SearchAutocompleteItemsBundledTest
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
        public void testFrontendSearchAutocompleteItemsBundled()
        {
            SearchAutocompleteItemsBundled _searchAutocompleteItemsBundled = new SearchAutocompleteItemsBundled();
            try
            {
                _searchAutocompleteItemsBundled.account = this.account;
                _searchAutocompleteItemsBundled.password = this.password;
                _searchAutocompleteItemsBundled.print = false;
                List<string> firstTextualSuggestions = new List<string>() { { "ida workout parachute pant" }, { "jade yoga jacket" }, { "push it messenger bag" } };
                List<string> secondTextualSuggestions = new List<string>() { { "argus all weather tank" }, { "jupiter all weather trainer" }, { "livingston all purpose tight" } };
                _searchAutocompleteItemsBundled.searchAutocompleteItemsBundled();
                Assert.AreEqual(_searchAutocompleteItemsBundled.bxAutocompleteResponses.Count, 2);

                //first response
                CollectionAssert.AreEqual(_searchAutocompleteItemsBundled.bxAutocompleteResponses[0].getTextualSuggestions(), firstTextualSuggestions);

                //global ids
                CollectionAssert.AreEqual(_searchAutocompleteItemsBundled.bxAutocompleteResponses[0].getBxSearchResponse().getHitFieldValues(new string[] { "title" }).Keys, new List<string>() { { "115" }, { "131" }, { "227" }, { "355" }, { "611" } });

                //second response
                CollectionAssert.AreEqual(_searchAutocompleteItemsBundled.bxAutocompleteResponses[1].getTextualSuggestions(), secondTextualSuggestions);

                //global ids
                CollectionAssert.AreEqual(_searchAutocompleteItemsBundled.bxAutocompleteResponses[1].getBxSearchResponse().getHitFieldValues(new string[] { "title" }).Keys, new List<string>() { { "1545" } });

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
