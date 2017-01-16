using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web;
using System.Web.SessionState;
using System.Reflection;
using System.IO;
using BoxalinoWeb.frontend;
using boxalino_client_SDK_CSharp;
using System.Linq;

namespace boxlinoTest.frontend
{
    /// <summary>
    /// Summary description for SearchAutocompleteItemsTest
    /// </summary>
    [TestClass]
    public class SearchAutocompleteItemsTest
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
        public void testFrontendSearchAutocompleteItems()
        {
            SearchAutocompleteItems _searchAutocompleteItems = new SearchAutocompleteItems();
            try
            {
                _searchAutocompleteItems.account = this.account;
                _searchAutocompleteItems.password = this.password;
                _searchAutocompleteItems.print = false;

                List<string> textualSuggestions = new List<string>() { { "ida workout parachute pant" }, { "jade yoga jacket" }, { "push it messenger bag" } };
                _searchAutocompleteItems.searchAutocompleteItems();

                NestedDictionary<string, object> itemSuggestions = _searchAutocompleteItems.bxAutocompleteResponse.getBxSearchResponse().getHitFieldValues(new string[] { "title" });

                Assert.AreEqual(itemSuggestions.Count, 5);

                CollectionAssert.AreEqual(_searchAutocompleteItems.bxAutocompleteResponse.getTextualSuggestions(), textualSuggestions);

            }
            catch (Exception ex)
            {
                Assert.Fail("Expected no exception, but got: " + ex.Message);
            }
        }
    }
}
