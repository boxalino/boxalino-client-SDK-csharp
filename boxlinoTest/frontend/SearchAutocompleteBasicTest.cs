using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Reflection;
using System.Web.SessionState;
using System.IO;
using BoxalinoWeb.frontend;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchAutocompleteBasicTest
    /// </summary>
    [TestClass]
    public class SearchAutocompleteBasicTest
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
        public void testFrontendSearchAutocompleteBasic()
        {
            SearchAutocompleteBasic _searchAutocompleteBasic = new SearchAutocompleteBasic();
            try
            {
                _searchAutocompleteBasic.account = this.account;
                _searchAutocompleteBasic.password = this.password;
                _searchAutocompleteBasic.print = false;
                List<string> textualSuggestions = new List<string>() { { "ida workout parachute pant" }, { "jade yoga jacket" }, { "push it messenger bag" } };
                _searchAutocompleteBasic.searchAutocompleteBasic();
                CollectionAssert.AreEqual(_searchAutocompleteBasic.bxAutocompleteResponse.getTextualSuggestions(), textualSuggestions);
            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
